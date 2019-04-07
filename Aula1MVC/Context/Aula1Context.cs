using Aula1MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Aula1MVC.Context
{
    public class Aula1Context : DbContext
    {
        public Aula1Context() : base ("Aula1Context")
        {

        }

        public DbSet<Cliente> Cliente { get; set; }
    }
}