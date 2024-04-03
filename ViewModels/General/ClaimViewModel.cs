using PublicService;

namespace ViewModels.General
{
    public class ClaimViewModel : object
    {
        #region Constructor
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="succeeded">succeeded</param>
        public ClaimViewModel(string permissionName, int accessLevel, Guid rolePermissionId) : base()
        {
            RolePermissionId = rolePermissionId;
            PermissionName = permissionName;
            AccessLevel = accessLevel;
        }
        #endregion /Constructor

        #region Properties

        #region public Guid RolePermissionId{ get; set; }
        /// <summary>
        /// RolePermissionId
        /// </summary>
        public Guid RolePermissionId { get; set; }
        #endregion /public Guid RolePermissionId{ get; set; }

        #region public string PermissionName{ get; set; }
        /// <summary>
        /// PermissionName
        /// </summary>
        public string PermissionName { get; set; }
        #endregion /public string PermissionName{ get; set; }

        #region public int AccessLevel{ get; set; }
        /// <summary>
        /// AccessLevel
        /// </summary>
        public int AccessLevel { get; set; }
        #endregion /public int AccessLevel{ get; set; }

        #endregion /Properties
    }
}
