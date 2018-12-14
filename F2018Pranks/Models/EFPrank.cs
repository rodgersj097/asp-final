using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2018Pranks.Models
{
    public class EFPrank : IMockPrank
    {
        public DbModel db = new DbModel();

        public IQueryable<Prank> Pranks { get { return db.Pranks; } }

        public Prank Save(Prank prank)
        {
            if(prank.PrankId == 0)
            {
                db.Pranks.Add(prank);
            }
            else
            {
                db.Entry(prank).State = System.Data.Entity.EntityState.Modified; 
            }
            db.SaveChanges();
            return prank;
        }
    }
}