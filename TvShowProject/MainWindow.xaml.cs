using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TvShowProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Show> allShows = new List<Show>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void winTvShows_Loaded(object sender, RoutedEventArgs e)
        {
            Show s1 = new Show() { Title = "Breaking Bad", Seasons = 5, Episodes = 62, YearStart = 2008, YearEnd = 2013, Description = "Breaking bad sees Chemistry teacher Walter White and drop out student Jesse Pinkman team up making meth. Not everything goes to plan.", ShowImage = "https://upload.wikimedia.org/wikipedia/en/6/61/Breaking_Bad_title_card.png" };
            Show s2 = new Show() { Title = "Rick and Morty", Seasons = 4, Episodes = 41, YearStart = 2013, YearEnd = 2021, Description = "Animated adventures of mad scientist Rick Sanchez and schoolboy Morty Smith", ShowImage = "https://m.media-amazon.com/images/M/MV5BZjRjOTFkOTktZWUzMi00YzMyLThkMmYtMjEwNmQyNzliYTNmXkEyXkFqcGdeQXVyNzQ1ODk3MTQ@._V1_.jpg" };
            Show s3 = new Show() { Title = "The Office", Seasons = 9, Episodes = 201, YearStart = 2005, YearEnd = 2013, Description = "Mockumentary of a normal day to day in an paper company" };
            Show s4 = new Show() { Title = "Paradise PD", Seasons = 3, Episodes = 18, YearStart = 2018, YearEnd = 2021, Description = "Animated ventures of local police team" };
            Show s5 = new Show() { Title = "Aunty Donnas Big Ol House Of Fun", Seasons = 1, Episodes = 6, YearStart = 2020, YearEnd = 2021, Description = "Morning Brown" };
            
            //add to collection
            allShows.Add(s1);
            allShows.Add(s2);
            allShows.Add(s3);
            allShows.Add(s4);
            allShows.Add(s5);

            //sort shows
            allShows.Sort();

            lbxShows.ItemsSource = allShows;
        }

        private void lbxShows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show selectedShow = lbxShows.SelectedItem as Show;

            if(selectedShow != null)
            {
                tbxTitle.Text = selectedShow.Title;
                tbxSeasons.Text = selectedShow.Seasons.ToString();
                tbxEpisodes.Text = selectedShow.Episodes.ToString();
                tbxYearStart.Text = selectedShow.YearStart.ToString();
                tbxYearEnd.Text = selectedShow.YearEnd.ToString();
                tbxDescription.Text = selectedShow.Description;
                
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxTitle.Clear();
            tbxSeasons.Clear();
            tbxEpisodes.Clear();
            tbxYearStart.Clear();
            tbxYearEnd.Clear();
            tbxDescription.Clear();
        }
    }
}
