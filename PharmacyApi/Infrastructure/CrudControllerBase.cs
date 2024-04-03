using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Infrastructure;
using Services;
using System.Linq.Dynamic.Core;
using System.Reflection;
using ViewModels.General;
using X.PagedList;

namespace PharmacyApi.Infrastructure
{
	[ApiController]
    [Route("[controller]")]
    public class CrudControllerBase<TViewModel, TModel, TController> : BaseController
        where TModel : BaseEntity
        where TController : class
        where TViewModel : class
    {
        private readonly ILogger<TController> _logger;
        private readonly IMapper _mapper;
        private readonly ApiServiceGeneric<TViewModel> _apiService;

        public CrudControllerBase(
               ILogger<TController> logger,
               IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _apiService = new ApiServiceGeneric<TViewModel>();
        }

        #region [HttpGet]
        [HttpGet]
        public virtual async Task<dynamic> List(int? pageNumber, int? pageSize, bool? paging,
            string? orderBy, string? direction, string? search)
        {
            try
            {
                var properties = typeof(TModel).GetRuntimeProperties();

                var inputValue = new ApiPagingViewModel(pageNumber, pageSize, paging, orderBy, direction, properties);

                var query = MyDatabaseContext.Set<TModel>().Where(current => current.IsDeleted == false);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.WhereContains<TModel>(search);
                }


                var foundedItems = await query
                    .OrderBy<TModel>(inputValue.OrderByColumn, inputValue.IsASC)
                    .ToPagedListAsync(inputValue.PageNumber, inputValue.PageSize)
                    .ConfigureAwait(false);

                if (!inputValue.NeedPaging)
                    return new ApiResponseWithDataViewModel<IPagedList<TViewModel>>
                        (foundedItems.Select(
                            current => _mapper.Map<TModel, TViewModel>(current)), true);

                var modalResult = _apiService.
                    ToPaginate(foundedItems.Select(current => _mapper.Map<TModel, TViewModel>(current)));

                return new ApiResponseWithDataViewModel<ApiPagingResponseViewModel<TViewModel>>(modalResult, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the List operation.");

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpGet]

        #region [HttpPost]
        [HttpPost]
        public virtual async Task<dynamic> Create(TModel entity)
        {
            try
            {
                var thisTime = DateTime.Now;

                entity.LastUpdateDate = thisTime;
                entity.DateAdded = thisTime;

                if (!ModelState.IsValid)
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Validation Error");

                    return resultModel;
                }
                await MyDatabaseContext.Set<TModel>().AddAsync(entity).ConfigureAwait(false);

                var saveChangesResult = await MyDatabaseContext.SaveChangesAsync();

                if (saveChangesResult != 1)
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Some Error In Save To Database");

                    return resultModel;
                }

                return new ApiResponseWithDataViewModel<TViewModel>(_mapper.Map<TModel, TViewModel>(entity), true);
            }
            catch (Exception ex)
            {
                _logger.Log(logLevel: LogLevel.Error, ex.ToString());

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpPost]

        #region [HttpPost, Route("List")]
        [HttpPost, Route("List")]
        public virtual async Task<dynamic> CreateList(IEnumerable<TModel> entities)
        {
            try
            {
                var thisTime = DateTime.Now;


                foreach (var entity in entities)
                {
                    entity.LastUpdateDate = thisTime;
                    entity.DateAdded = thisTime;
                }

                await MyDatabaseContext.Set<TModel>().AddRangeAsync(entities).ConfigureAwait(false);

                var saveChangesResult = await MyDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                if (saveChangesResult != entities.Count())
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Some Error In Save To Database");

                    return resultModel;
                }

                return new ApiResponseWithDataViewModel<IEnumerable<TViewModel>>
                    (entities.Select(current => _mapper.Map<TModel, TViewModel>(current)), true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the List operation.");

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpPost, Route("List")]

        #region [HttpPut("{id}")]
        [HttpPut("{id}")]
        public virtual async Task<dynamic> Update(Guid id, TModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    var resultModel = new ApiResponseViewModel(false);
                    resultModel.AddErrorMessage("Entity Id And Url Id Not Match");

                    return resultModel;

                }

                if (!await EntityExists<TModel>(id))
                {
                    var resultModel = new ApiResponseViewModel(false);
                    resultModel.AddErrorMessage("Cannot Find Item Please Refresh Page");

                    return resultModel;
                }

                var thisTime = DateTime.Now;

                entity!.LastUpdateDate = thisTime;

                MyDatabaseContext.Entry(entity).State = EntityState.Modified;

                var saveChangesResult = await MyDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                if (saveChangesResult != 1)
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Some Error In Save To Database");

                    return resultModel;
                }

                return new ApiResponseWithDataViewModel<TViewModel>(_mapper.Map<TModel, TViewModel>(entity), true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the List operation.");

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpPut("{id}")]

        #region [HttpPatch("{id}")]
        [HttpPatch("{id}")]
        public virtual async Task<dynamic> UpdateItem(Guid id, TViewModel viewModel)
        {
            try
            {
                if (!await EntityExists<TModel>(id))
                {
                    var resultModel = new ApiResponseViewModel(false);
                    resultModel.AddErrorMessage("Cannot Find Item Please Refresh Page");

                    return resultModel;
                }

                var thisTime = DateTime.Now;

                var entity = MyDatabaseContext.Set<TModel>()
                    .Where(current => current.IsDeleted == false &&
                        current.Id == id)
                    .FirstOrDefault();

                var properties = typeof(TViewModel).GetRuntimeProperties();

                foreach (var property in properties)
                {

                    var value = property.GetValue(viewModel);
                    if (value != null)
                    {
                        var tModelProperty = typeof(TModel).GetRuntimeProperties()
                            .Where(current => current.Name == property.Name)
                            .FirstOrDefault();

                        if (tModelProperty == null)
                            continue;

                        tModelProperty.SetValue(entity, value);
                    }
                }

                entity!.LastUpdateDate = thisTime;

                MyDatabaseContext.Entry(entity).State = EntityState.Modified;

                var saveChangesResult = await MyDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                if (saveChangesResult != 1)
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Some Error In Save To Database");

                    return resultModel;
                }

                return new ApiResponseWithDataViewModel<TViewModel>(_mapper.Map<TModel, TViewModel>(entity), true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the List operation.");

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpPatch("{id}")]

        #region [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        public virtual async Task<dynamic> Delete(Guid id)
        {
            try
            {
                var entity = await MyDatabaseContext.Set<TModel>().FindAsync(id).ConfigureAwait(false);

                if (entity == null)
                {
                    var resultModel = new ApiResponseViewModel(false);
                    resultModel.AddErrorMessage("Cannot Find Item Please Refresh Page");

                    return resultModel;
                }

                var thisTime = DateTime.Now;

                entity.LastUpdateDate = thisTime;

                entity.IsDeleted = true;

                MyDatabaseContext.Entry(entity).State = EntityState.Modified;

                var saveChangesResult = await MyDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                if (saveChangesResult != 1)
                {
                    var resultModel = new ApiResponseViewModel(false);

                    resultModel.AddErrorMessage("Some Error In Save To Database");

                    return resultModel;
                }

                return new ApiResponseWithDataViewModel<TViewModel>(_mapper.Map<TModel, TViewModel>(entity), true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the List operation.");

                var resultModel = new ApiResponseViewModel(false);
                resultModel.AddErrorMessage(ex.ToString());

                return resultModel;
            }
        }
        #endregion /[HttpDelete("{id}")]
    }
}