using PublicService;

namespace ViewModels.General
{
	public class ApiResponseViewModel : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="succeeded">succeeded</param>
        public ApiResponseViewModel(bool succeeded) : base()
        {
            Succeeded = succeeded;
            InformationMessages = new List<string>();
            HiddenMessages = new List<string>();
            ErrorMessages = new List<string>();
            DateTime = DateTime.Now;

            if (succeeded)
            {
                AddHiddenMessage("Ok");
            }

            Version = "V 1.1.30 2024-03-01";
        }
        #endregion /Constructor

        #region Properties

        #region public List<string> InformationMessages { get; set; }
        /// <summary>
        /// Information Message
        /// </summary>
        public List<string> InformationMessages { get; protected set; }
        #endregion /public List<string> InformationMessages { get; set; }

        #region public List<string> HiddenMessages { get; set; }
        /// <summary>
        /// Hidden Message
        /// </summary>
        public List<string> HiddenMessages { get; protected set; }
        #endregion /public List<string> HiddenMessages { get; set; }

        #region public List<string> ErrorMessages { get; set; }
        /// <summary>
        /// Error Message
        /// </summary>
        public List<string> ErrorMessages { get; protected set; }
        #endregion /public List<string> ErrorMessages { get; set; }

        #region public bool Succeeded { get; }
        /// <summary>
        /// Id
        /// </summary>
        public bool Succeeded { get; protected set; }
        #endregion /public bool Succeeded { get; }

        #region public DateTime DateTime { get; protected set; }
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime DateTime { get; protected set; }
        #endregion /public DateTime DateTime { get; protected set; }

        #region public string Version { get; }
        /// <summary>
        /// DateTime
        /// </summary>
        public string Version { get; }
        #endregion /public string Version { get; }

        #endregion /Properties

        #region Methods

        #region public void AddErrorMessage(string message)
        /// <summary>
        /// Add Error Message To response
        /// </summary>
        /// <param name="message">Error Message</param>
        public void AddErrorMessage(string message)
        {
            message =
                ApiService.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (ErrorMessages.Contains(message))
            {
                return;
            }

            ErrorMessages.Add(message);
        }
        #endregion public void AddErrorMessage(string message)

        #region public void AddHiddenMessage(string message)
        /// <summary>
        /// Add Hidden Message To response
        /// </summary>
        /// <param name="message">Hidden Message</param>
        public void AddHiddenMessage(string message)
        {
            message =
                ApiService.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (HiddenMessages.Contains(message))
            {
                return;
            }

            HiddenMessages.Add(message);
        }
        #endregion public void AddHiddenMessage(string message)

        #region public void AddInformationMessage(string message)
        /// <summary>
        /// Add Hidden Message To response
        /// </summary>
        /// <param name="message">Hidden Message</param>
        public void AddInformationMessage(string message)
        {
            message =
                  ApiService.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (InformationMessages.Contains(message))
            {
                return;
            }

            InformationMessages.Add(message);
        }
        #endregion public void AddInformationMessage(string message)

        #region public void ClearAllMessages()
        /// <summary>
        /// When call this method all message list cleared
        /// </summary>
        public void ClearAllMessages()
        {
            ClearErrorMessages();
            ClearHiddenMessages();
            ClearInformationMessages();
        }
        #endregion public void ClearAllMessages()

        #region public void ClearNonHiddenMessages()
        /// <summary>
        /// When call this method clear non hidden message 
        /// </summary>
        public void ClearNonHiddenMessages()
        {
            ClearErrorMessages();
            ClearInformationMessages();
        }
        #endregion public void ClearNonHiddenMessages()

        #region public void ClearErrorMessages()
        /// <summary>
        /// When call this method clear all Error Message
        /// </summary>
        public void ClearErrorMessages()
        {
            ErrorMessages.Clear();
        }
        #endregion public void ClearErrorMessages()

        #region public void ClearHiddenMessages()
        /// <summary>
        /// When call this method clear all Hidden Message
        /// </summary>
        public void ClearHiddenMessages()
        {
            HiddenMessages.Clear();
        }
        #endregion public void ClearHiddenMessages()

        #region public void ClearInformationMessages()
        /// <summary>
        /// When call this method clear all Information Message
        /// </summary>
        public void ClearInformationMessages()
        {
            InformationMessages.Clear();
        }
        #endregion public void ClearInformationMessages()

        #region  public void setSuccess()
        /// <summary>
        /// When call this method clear all Information Message
        /// </summary>
        public void setSuccess()
        {
            Succeeded = true;
        }
        #endregion  public void setSuccess()
        
        #endregion /Methods
    }
}
