using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using ERPDataStaging.Models;
using ERPDataStaging.DAL;

namespace ERPDataStaging.Controllers.Tests
{
    public class SeedDataController : Controller
    {
        private List<Product> products;
        private ERPDataStagingDBContext db = new ERPDataStagingDBContext();

        public SeedDataController()
        {
            products = new List<Product>();
            //Data Fields from csv: 
            //Key Artikelcode Kleurcode Omschrijving    Prijs ActiePrijs  Levertijd q1  maat kleur
            //1560074 15  Jeans Willy Boys Mexx	€ 5.00      1 - 3 werkdagen boy 74  blauw
            //1560080 15  Jeans Willy Boys Mexx	€ 5.00      1 - 3 werkdagen boy 80  blauw
            //0822801A20050   0822801A Short Billy & Bobble    Mexx    € 5.00      1 - 3 werkdagen NOINDEX 50  rood
            //0822801A20056   0822801A Short Billy & Bobble    Mexx    € 5.00      1 - 3 werkdagen NOINDEX 56  rood
            //21100116	21	jacket	Gaastra	€ 24.00 		1-3 werkdagen	boy	116	wit
            //1279074	12	Steve Irwin	Vingino	€ 5.00 		1-3 werkdagen	baby	74	beige
            //00000002zwar/l3292	2	broek	Gaastra	€ 8.00 		1-3 werkdagen	baby	92	zwart
            //00000002groe110 2   broek Gaastra	€ 8.00      1 - 3 werkdagen baby    110 groen

            // Todo: check: ProductID auto generate in DB?
            products.Add(new Product
            {
                Key = "1560074",
                Artikelcode = "15",
                Kleurcode = "Jeans Willy Boys",
                Omschrijving = "Mexx",
                Prijs = 5.00M,
                Levertijd = "1 - 3 werkdagen",
                q1 = "boy",
                maat = 74,
                kleur = "blauw"
            });
            products.Add(new Product
            {
                Key = "0822801A20050",
                Artikelcode = "0822801A",
                Kleurcode = "Short Billy & Bobble",
                Omschrijving = "Mexx",
                Prijs = 5.00M,
                Levertijd = "1 - 3 werkdagen",
                q1 = "NOINDEX",
                maat = 50,
                kleur = "rood"
            });
            products.Add(new Product
            {
                Key = "00000002zwar/l3292",
                Artikelcode = "2",
                Kleurcode = "broek",
                Omschrijving = "Gaastra",
                Prijs = 8.00M,
                Levertijd = "1 - 3 werkdagen",
                q1 = "baby",
                maat = 92,
                kleur = "zwart"

            });
        }

        // GET: SeedData
        public ActionResult Index()
        {
            return View(products);
        }

        public ActionResult InsertMemData ()
        {
            if (ModelState.IsValid)
            {
                //this.products.ForEach(p => db.Products.Add(p));
                //db.SaveChanges();
                var pH = new ProductsHandler();
                pH.SetProductsDB(products);
                return RedirectToAction("Index", "EditProducts");
            }
            else
                return View("Error"); // Todo: use error view to show error msg.
        }
    }
}