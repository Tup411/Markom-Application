using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _02.MarkomApplication.ModelAccess;
using _03.MarkomApplication.ModelView;
using _04.MarkomApplication.ModelData;

namespace _01.MarkomApplication.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchName, string searchCode, string searchDescription, DateTime? searchDate, string searchCreatedProduct)
        {
            List<ProductModelView> List = new List<ProductModelView>();
            List = ProductModelAccess.GetListAll(searchName, searchCode, searchDescription, searchDate, searchCreatedProduct);
            ViewBag.DataKosong = ProductModelAccess.DataKosong;
            return PartialView("List", List);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(ProductModelView paramModel)
        {
            try
            {
                ProductModelAccess.Message = string.Empty;

                //Create auto generate product_code
                using (var db = new Db_MarkomEntities())
                {
                    string nol = "";
                    m_product cek = db.m_product.OrderByDescending(x => x.code).First();
                    int simpan = int.Parse(cek.code.Substring(3));
                    simpan++;
                    for (int i = simpan.ToString().Length; i < 4; i++)
                    {
                        nol = nol + "0";
                    }
                    paramModel.code = "PR" + nol + simpan;
                }

                paramModel.createdBy = "Administrator";
                paramModel.createdDate = DateTime.Now;
                paramModel.isDelete = false;

                if (null == paramModel.name)
                {
                    ProductModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(ProductModelAccess.Message))
                {
                    return Json(new
                    {
                        success = ProductModelAccess.Insert(paramModel),
                        message = paramModel.code
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = ProductModelAccess.Message },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception hasError)
            {
                return Json(new { success = false, message = hasError.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(Int32 paramId)
        {
            return PartialView(ProductModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Edit(ProductModelView paramModel)
        {
            try
            {
                ProductModelAccess.Message = string.Empty;

                paramModel.updatedBy = "Zoni";
                paramModel.updatedDate = DateTime.Now;

                if (null == paramModel.name)
                {
                    ProductModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(ProductModelAccess.Message))
                {
                    return Json(new
                    {
                        success = ProductModelAccess.Update(paramModel),
                        message = ProductModelAccess.Message
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = ProductModelAccess.Message },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception hasError)
            {
                return Json(new { success = false, message = hasError.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail(Int32 paramId)
        {
            return PartialView(ProductModelAccess.GetDetailById(paramId));
        }

        public ActionResult Delete(Int32 paramId)
        {
            return PartialView(ProductModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Delete(ProductModelView paramModel)
        {
            try
            {
                return Json(new
                {
                    success = ProductModelAccess.Delete(paramModel),
                    message = ProductModelAccess.Message
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception hasError)
            {
                return Json(new { success = false, message = hasError.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }









    }
}