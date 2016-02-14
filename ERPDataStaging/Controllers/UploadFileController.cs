using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ERPDataStaging.Models;

namespace ERPDataStaging.Controllers.Tests
{
    public class UploadFileController : Controller
    {
        private string gotStringFromUI;

        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult DoFileUpload(HttpPostedFileBase file)
        //
        [HttpPost]
        public ActionResult DoFileUpload(HttpPostedFileBase file)
        {
            //Undone: show error msg to user!!
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 20; //20 MB
                    string[] AllowedFileExtensions = new string[] { ".csv", ".txt" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please upload file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
                        file.SaveAs(path); //Note: overwrite file with the same name
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";

                        //Todo: caution, overflow! long file, file.InputStream.Length is a long int
                        //var b = new BinaryReader(file.InputStream);
                        //byte[] binData = b.ReadBytes((int)file.InputStream.Length);
                        //string s = new StreamReader(file.InputStream).ReadToEnd();
                        var pH = new ProductsHandler();
                        pH.SetProductsDB(
                            pH.GetProductsStream(file.InputStream)
                            );

                        return RedirectToAction("Index", "EditProducts");
                    }
                }
            }
            return View();
        }
    }
}