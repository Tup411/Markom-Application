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
    public class SouvenirController : Controller
    {
        // GET: Souvenir
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchName, string searchCode, Int32 searchUnitName, DateTime? searchDate, string searchCreatedSouvenir)
        {
            List<SouvenirModelView> List = new List<SouvenirModelView>();
            List = SouvenirModelAccess.GetListAll(searchName, searchCode, searchUnitName, searchDate, searchCreatedSouvenir);
            ViewBag.UnitName = new SelectList(List, "mUnitId", "nameUnit");
            ViewBag.DataKosong = SouvenirModelAccess.DataKosong;
            return PartialView("List", List);
        }

        public ActionResult Create()
        {
            ViewBag.UnitName = new SelectList(SouvenirModelAccess.UnitName(), "id", "name");
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(SouvenirModelView paramModel)
        {
            try
            {
                SouvenirModelAccess.Message = string.Empty;

                //Create auto generate souvenir_code
                using (var db = new Db_MarkomEntities())
                {
                    string nol = "";
                    m_souvenir cek = db.m_souvenir.OrderByDescending(x => x.code).First();
                    int simpan = int.Parse(cek.code.Substring(3));
                    simpan++;
                    for (int i = simpan.ToString().Length; i < 4; i++)
                    {
                        nol = nol + "0";
                    }
                    paramModel.code = "SV" + nol + simpan;
                }

                paramModel.createdBy = "Administrator";
                paramModel.createdDate = DateTime.Now;
                paramModel.isDelete = false;

                if (null == paramModel.name || "0" == (paramModel.mUnitId).ToString())
                {
                    SouvenirModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(SouvenirModelAccess.Message))
                {
                    return Json(new
                    {
                        success = SouvenirModelAccess.Insert(paramModel),
                        message = paramModel.code
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = SouvenirModelAccess.Message },
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
            ViewBag.UnitName = new SelectList(SouvenirModelAccess.UnitName(), "id", "name");
            return PartialView(SouvenirModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Edit(SouvenirModelView paramModel)
        {
            try
            {
                SouvenirModelAccess.Message = string.Empty;

                paramModel.updatedBy = "Zoni";
                paramModel.updatedDate = DateTime.Now;

                if (null == paramModel.name || "0" == (paramModel.mUnitId).ToString())
                {
                    SouvenirModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }
                if (string.IsNullOrEmpty(SouvenirModelAccess.Message))
                {
                    return Json(new
                    {
                        success = SouvenirModelAccess.Update(paramModel),
                        message = SouvenirModelAccess.Message
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = SouvenirModelAccess.Message },
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
            ViewBag.UnitName = new SelectList(SouvenirModelAccess.UnitName(), "id", "name");
            return PartialView(SouvenirModelAccess.GetDetailById(paramId));
        }

        public ActionResult Delete(Int32 paramId)
        {
            return PartialView(SouvenirModelAccess.GetDetailById(paramId));
        }

        [HttpPost]
        public ActionResult Delete(SouvenirModelView paramModel)
        {
            try
            {
                return Json(new
                {
                    success = SouvenirModelAccess.Delete(paramModel),
                    message = SouvenirModelAccess.Message
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