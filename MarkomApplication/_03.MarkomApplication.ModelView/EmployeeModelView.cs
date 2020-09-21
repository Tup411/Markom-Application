using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.MarkomApplication.ModelView
{
    public class EmployeeModelView
    {
        public long id { get; set; }
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public Nullable<long> mCompanyId { get; set; }
        public string email { get; set; }
        public bool isDelete { get; set; }
        public string createdBy { get; set; }
        public System.DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }

    }
}
