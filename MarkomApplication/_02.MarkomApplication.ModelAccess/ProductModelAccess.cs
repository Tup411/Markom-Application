using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.MarkomApplication.ModelView;
using _04.MarkomApplication.ModelData;

namespace _02.MarkomApplication.ModelAccess
{
    public class ProductModelAccess
    {
        public static string Message;
        public static string DataKosong;
        public static List<ProductModelView> GetListAll(string searchName, string searchCode, string searchDescription, DateTime? searchDate, string searchCreatedProduct)
        {
            List<ProductModelView> listData = new List<ProductModelView>();
            using (var db = new Db_MarkomEntities())
            {
                listData = (from a in db.m_product
                            where a.is_delete == false
                            select new ProductModelView
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
            if (!string.IsNullOrEmpty(searchDescription))
            {
                listData = listData.Where(x => x.description == searchDescription).ToList();
            }
            if (searchDate != null)
            {
                listData = listData.Where(x => x.createdDate.ToString("dd MMMM yyyy") == searchDate.Value.ToString("dd MMMM yyyy")).ToList();
            }
            if (!string.IsNullOrEmpty(searchCreatedProduct))
            {
                listData = listData.Where(x => x.createdBy == searchCreatedProduct).ToList();
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

        public static ProductModelView GetDetailById(int Id)
        {
            ProductModelView result = new ProductModelView();
            using (var db = new Db_MarkomEntities())
            {
                result = (from a in db.m_product
                          where a.id == Id
                          select new ProductModelView
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

        public static bool Insert(ProductModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_product a = new m_product();
                    a.code = paramModel.code;
                    a.name = paramModel.name;
                    a.description = paramModel.description;
                    a.is_delete = paramModel.isDelete;
                    a.created_by = paramModel.createdBy;
                    a.created_date = paramModel.createdDate;

                    db.m_product.Add(a);
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

        public static bool Update(ProductModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_product a = db.m_product.Where(o => o.id == paramModel.id).FirstOrDefault();
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

        public static bool Delete(ProductModelView paramModel)
        {
            bool result = true;
            try
            {
                using (var db = new Db_MarkomEntities())
                {
                    m_product a = db.m_product.Where(o => o.id == paramModel.id).FirstOrDefault();
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
