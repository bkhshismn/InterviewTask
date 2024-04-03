using PublicService;

namespace ViewModels.General
{
    public class LoginViewModel : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="succeeded">succeeded</param>
        public LoginViewModel() : base()
        {
        }
        #endregion /Constructor

        #region Properties

        #region public string Username { get; protected set; }
        /// <summary>
        /// Username
        /// </summary>
        public string? Username { get; set; }
        #endregion /public string Username { get; protected set; }

        #region public string Password { get; protected set; }
        /// <summary>
        /// Password
        /// </summary>
        public string? Password { get; set; }
        #endregion /public string Password { get; protected set; }

        #endregion /Properties
    }
}
