using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14MTZO.DB;

[Table("Tbl_UserAcount")]
public class UserAcountModel
{
    [Key]
    public string? UserAcountId { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public int Pin { get; set; }
    public decimal Balance { get; set; }

    public string? CardNumber { get; set; } = string.Empty;
}
