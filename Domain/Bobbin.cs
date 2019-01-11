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
        [Display(Name = "Bobijnnummer")]
        public string BobbinId { get; set; }

        [Required]
        [Display(Name = "Kabellengte")]
        public int CableLength { get; set; }

        [Display(Name = "Geretourneerd")]
        public bool IsReturned { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Afhaaldatum")]
        public DateTime FetchDate { get; set; }

        [Required]
        [Display(Name = "Resterende kabel")]
        public int AmountRemains { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Retourdatum")]
        public DateTime? ReturnDate { get; set; }

        [ForeignKey("CableType")]
        [Required]
        [Display(Name = "Kabeltype")]
        public int CableTypeId { get; set; }

        public virtual CableType CableType { get; set; }
        public virtual ICollection<BobbinDebit> BobbinDebits { get; set; }

        [Required]
        [Display(Name = "Infragebied")]
        public string FetchLocation { get; set; }
    }
}
