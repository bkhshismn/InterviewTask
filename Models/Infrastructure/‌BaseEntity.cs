using System.ComponentModel.DataAnnotations;

namespace Models.Infrastructure;

/// <summary>
/// Base Entity
/// </summary>
public class BaseEntity : object
{
    #region Constructor
    /// <summary>
    /// Constractor
    /// </summary>
    public BaseEntity(Guid ownerId) : base()
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        DateAdded = DateTime.Now;
        LastUpdatedBy = ownerId;
        LastUpdateDate = DateAdded;
        IsDeleted = false;
    }
    #endregion /Constructor

    #region Properties

    #region public Guid Id { get; set; }
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    #endregion /public Guid Id { get; set; }

    #region public Guid OwnerId { get; set; }
    /// <summary>
    /// Creator Id
    /// </summary>
    public Guid OwnerId { get; set; }
    #endregion /public Guid OwnerId { get; set; }

    #region public DateTime DateAdded { get; set; }
    /// <summary>
    /// Date Added
    /// </summary>
    public DateTime DateAdded { get; set; }
    #endregion /public DateTime DateAdded { get; set; }

    #region public Guid LastUpdatedBy { get; set; }
    /// <summary>
    /// Last Updater User Id
    /// </summary>
    public Guid LastUpdatedBy { get; set; }
    #endregion /public Guid LastUpdatedBy { get; set; }

    #region public DateTime LastUpdateDate { get; set; }
    /// <summary>
    /// Last Update datetime
    /// </summary>
    public DateTime LastUpdateDate { get; set; }
    #endregion /public DateTime LastUpdateDate { get; set; }

    #region public bool IsDeleted { get; set; }
    /// <summary>
    /// Last Update datetime
    /// </summary>
    public bool IsDeleted { get; set; }
    #endregion /public bool IsDeleted { get; set; }

    #region public string? Name { get; set; }
    /// <summary>
    /// Field Name
    /// </summary>
    public string? Name { get; set; }
    #endregion /public string? Name { get; set; }

    #region public string? PersianName { get; set; }
    /// <summary>
    /// Field PersianName
    /// </summary>
    public string? PersianName { get; set; }
    #endregion /public string? PersianName { get; set; }

    #endregion /Properties
}
