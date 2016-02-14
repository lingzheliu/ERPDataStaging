using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPDataStaging.DAL;

using Microsoft.VisualBasic.FileIO;

namespace ERPDataStaging.Models
{

    public class ProductsHandler
    {

        // Todo: 1. bulk update, 2. updateDB success?
        public void SetProductsDB(List<Product> products)
        {            
            var context = new ERPDataStagingDBContext();
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        //
        // read data from DB. this is just a practising excercise for EF.
        //
        public List<Product> GetProductsDB ()
        {
            var context = new ERPDataStagingDBContext();
            List<Product> products = context.Products.ToList();
            return products;
        }

        public List<Product> GetProductsStream(Stream stream)
        {
            var products = new List<Product>();

            var parser = new TextFieldParser(stream);
            //parser.CommentTokens = new string[] { "#" };
            //parser.SetDelimiters(new string[] { "," });
            parser.SetDelimiters(new string[] { "," }); //Todo: allow user to specify delimiters;
            parser.HasFieldsEnclosedInQuotes = true; // Note: (todo consultant) advice user to upload csv which use quotes for prijs with comma as decimal points
            parser.TrimWhiteSpace = true;

            // Skip over header line.
            string[] fields = parser.ReadFields();
            int fieldCount = fields.Length;
            //verify the csv has the same fields as Product class. empty end field is also regarded as a field (extra comma at EoL in csv. e.g. ...,maat,kleur,)
            if (fieldCount != 10) //Todo: map csv fields to Product fields
            {
                return products; // do nothing, if fieldCount incorrect 
            }
            //Todo: throw exception if fieldCount incorrect 

            while (!parser.EndOfData)
            {
                // Todo : return error line(s).
                //Todo: try parser.ReadFields
                fields = parser.ReadFields();
                
                if (fields.Length != 10) // Todo : find the number of fields of Product class
                {
                    continue; // do nothing, if fieldCount incorrect 
                    //string errorText = "(" + parser.LineNumber.ToString() + ")" + "This recordLine has incorrect number of fields." + fields[0];
                    //throw new Exception(errorText);
                }

                //Data Fields from csv: Key Artikelcode Kleurcode Omschrijving    Prijs ActiePrijs  Levertijd q1  maat kleur
                // Todo: find the right value of null numerics
                // Todo: deal with currency marks, e.g. € 8.00
                decimal temp;                decimal? tempPrijs = decimal.TryParse(fields[4], out temp) ? temp : default(decimal?); 
                decimal? tempActiePrijs = decimal.TryParse(fields[5], out temp) ? temp : default(decimal?);
                int tempI;
                int? tempmaat = int.TryParse(fields[8], out tempI) ? tempI : default(int?);

                products.Add(new Product()
                {
                    Key = fields[0],
                    Artikelcode = fields[1],
                    Kleurcode = fields[2],
                    Omschrijving = fields[3],
                    Prijs = tempPrijs.GetValueOrDefault(0),                 // note: default null prics is 0
                    ActiePrijs = tempActiePrijs.GetValueOrDefault(0),       // note: default null prics is 0
                    Levertijd = fields[6],
                    q1 = fields[7],
                    maat = tempmaat.GetValueOrDefault(-1),                  // note: default null maat is -1
                    kleur = fields[9]
                });
            }
            return products;
        }
    }

    
}