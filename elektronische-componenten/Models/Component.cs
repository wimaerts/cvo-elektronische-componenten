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
        public Categorie Categorie { get; set; }

        [Required]
        public string Naam { get; set; }
        public string Datasheet { get; set; }
        public int Aantal { get; set; }
        public decimal Aankoopprijs { get; set; }
    }
}