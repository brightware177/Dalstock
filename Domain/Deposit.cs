using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Deposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepositId { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Deposited_By_Staff")]
        public int Deposited_By { get; set; }

        public virtual Staff Deposited_By_Staff { get; set; }
        public virtual Item Item { get; set; }
    }
}
