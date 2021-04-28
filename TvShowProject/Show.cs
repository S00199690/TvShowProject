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

        public int? YearEnd { get; set; }

        public string Description { get; set; }

        public string ShowImage { get; set; }

        public string Genre { get; set; }

        //constructors
        public Show(string title, int seasons, int episodes, int yearStart, int? yearEnd, string description, string showImage)
        {
            Title = title;
            Seasons = seasons;
            Episodes = episodes;
            YearStart = yearStart;
            YearEnd = yearEnd;
            Description = description;
            ShowImage = showImage;
            
        }

        public Show()
        {

        }

        //methods

        public override string ToString()
        {
            return Title;
        }

        public int CompareTo(object obj)
        {
            Show otherShow = obj as Show;
            return this.Title.CompareTo(otherShow.Title);
        }
    }

   
}
