using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowProject
{
    public class Show : IComparable
    {
        //properties
        [Key]
        public int ShowID { get; set; }
        public string Title { get; set; }
        public int Seasons { get; set; }

        public int Episodes { get; set; }

        public int YearStart { get; set; }

        public string Description { get; set; }

        public string ShowImage { get; set; }

        public string Genre { get; set; }

        //constructors
        public Show(string title, int seasons, int episodes, int yearStart, string description, string showImage, string genre)
        {
            Title = title;
            Seasons = seasons;
            Episodes = episodes;
            YearStart = yearStart;
            Description = description;
            ShowImage = showImage;
            Genre = genre;
            
        }

        public Show(string title, int seasons, int episodes, int yearStart, string description, string genre)
        {
            Title = title;
            Seasons = seasons;
            Episodes = episodes;
            YearStart = yearStart;
            Description = description;
            Genre = genre;
        }

        public Show()
        {

        }

        //methods

        public override string ToString()
        {
            return Title;
        }

        //method to compare and sort shows
        public int CompareTo(object obj)
        {
            Show otherShow = obj as Show;
            return this.Title.CompareTo(otherShow.Title);
        }

        //test method for unit tests
        public string GenreCheck(string genre)
        {
            //checks if genre matches a genre in the database
            if (Genre == genre)
            {
                return genre;
            }
            else
            {
                return "Not a Genre";
            }
        }
    }

   
}
