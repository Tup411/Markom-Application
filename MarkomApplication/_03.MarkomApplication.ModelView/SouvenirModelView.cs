using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.MarkomApplication.ModelView
{
    public class SouvenirModelView
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string nameUnit { get; set; }
        public string description { get; set; }
        public long mUnitId { get; set; }
        public bool isDelete { get; set; }
        public string deleteSouvenir { get; set; }
        public string createdBy { get; set; }
        public System.DateTime createdDate { get; set; }
        public string tanggal => createdDate.ToString("dd MMMM yyyy");
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
