using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPDataStaging.DAL;
using ERPDataStaging.Models;

namespace ERPDataStaging.Controllers.Tests
{
    public class DelTblDataController : Controller
    {
        private ERPDataStagingDBContext db = new ERPDataStagingDBContext();
        // GET: DelTblData
        public ActionResult Index()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Product]");
            return RedirectToAction("Index", "EditProducts"); // Todo: redirect to show current data, add EditProducts ctrl
        }
    }
}