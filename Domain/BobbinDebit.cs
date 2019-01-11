using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BobbinDebit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Bobijnnummer")]
        public int BobbinDebitId { get; set; }
        [Required]
        [Display(Name = "Beginindex")]
        public int StartIndex { get; set; }
        [Required]
        [Display(Name = "Eindindex")]
        public int EndIndex { get; set; }
        [Required]
        [Display(Name = "Verbruikte kabel (m)")]
        public int AmountUsed { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Verbruikdatum")]
        public DateTime Date { get; set; }
        [ForeignKey("Workplace")]
        public int WorkplaceId { get; set; }
        [ForeignKey("Bobbin")]
        public int BobbinId { get; set; }

        public virtual Bobbin Bobbin { get; set; }
        public virtual Workplace Workplace { get; set; }
    }
}
