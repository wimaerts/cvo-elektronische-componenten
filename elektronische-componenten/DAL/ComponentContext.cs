using elektronische_componenten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace elektronische_componenten.DAL
{
    public class ComponentContext : DbContext
    {
        public DbSet<Component> Componenten { get; set; }
        public DbSet<Categorie> Categoriën { get; set; }
    }
}