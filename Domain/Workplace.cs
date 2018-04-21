using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("workplace")]
    public class Workplace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Gelieve een dossiernummer in te voeren")]
        [StringLength(45)]
        public string WorkplaceId { get; set; }
        [Required(ErrorMessage = "Gelieve een adres in te voeren")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Gelieve een postcode te selecteren")]
        [ForeignKey("City")]
        public int CityId { get; set; }
        
        public virtual ICollection<Debit> Debits { get; set; }
        public virtual ICollection<BobbinDebit> BobbinDebits { get; set; }
        public virtual City City { get; set; }
    }
}
