using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPDataStaging.Models
{
    //Data Fields from csv: Key Artikelcode Kleurcode Omschrijving    Prijs ActiePrijs  Levertijd q1  maat kleur
    // Todo: better data model for Kleurcode, Levertijd, q1, kleur
    public class Product
    {
        //EF convention (used annotation instead):  classnameID as the primary key
        [Key]
        public int ProductID { get; set; }
        // Todo: check identity of original key in ERP DB
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Key { get; set; }
        public string Artikelcode { get; set; } //note: this field should be nullable
        public string Kleurcode { get; set; }
        public string Omschrijving { get; set; }
        [Column(TypeName = "Money")] //note: caution, Money type might not suitable for the financial context with requirement of high precision 
        public decimal Prijs { get; set; }
        [Column(TypeName = "Money")]
        public decimal ActiePrijs { get; set; }
        public string Levertijd { get; set; }
        public string q1 { get; set; }
        public int maat { get; set; }
        public string kleur { get; set; }
    }
}