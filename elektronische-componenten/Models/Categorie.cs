using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace elektronische_componenten.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        public string Naam { get; set; }

        public Categorie()
        {

        }

        public Categorie(string naam)
        {
            this.Naam = naam;
        }
    }
}