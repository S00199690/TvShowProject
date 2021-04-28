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
        ShowData db = new ShowData();
        List<Show> allShows = new List<Show>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void winTvShows_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from shows in db.Shows
                        select shows;

            allShows = query.ToList();
                      
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
