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
        [Display(Name = "Netwerknummer")]
        public string WorkplaceId { get; set; }
        [Required(ErrorMessage = "Gelieve een adres in te voeren")]
        [Display(Name = "Straat")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Gelieve een postcode te selecteren")]
        [ForeignKey("City")]
        [Display(Name = "Stad")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Gelieve een infragebied te selecteren")]
        [ForeignKey("Infrastructure")]
        [Display(Name = "Infra-gebied")]
        public int InfrastructureId { get; set; }

        public virtual ICollection<Debit> Debits { get; set; }
        public virtual ICollection<BobbinDebit> BobbinDebits { get; set; }
        public virtual City City { get; set; }
        public virtual Infrastructure Infrastructure { get; set; }
    }
}
