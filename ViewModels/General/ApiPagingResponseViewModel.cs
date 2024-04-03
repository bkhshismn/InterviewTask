namespace ViewModels.General
{
    public class ApiPagingResponseViewModel<T> : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        public ApiPagingResponseViewModel(
            int? count,
            int? firstItemOnPage,
            bool? hasNextPage,
            bool? hasPreviousPage,
            bool? isFirstPage,
            bool? isLastPage,
            int? lastItemOnPage,
            int? pageCount,
            int? pageNumber,
            int? pageSize,
            int? totalItemCount,
            List<T>? data
            ) : base()
        {
            if (count.HasValue)
                Count = count.Value;
            else
                Count = 0;

            if (firstItemOnPage.HasValue)
                FirstItemOnPage = firstItemOnPage.Value;
            else
                FirstItemOnPage = 0;

            if (hasNextPage.HasValue)
                HasNextPage = hasNextPage.Value;
            else
                HasNextPage = false;

            if (hasPreviousPage.HasValue)
                HasPreviousPage = hasPreviousPage.Value;
            else
                HasPreviousPage = false;

            if (isFirstPage.HasValue)
                IsFirstPage = isFirstPage.Value;
            else
                IsFirstPage = false;

            if (isLastPage.HasValue)
                IsLastPage = isLastPage.Value;
            else
                IsLastPage = false;

            if (lastItemOnPage.HasValue)
                LastItemOnPage = lastItemOnPage.Value;
            else
                LastItemOnPage = 0;

            if (pageCount.HasValue)
                PageCount = pageCount.Value;
            else
                PageCount = 0;

            if (pageNumber.HasValue)
                PageNumber = pageNumber.Value;
            else
                PageNumber = 0;

            if (pageSize.HasValue)
                PageSize = pageSize.Value;
            else
                PageSize = 0;

            if (totalItemCount.HasValue)
                TotalItemCount = totalItemCount.Value;
            else
                TotalItemCount = 0;

            if (data == null)
                Items = new List<T>();
            else
                Items = data;
        }
        #endregion /Constructor

        #region Properties

        #region public int Items { get; protected set; }
        /// <summary>
        /// this page data
        /// </summary>
        public List<T> Items { get; protected set; }
        #endregion public int Items { get; protected set; }

        #region public int Count { get; protected set; }
        /// <summary>
        /// count In Page
        /// </summary>
        public int Count { get; protected set; }
        #endregion public int Count { get; protected set; }

        #region public int FirstItemOnPage { get; protected set; }
        /// <summary>
        /// number of first item on this page
        /// </summary>
        public int FirstItemOnPage { get; protected set; }
        #endregion public int FirstItemOnPage { get; protected set; }

        #region public bool HasNextPage { get; protected set; }
        /// <summary>
        /// if has next page return true
        /// </summary>
        public bool HasNextPage { get; protected set; }
        #endregion public bool HasNextPage { get; protected set; }

        #region public bool HasPreviousPage { get; protected set; }
        /// <summary>
        /// if has previous page return true
        /// </summary>
        public bool HasPreviousPage { get; protected set; }
        #endregion public bool HasPreviousPage { get; protected set; }

        #region public bool IsFirstPage { get; protected set; }
        /// <summary>
        /// if this page is first page return true
        /// </summary>
        public bool IsFirstPage { get; protected set; }
        #endregion public bool IsFirstPage { get; protected set; }

        #region public bool IsLastPage { get; protected set; }
        /// <summary>
        /// if this page is last page return true
        /// </summary>
        public bool IsLastPage { get; protected set; }
        #endregion public bool IsLastPage { get; protected set; }

        #region public int LastItemOnPage { get; protected set; }
        /// <summary>
        /// return number of last item on this page
        /// </summary>
        public int LastItemOnPage { get; protected set; }
        #endregion public int LastItemOnPage { get; protected set; }

        #region public int PageCount { get; protected set; }
        /// <summary>
        /// count of all page 
        /// </summary>
        public int PageCount { get; protected set; }
        #endregion public int PageCount { get; protected set; }

        #region public int PageNumber { get; protected set; }
        /// <summary>
        /// this page number 
        /// </summary>
        public int PageNumber { get; protected set; }
        #endregion public int PageNumber { get; protected set; }

        #region public int PageSize { get; protected set; }
        /// <summary>
        /// this page size
        /// </summary>
        public int PageSize { get; protected set; }
        #endregion public int PageSize { get; protected set; }

        #region public int TotalItemCount { get; protected set; }
        /// <summary>
        /// count of all records
        /// </summary>
        public int TotalItemCount { get; protected set; }
        #endregion public int TotalItemCount { get; protected set; }

        #endregion /Properties
    }
}
