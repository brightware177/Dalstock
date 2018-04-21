using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Debit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DebitId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Workplace")]
        public int WorkplaceId { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [ForeignKey("Debited_By_Staff")]
        public int Debited_By_Staff_Id { get; set; }
        [ForeignKey("Approved_By_Staff")]
        public int Approved_By_Staff_Id { get; set; }

        public virtual Item Item { get; set; }
        public virtual Workplace Workplace { get; set; }
        public virtual Staff Debited_By_Staff { get; set; }
        public virtual Staff Approved_By_Staff { get; set; }
        public virtual DebitState DebitState { get; set; }

        public class DebitMapping : EntityTypeConfiguration<Debit>
        {
            public DebitMapping()
            {
                HasRequired(m => m.Debited_By_Staff).WithMany(m => m.Debited_debits);
                HasRequired(m => m.Approved_By_Staff).WithMany(m => m.Approved_debits);
            }
        }
    }
}
