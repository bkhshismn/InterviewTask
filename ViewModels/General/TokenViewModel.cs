using PublicService;

namespace ViewModels.General
{
    public class TokenViewModel : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="succeeded">succeeded</param>
        public TokenViewModel(string token) : base()
        {
            Token = token;
        }
        #endregion /Constructor

        #region Properties

        #region public string Token{ get; protected set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; protected set; }
        #endregion /public string Token{ get; protected set; }

        #endregion /Properties
    }
}
