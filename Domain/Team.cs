using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Team
    {
        [Key]
        public string TeamId { get; set; }
        public virtual ICollection<Staff> Members { get; set; }
        public virtual MemberType MemberType { get; set; }

    }
}
