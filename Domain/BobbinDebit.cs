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
        public int BobbinDebitId { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int AmountUsed { get; set; }
        [ForeignKey("Workplace")]
        public int WorkplaceId { get; set; }
        [ForeignKey("Bobbin")]
        public int BobbinId { get; set; }

        public virtual Bobbin Bobbin { get; set; }
        public virtual Workplace Workplace { get; set; }
    }
}
