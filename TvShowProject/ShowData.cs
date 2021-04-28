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
        public ShowData() : base("MyShowData")
        {

        }//give db its name

        public DbSet<Show> Shows { get; set; } //creates Show Table

    }
}
