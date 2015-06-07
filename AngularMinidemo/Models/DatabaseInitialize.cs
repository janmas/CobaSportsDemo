using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularMinidemo.Models
{
    public class CobaSportsContextInitializer : DropCreateDatabaseAlways<SportContext>
    {
        protected override void Seed(SportContext context)
        {
            int maxElements = 5;
            IList<Sport> sports = Enumerable.Range(0, maxElements).Select(i => new Sport() { Id = i, Caption = "Sport " + i }).ToList();
            context.Sports.AddRange(sports);
        }
    }
}