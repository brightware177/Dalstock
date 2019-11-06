using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Price { get; set; }

        public virtual MeasuringState MeasuringState { get; set; }
        [ForeignKey("MeasuringState")]
        [Display(Name = "Meetstaat")]
        public int MeasuringStateId { get; set; }

    }
}
