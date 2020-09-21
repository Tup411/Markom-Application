using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.MarkomApplication.ModelView
{
    public class TSouvenirModelView
    {
        public long id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public Nullable<long> tEventId { get; set; }
        public long requestBy { get; set; }
        public Nullable<System.DateTime> requestDate { get; set; }
        public Nullable<System.DateTime> requestDueDate { get; set; }
        public Nullable<long> approvedBy { get; set; }
        public Nullable<System.DateTime> approvedDate { get; set; }
        public Nullable<long> receivedBy { get; set; }
        public string received { get; set; }
        public Nullable<System.DateTime> receivedDate { get; set; }
        public string tanggalReceived => receivedDate.Value.ToString("dd MMMM yyyy");

        public Nullable<long> settlementBy { get; set; }
        public Nullable<System.DateTime> settlementDate { get; set; }
        public Nullable<long> settlementApprovedBy { get; set; }
        public Nullable<System.DateTime> settlementApprovedDate { get; set; }
        public Nullable<int> status { get; set; }
        public string note { get; set; }
        public string rejectReason { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public Nullable<long> createdBy { get; set; }
        public string created { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string tanggalCreated => createdDate.Value.ToString("dd MMMM yyyy");
        public Nullable<long> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }






















        }
}
