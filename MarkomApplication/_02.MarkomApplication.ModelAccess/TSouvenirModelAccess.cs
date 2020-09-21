using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.MarkomApplication.ModelView;
using _04.MarkomApplication.ModelData;


namespace _02.MarkomApplication.ModelAccess
{
    public class TSouvenirModelAccess
    {
        public static string Message;
        public static string DataKosong;
        public static List<TSouvenirModelView> GetListAll(string searchCode, string SearchReceived, DateTime? searchReceivedDate, DateTime? searchCreatedDate, string searchCreated)
        {
            List<TSouvenirModelView> listData = new List<TSouvenirModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listData = (from a in db.t_souvenir
                            join b in db.m_employee
                            on a.request_by equals b.id
                            where a.is_delete == false
                            select new TSouvenirModelView
                            {
                                id = a.id,
                                code = a.code,
                                receivedBy = a.received_by,
                                received = b.first_name + " " + b.last_name,
                                receivedDate = a.received_date,
                                isDelete = a.is_delete,
                                createdBy = a.created_by,
                                created = b.created_by,
                                createdDate = a.created_date,
                            }).ToList();
            }
            if (!string.IsNullOrEmpty(searchCode))
            {
                listData = listData.Where(x => x.code == searchCode).ToList();
            }
            if (!string.IsNullOrEmpty(SearchReceived))
            {
                listData = listData.Where(x => x.received == SearchReceived).ToList();
            }
            if (searchReceivedDate != null)
            {
                listData = listData.Where(x => x.receivedDate.Value.ToString("dd MMMM yyyy") == searchReceivedDate.Value.ToString("dd MMMM yyyy")).ToList();
            }
            if (searchCreatedDate != null)
            {
                listData = listData.Where(x => x.createdDate.Value.ToString("dd MMMM yyyy") == searchCreatedDate.Value.ToString("dd MMMM yyyy")).ToList();
            }
            if (!string.IsNullOrEmpty(searchCreated))
            {
                listData = listData.Where(x => x.created == searchCreated).ToList();
            }
            if (listData.Count == 0)
            {
                DataKosong = "Data tidak ditemukan";
            }
            else
            {
                DataKosong = "";
            }

            return listData;
        }

        public static List<EmployeeModelView> receivedName()
        {
            List<EmployeeModelView> listNote = new List<EmployeeModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listNote = (from a in db.m_employee
                            where a.is_delete == false
                            select new EmployeeModelView
                            {
                                id = a.id,
                                fullName = a.first_name + " " + a.last_name
                            }).ToList();
            }
            return listNote;
        }

        public static bool Insert(TSouvenirModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    t_souvenir a = new t_souvenir();
                    a.type = "additional";
                    a.code = paramModel.code;
                    a.received_by = paramModel.receivedBy;
                    a.received_date = paramModel.receivedDate;
                    a.request_by = paramModel.receivedBy.Value;
                    a.note = paramModel.note;
                    a.is_delete = paramModel.isDelete;
                    a.created_by = paramModel.createdBy;
                    a.created_date = paramModel.createdDate;

                    db.t_souvenir.Add(a);
                    db.SaveChanges();
                }
            }
            catch (Exception hasError)
            {
                result = false;
                if (hasError.Message.ToLower().Contains("inner exception"))
                {
                    Message = hasError.InnerException.InnerException.Message;
                }
                else
                {
                    Message = hasError.Message;
                }
            }
            return result;
        }















    }
}
