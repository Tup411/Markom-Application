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
    public class TSouvenirController : Controller
    {
        // GET: TSouvenir
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchCode, string SearchReceived, DateTime? searchReceivedDate, DateTime? searchCreatedDate, string searchCreated)
        {
            List<TSouvenirModelView> List = new List<TSouvenirModelView>();
            List = TSouvenirModelAccess.GetListAll(searchCode, SearchReceived, searchReceivedDate, searchCreatedDate, searchCreated);
            ViewBag.DataKosong = TSouvenirModelAccess.DataKosong;
            return PartialView("List", List);
        }

        public ActionResult Create()
        {
            ViewBag.receivedName = new SelectList(TSouvenirModelAccess.receivedName(), "id", "fullName");
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(TSouvenirModelView paramModel)
        {
            try
            {
                TSouvenirModelAccess.Message = string.Empty;

                paramModel.createdBy = 1;
                paramModel.createdDate = DateTime.Now;
                paramModel.isDelete = false;

                if (null == paramModel.receivedBy || null == paramModel.receivedDate)
                {
                    TSouvenirModelAccess.Message = "Anda belum memasukan semua data. Silahkan ulangi kembali";
                }

                if (string.IsNullOrEmpty(TSouvenirModelAccess.Message))
                {
                    //Create auto generate transaction stock code
                    using (var db = new Db_MarkomEntities())
                    {
                        string codeDate = paramModel.receivedDate.Value.ToString("ddMMyy");
                        string nol = "";
                        t_souvenir cek = db.t_souvenir.OrderByDescending(x => x.code).First();
                        int simpan = int.Parse(cek.code.Substring(10));
                        simpan++;
                        for (int i = simpan.ToString().Length; i < 5; i++)
                        {
                            nol = nol + "0";
                        }
                        paramModel.code = "TRSV" + codeDate + nol + simpan;
                    }

                    return Json(new
                    {
                        success = TSouvenirModelAccess.Insert(paramModel),
                        message = paramModel.code
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = TSouvenirModelAccess.Message },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception hasError)
            {
                return Json(new { success = false, message = hasError.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }
































    }
}