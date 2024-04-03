using System.Dynamic;
using System.Reflection;
using System.Xml.Linq;

namespace ViewModels.General
{
    public class ApiPagingViewModel : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        public ApiPagingViewModel(int? pageNumber, int? pageSize, bool? paging, string? orderBy, string? direction, IEnumerable<PropertyInfo> properties) : base()
        {
            NeedPaging = fixPaging(paging);
            IsASC = fixDirection(direction);
            PageSize = fixPageSize(pageSize);
            PageNumber = fixPageNumber(pageNumber);
            OrderByColumn = fixOrderBy(orderBy, properties);
        }
        #endregion /Constructor

        #region Properties

        #region public int PageNumber { get; protected set; }
        /// <summary>
        /// Number of Page
        /// </summary>
        public int PageNumber { get; protected set; }
        #endregion public int PageNumber { get; protected set; }

        #region public int PageSize { get; protected set; }
        /// <summary>
        /// Lenght of page
        /// </summary>
        public int PageSize { get; protected set; }
        #endregion public int PageSize { get; protected set; }

        #region public bool NeedPaging { get; protected set; }
        /// <summary>
        /// Need Paging
        /// </summary>
        public bool NeedPaging { get; protected set; }
        #endregion public bool NeedPaging { get; protected set; }

        #region public bool IsASC { get; protected set; }
        /// <summary>
        /// order by asc or not
        /// </summary>
        public bool IsASC { get; protected set; }
        #endregion public bool IsASC { get; protected set; }

        #region public string OrderByColumn { get; protected set; }
        /// <summary>
        /// order by column
        /// </summary>
        public string OrderByColumn { get; protected set; }
        #endregion /public string OrderByColumn { get; protected set; }

        #endregion /Properties

        #region /Method

        #region private int fixPageNumber(int? pageNumber)
        /// <summary>
        /// check input page number and if this is null return 1
        /// </summary>
        /// <param name="pageNumber">user choosen this number could be null</param>
        /// <returns>int number of page</returns>
        private int fixPageNumber(int? pageNumber)
        {
            if (pageNumber.HasValue)
                return pageNumber.Value;
            else
                return 1;
        }
        #endregion /private int fixPageNumber(int? pageNumber)

        #region private int fixPageSize(int? pageSize)
        /// <summary>
        /// check input page size and if this is null return 1
        /// </summary>
        /// <param name="pageSize">user choosen this number could be null</param>
        /// <returns>int number of page size</returns>
        private int fixPageSize(int? pageSize)
        {
            if (pageSize.HasValue)
                return pageSize.Value;
            else
                return 10;
        }
        #endregion /private int fixPageSize(int? pageSize)

        #region private bool fixPaging(bool? pageSize)
        /// <summary>
        /// check input user need paging or not
        /// </summary>
        /// <param name="paging">user choosen this true/false could be null</param>
        /// <returns>true/false</returns>
        private bool fixPaging(bool? paging)
        {
            if (paging.HasValue)
                return paging.Value;
            else
                return true;
        }
        #endregion /private bool fixPaging(int? pageSize)

        #region private bool fixDirection(bool? direction)
        /// <summary>
        /// check input order by direction
        /// </summary>
        /// <param name="direction">if user send asc order by asc if send desc order by desc and send anything other order by asc</param>
        /// <returns>true/false</returns>
        private bool fixDirection(string? direction)
        {
            if (direction == null)
            {
                return true;
            }
            else if (direction.ToLower() == "desc")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion /private bool fixDirection(bool? direction)

        #region private bool fixDirection(bool? direction)
        /// <summary>
        /// check selected column is exist
        /// </summary>
        /// <param name="orderBy">Selected user column</param>
        /// <returns>Column name</returns>
        private string fixOrderBy(string? orderBy, IEnumerable<PropertyInfo> properties)
        {
            var foundedFirstItem = properties
                .Where(current=>current.PropertyType.Name!="ICollection`1"
                &&current.PropertyType.Name!= "List`1")
                .FirstOrDefault();

            if (foundedFirstItem == null)
                return "";

            if (orderBy == null)
                return foundedFirstItem.Name;

            orderBy = orderBy.ToLower().Trim();

            var foundedColumn = properties
                .Where(current => current.Name.ToLower() == orderBy)
                .FirstOrDefault();

            if (foundedColumn == null)
                return foundedFirstItem.Name;

            return foundedColumn.Name;
        }
        #endregion /private bool fixDirection(bool? direction)

        #endregion /Method
    }
}
