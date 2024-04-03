using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Infrastructure;

namespace PharmacyApi.Infrastructure
{
	public class BaseController : Controller
    {
        #region Properties

        #region private DatabaseContext myDatabaseContext
        /// <summary>
        /// private instance of database context for check all time one instace alive
        /// </summary>
        private DatabaseContext? myDatabaseContext;
        #endregion private DatabaseContext myDatabaseContext

        #region protected DatabaseContext MyDatabaseContext
        /// <summary>
        /// instance of database context you cand used on controllers
        /// </summary>
        protected DatabaseContext MyDatabaseContext
        {
            get
            {
                if (myDatabaseContext == null)
                {
                    myDatabaseContext =
                        new DatabaseContext();
                }

                return (myDatabaseContext);
            }
        }
        #endregion protected DatabaseContext MyDatabaseContext

        #endregion Properties

        #region Methods

        #region protected Task<bool> EntityExists<TModel>(Guid id) where TModel : BaseEntity
        protected Task<bool> EntityExists<TModel>(Guid id) where TModel : BaseEntity
        {
            return MyDatabaseContext.Set<TModel>()
               .Where(current => current.IsDeleted == false)
               .AnyAsync(e => e.Id == id);
        }
        #endregion /protected Task<bool> EntityExists<TModel>(Guid id) where TModel : BaseEntity

        #region protected bool EntityExists<TModel>(Guid id, out TModel? foundedItem) where TModel : BaseEntity
        protected bool EntityExists<TModel>(Guid id, out TModel? foundedItem) where TModel : BaseEntity
        {
            foundedItem = MyDatabaseContext.Set<TModel>()
                .Where(current => current.IsDeleted == false)
                .FirstOrDefault();

            return foundedItem != null;
        }
        #endregion /protected bool EntityExists<TModel>(Guid id, out TModel? foundedItem) where TModel : BaseEntity

        #region protected override void Dispose(bool disposing)
        /// <summary>
        /// Method for Dispose database context when worked finished 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (myDatabaseContext != null)
                {
                    myDatabaseContext.Dispose();
                    myDatabaseContext = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion protected override void Dispose(bool disposing)

        #endregion /Methods
    }
}
