namespace ViewModels.General
{
    public class ApiResponseWithDataViewModel<T> : ApiResponseViewModel
    {
        #region Constructor
        public ApiResponseWithDataViewModel(T? data, bool succeeded) : base(succeeded)
        {
            if (data == null)
            {
                AddInformationMessage("No Data");
                AddHiddenMessage("Ok");
            }
            else if (succeeded)
            {
                AddHiddenMessage("Ok");
            }

            Data = data;
        }
        #endregion /Constructor

        #region Properties

        #region public List<string> InformationMessages { get; set; }
        /// <summary>
        /// your Data for send to backend
        /// </summary>
        public T? Data { get; protected set; }
        #endregion /public List<string> InformationMessages { get; set; }

        #endregion /Properties
    }
}
