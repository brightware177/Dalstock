using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MeasuringState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(45)]
        [Display(Name = "Meetstaatnummer")]
        public string MeasuringStateId { get; set; }
        [Display(Name = "Prijs")]
        public double Price { get; set; }
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Einddatum")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Aantal dagen")]
        public int Days { get; set; }
        [Display(Name = "Ontvangstdatum")]
        public DateTime RecieveDate { get; set; }
        
        public virtual Workplace Workplace { get; set; }
        [ForeignKey("Workplace")]
        [Display(Name = "Werf")]
        public int WorkplaceId { get; set; }

        public virtual Team Team { get; set; }
        [ForeignKey("Team")]
        [Display(Name = "Ploeg")]
        public string TeamId { get; set; }
    }
}
