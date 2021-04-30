using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowProject;

namespace DataManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            //create database instance
            ShowData db = new ShowData();

            using (db)
            {
                //creating objects
                Show s1 = new Show() { ShowID= 1, Title = "Breaking Bad", Seasons = 5, Episodes = 62, YearStart = 2008, /*YearEnd = 2013,*/ Description = "Breaking bad sees Chemistry teacher Walter White and drop out student Jesse Pinkman team up making meth. Not everything goes to plan.", ShowImage = "https://upload.wikimedia.org/wikipedia/en/6/61/Breaking_Bad_title_card.png", Genre = "Action" };
                Show s2 = new Show() { ShowID = 2, Title = "Rick and Morty", Seasons = 4, Episodes = 41, YearStart = 2013,/*YearEnd = null,*/ Description = "Animated adventures of mad scientist Rick Sanchez and schoolboy Morty Smith", ShowImage = "https://m.media-amazon.com/images/M/MV5BZjRjOTFkOTktZWUzMi00YzMyLThkMmYtMjEwNmQyNzliYTNmXkEyXkFqcGdeQXVyNzQ1ODk3MTQ@._V1_.jpg", Genre = "Animated" };
                Show s3 = new Show() { ShowID = 3, Title = "The Office", Seasons = 9, Episodes = 201, YearStart = 2005, /*YearEnd = 2013,*/ Description = "Mockumentary of a normal day to day in an paper company", ShowImage = "/Images/the office.jpg", Genre = "Comedy" };
                Show s4 = new Show() { ShowID = 4, Title = "Paradise PD", Seasons = 3, Episodes = 18, YearStart = 2018, /*YearEnd = null,*/ Description = "Animated ventures of local police team", ShowImage = "/Images/paradisepd.png", Genre = "Animated" };

                //add to database
                db.Shows.Add(s1);
                db.Shows.Add(s2);
                db.Shows.Add(s3);
                db.Shows.Add(s4);

                //save changes to database
                db.SaveChanges();
                Console.WriteLine("Saved changes to database");
            }
        }
    }
}
