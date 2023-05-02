using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Shared
{
    public abstract class BaseEntity
    {
        public DateTime CreateTime { get; set; } 
        public DateTime? UpdateTime { get; set; } 
        public DateTime? DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
