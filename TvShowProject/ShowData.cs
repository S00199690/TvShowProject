using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowProject
{
    public class ShowData: DbContext
    {
        //name of database
        public ShowData() : base("MyShowData")
        {

        }

        //creates Show Table
        public DbSet<Show> Shows { get; set; } 

    }
}
