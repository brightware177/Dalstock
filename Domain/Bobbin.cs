using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Bobbin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(45)]
        public string BobbinId { get; set; }
        [Required]
        public int CableLength { get; set; }
        public bool IsReturned { get; set; }
        [Required]
        public DateTime FetchDate { get; set; }
        [Required]
        public int AmountRemains { get; set; }
        public DateTime ReturnDate { get; set; }
        [Required]
        public virtual CableType CableType { get; set; }
        public virtual ICollection<BobbinDebit> BobbinDebits { get; set; }
        [Required]
        public string FetchLocation { get; set; }
    }
}
