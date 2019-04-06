using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ApplicationUserId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Debit> Debited_debits { get; set; }
        [JsonIgnore]
        public virtual ICollection<Debit> Approved_debits { get; set; }
    }
}
