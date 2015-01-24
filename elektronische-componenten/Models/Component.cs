using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace elektronische_componenten.Models
{
    public class Component
    {
        public int Id { get; set; }
        public virtual Categorie Categorie { get; set; }

        [Required]
        public string Naam { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Product link")]
        public string Datasheet { get; set; }  
      
        public int Aantal { get; set; }
        public double Aankoopprijs { get; set; }

        public Component()
        {

        }

        public Component(string naam, Categorie categorie, string datasheet, int aantal, double aankoopprijs)
        {
            this.Naam = naam;
            this.Categorie = categorie;
            this.Datasheet = datasheet;
            this.Aantal = aantal;
            this.Aankoopprijs = aankoopprijs;
        }
    }
}