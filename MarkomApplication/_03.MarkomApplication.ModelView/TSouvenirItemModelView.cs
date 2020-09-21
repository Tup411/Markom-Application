using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.MarkomApplication.ModelView
{
    public class TSouvenirItemModelView
    {
        public long id { get; set; }
        public long tSouvenirId { get; set; }
        public long mSouvenirId { get; set; }
        public Nullable<long> qty { get; set; }
        public Nullable<long> qtySettlement { get; set; }
        public string note { get; set; }
        public bool isDelete { get; set; }
        public Nullable<long> createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<long> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }

    }
}
