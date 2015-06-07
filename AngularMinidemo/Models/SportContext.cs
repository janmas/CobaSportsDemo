using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularMinidemo.Models
{
    public class SportContext : DbContext
        
    {
        public SportContext()
            : base("CobaSportsMinidemo")
        {
            // Database.SetInitializer(new CobaSportsContextInitializer());
        }

        //this.Configuration.ProxyCreationEnabled = false;
        public System.Data.Entity.DbSet<AngularMinidemo.Models.Sport> Sports { get; set; }        
    }
}