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
    public class UnitController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchName, string searchCode, DateTime? searchDate, string searchCreatedUnit)
        {
            List<UnitModelView> List = new List<UnitModelView>();
            List = UnitModelAccess.GetListAll(searchName, searchCode, searchDate, searchCreatedUnit);
            ViewBag.UnitCode = new SelectList(List, "code", "code");
            ViewBag.UnitName = new SelectList(List, "name", "name");
            ViewBag.DataKosong = UnitModelAccess.DataKosong;
            return PartialView("List", List);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(UnitModelView paramModel)
        {
            try
            {
                UnitModelAccess.Message = string.Empty;

                //Create auto generate unit_code
                using (var db = new Db_MarkomEntities())
                {
                    string nol = "";
                    m_unit cek = db.m_unit.OrderByDescending(x => x.code).First();
                    int simpan = int.Parse(cek.code.Substring(3));
                    simpan++;
                    for (int i = simpan.ToString().Length; i < 4; i++)
                    {
                        nol = nol + "0";
                    }
                    paramModel.code = "UN" + nol + simpan;
                }

                paramModel.createdBy = "Administrator";
                paramModel.createdDate = DateTime.Now;
                paramModel.isDelete = false;

                if (null == paramModel.name)
                {
                    UnitModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(UnitModelAccess.Message))
                {
                    return Json(new
                    {
                        success = UnitModelAccess.Insert(paramModel),
                        message = paramModel.code
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = UnitModelAccess.Message },
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
            return PartialView(UnitModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Edit(UnitModelView paramModel)
        {
            try
            {
                UnitModelAccess.Message = string.Empty;

                paramModel.updatedBy = "Zoni";
                paramModel.updatedDate = DateTime.Now;

                if (null == paramModel.name)
                {
                    UnitModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(UnitModelAccess.Message))
                {
                    return Json(new
                    {
                        success = UnitModelAccess.Update(paramModel),
                        message = UnitModelAccess.Message
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = UnitModelAccess.Message },
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
            return PartialView(UnitModelAccess.GetDetailById(paramId));
        }

        public ActionResult Delete(Int32 paramId)
        {
            return PartialView(UnitModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Delete(UnitModelView paramModel)
        {
            try
            {
                return Json(new
                {
                    success = UnitModelAccess.Delete(paramModel),
                    message = UnitModelAccess.Message
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