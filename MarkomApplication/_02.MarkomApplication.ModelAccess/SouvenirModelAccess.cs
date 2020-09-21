using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.MarkomApplication.ModelView;
using _04.MarkomApplication.ModelData;


namespace _02.MarkomApplication.ModelAccess
{
    public class SouvenirModelAccess
    {
        public static string Message;
        public static string DataKosong;
        public static List<SouvenirModelView> GetListAll(string searchName, string searchCode, Int32 searchUnitName, DateTime? searchDate, string searchCreatedSouvenir)
        {
            List<SouvenirModelView> listData = new List<SouvenirModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listData = (from a in db.m_souvenir
                            join b in db.m_unit
                            on a.m_unit_id equals b.id
                            where a.is_delete == false
                            join c in db.t_souvenir_item.Select(o => new { o.m_souvenir_id}).Distinct()
                            on a.id equals c.m_souvenir_id into newgroup
                            from d in newgroup.DefaultIfEmpty()
                            select new SouvenirModelView
                            {
                                id = a.id,
                                code = a.code,
                                name = a.name,
                                nameUnit = b.name,
                                description = a.description,
                                mUnitId = a.m_unit_id,
                                isDelete = a.is_delete,
                                deleteSouvenir = d.m_souvenir_id.ToString() == null ? null : d.m_souvenir_id.ToString(),
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
            if (searchUnitName != 0)
            {
                listData = listData.Where(x => x.mUnitId == searchUnitName).ToList();
            }
            if (searchDate != null)
            {
                listData = listData.Where(x => x.createdDate.ToString("dd MMMM yyyy") == searchDate.Value.ToString("dd MMMM yyyy")).ToList();
            }
            if (!string.IsNullOrEmpty(searchCreatedSouvenir))
            {
                listData = listData.Where(x => x.createdBy == searchCreatedSouvenir).ToList();
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

        public static SouvenirModelView GetDetailById(int Id)
        {
            SouvenirModelView result = new SouvenirModelView();
            using (var db = new Db_MarkomEntities())
            {
                result = (from a in db.m_souvenir
                          where a.id == Id
                          select new SouvenirModelView
                          {
                              id = a.id,
                              code = a.code,
                              name = a.name,
                              description = a.description,
                              mUnitId = a.m_unit_id,
                              isDelete = a.is_delete,
                              createdBy = a.created_by,
                              createdDate = a.created_date,
                              updatedBy = a.updated_by,
                              updatedDate = a.updated_date
                          }).FirstOrDefault();
            }
            return result;
        }

        public static List<UnitModelView> UnitName()
        {
            List<UnitModelView> listName = new List<UnitModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listName = (from a in db.m_unit
                            where a.is_delete == false
                            select new UnitModelView
                            {
                                id = a.id,
                                name = a.name

                            }).ToList();
            }
            return listName;
        }

        public static bool Insert(SouvenirModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_souvenir a = new m_souvenir();
                    a.code = paramModel.code;
                    a.name = paramModel.name;
                    a.m_unit_id = paramModel.mUnitId;
                    a.description = paramModel.description;
                    a.is_delete = paramModel.isDelete;
                    a.created_by = paramModel.createdBy;
                    a.created_date = paramModel.createdDate;

                    db.m_souvenir.Add(a);
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

        public static bool Update(SouvenirModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_souvenir a = db.m_souvenir.Where(o => o.id == paramModel.id).FirstOrDefault();
                    if (a != null)
                    {
                        a.m_unit_id = paramModel.mUnitId;
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

        public static bool Delete(SouvenirModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_souvenir a = db.m_souvenir.Where(o => o.id == paramModel.id).FirstOrDefault();
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
