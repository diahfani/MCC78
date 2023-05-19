using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model;

public abstract class BaseEntity
{
    // buat nambahin key 
    [Key]
    // column buat nentuin nama kolom database
    [Column("guid")]
    public Guid Guid { get; set; }
    [Column("created_date")]
    public DateTime? CreatedDate { get; set; }
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}
