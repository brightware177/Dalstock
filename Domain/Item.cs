using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(45)]
        [Display(Name = "Materiaalnummer")]
        public string ItemId { get; set; }
        [Required]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Aantal")]
        public int Amount { get; set; }
        public string Image { get; set; }
    }
}
