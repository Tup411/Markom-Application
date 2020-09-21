using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.MarkomApplication.ModelView;
using _04.MarkomApplication.ModelData;

namespace _02.MarkomApplication.ModelAccess
{
    public class UnitModelAccess
    {
        public static string Message;
        public static string DataKosong;
        public static List<UnitModelView> GetListAll(string searchName, string searchCode, DateTime? searchDate, string searchCreatedUnit)
        {
            List<UnitModelView> listData = new List<UnitModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listData = (from a in db.m_unit.Where(c => c.is_delete == false)
                            join b in db.m_souvenir.Select(o => new { o.m_unit_id }).Distinct()
                            on a.id equals b.m_unit_id into newgroup
                            from c in newgroup.DefaultIfEmpty()
                            select new UnitModelView
                            {
                                id = a.id,
                                code = a.code,
                                name = a.name,
                                description = a.description,
                                isDelete = a.is_delete,
                                deleteUnit = c.m_unit_id.ToString() == null ? null : c.m_unit_id.ToString(),
                                createdBy = a.created_by,
                                createdDate = a.created_date,
                                updatedBy = a.updated_by,
                                updatedDate = a.updated_date
                            }).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                listData = listData.Where(x => x.name == searchName).ToList();
            }
            if (!string.IsNullOrEmpty(searchCode))
            {
                listData = listData.Where(x => x.code == searchCode).ToList();
            }
            if (searchDate != null)
            {
                listData = listData.Where(x => x.createdDate.ToString("dd MMMM yyyy") == searchDate.Value.ToString("dd MMMM yyyy")).ToList();
            }
            if (!string.IsNullOrEmpty(searchCreatedUnit))
            {
                listData = listData.Where(x => x.createdBy == searchCreatedUnit).ToList();
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

        public static UnitModelView GetDetailById(int Id)
        {
            UnitModelView result = new UnitModelView();
            using (var db = new Db_MarkomEntities())
            {
                result = (from a in db.m_unit
                          where a.id == Id
                          select new UnitModelView
                          {
                              id = a.id,
                              code = a.code,
                              name = a.name,
                              description = a.description,
                              isDelete = a.is_delete,
                              createdBy = a.created_by,
                              createdDate = a.created_date,
                              updatedBy = a.updated_by,
                              updatedDate = a.updated_date
                          }).FirstOrDefault();
            }
            return result;
        }

        public static bool Insert(UnitModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_unit a = new m_unit();
                    a.code = paramModel.code;
                    a.name = paramModel.name;
                    a.description = paramModel.description;
                    a.is_delete = paramModel.isDelete;
                    a.created_by = paramModel.createdBy;
                    a.created_date = paramModel.createdDate;

                    db.m_unit.Add(a);
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

        public static bool Update(UnitModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_unit a = db.m_unit.Where(o => o.id == paramModel.id).FirstOrDefault();
                    if (a != null)
                    {
                        a.name = paramModel.name;
                        a.description = paramModel.description;
                        a.updated_by = paramModel.updatedBy;
                        a.updated_date = paramModel.updatedDate;

                        db.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
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

        public static bool Delete(UnitModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_unit a = db.m_unit.Where(o => o.id == paramModel.id).FirstOrDefault();
                    if (a != null)
                    {
                        a.is_delete = true;

                        db.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
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
