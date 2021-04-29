using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TvShowProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int number;
        ShowData db = new ShowData();
        List<Show> allShows = new List<Show>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void winTvShows_Loaded(object sender, RoutedEventArgs e)
        {
            //linq query
            var query = from shows in db.Shows
                        select shows;

            allShows = query.ToList();
                      
            //sort shows
            allShows.Sort();

            lbxShows.ItemsSource = allShows;

            lbxGallery.ItemsSource = allShows;

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {           
           if(MessageBox.Show($"Are you sure you want to delete this show?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)            
            {
                db.Shows.Remove((Show)lbxShows.SelectedItem);
                allShows.Remove((Show)lbxShows.SelectedItem);

                db.SaveChanges();

                btnClear_Click(null, null);

                allShows.Sort();

                lbxShows.ItemsSource = null;
                lbxShows.ItemsSource = allShows;

                MessageBox.Show("Show deleted", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else { }
            


        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            Show addShow = new Show(tbxTitleCreate.Text, int.Parse(tbxSeasonsCreate.Text), int.Parse(tbxEpisodesCreate.Text), int.Parse(tbxYearStartCreate.Text), int.Parse(tbxYearEndCreate.Text), tbxDescriptionCreate.Text, tbxImageLinkCreate.Text);
            db.Shows.Add(addShow);

            db.SaveChanges();

            btnClearCreate_Click(null, null);

            var query = from shows in db.Shows
                        select shows;

            allShows = query.ToList();

            //sort shows
            allShows.Sort();

            lbxShows.ItemsSource = allShows;   

            MessageBox.Show("Show added to list", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnClearCreate_Click(object sender, RoutedEventArgs e)
        {
            tbxTitleCreate.Clear();
            tbxSeasonsCreate.Clear();
            tbxEpisodesCreate.Clear();
            tbxYearStartCreate.Clear();
            tbxYearEndCreate.Clear();
            tbxDescriptionCreate.Clear();
            tbxImageLinkCreate.Clear();
        }

        private void tbxTitleCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            

            if (tbxTitleCreate.Text == "")
            {
                tbxTitleCreate.Text = "Enter Title";
            }
            else if (int.TryParse(tbxTitleCreate.Text, out number))
            {
                MessageBox.Show("Not a Title", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxTitleCreate.Text = "Enter Title";

            }
        }

        private void tbxTitleCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxTitleCreate.Text == "Enter Title")
            {
                tbxTitleCreate.Text = "";
            }
            else{ }
        }

        private void tbxImageLinkCreate_LostFocus(object sender, RoutedEventArgs e)
        {

            if (tbxImageLinkCreate.Text == "")
            {
                tbxImageLinkCreate.Text = "Enter Link To Image";
            }
            else { }
        }

        private void tbxImageLinkCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxImageLinkCreate.Text == "Enter Link To Image")
            {
                tbxImageLinkCreate.Text = "";
            }
            else { }
        }

        private void tbxSeasonsCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSeasonsCreate.Text == "Enter Number Of Seasons")
            {
                tbxSeasonsCreate.Text = "";
            }
            else { }
        }

        private void tbxSeasonsCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSeasonsCreate.Text == "")
            {
                tbxSeasonsCreate.Text = "Enter Number Of Seasons";
            }
            else if (!int.TryParse(tbxSeasonsCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxSeasonsCreate.Text = "Enter Number Of Seasons";

            }
        }

        private void tbxEpisodesCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "Enter Number Of Episodes")
            {
                tbxEpisodesCreate.Text = "";
            }
            else { }
        }

        private void tbxEpisodesCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "")
            {
                tbxEpisodesCreate.Text = "Enter Number Of Episodes";
            }
            else if (!int.TryParse(tbxEpisodesCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxEpisodesCreate.Text = "Enter Number Of Episodes";

            }
        }

        private void tbxYearStartCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "Enter Year Started")
            {
                tbxYearStartCreate.Text = "";
            }
            else { }
        }

        private void tbxYearStartCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "")
            {
                tbxYearStartCreate.Text = "Enter Year Started";
            }
            else if (!int.TryParse(tbxYearStartCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxYearStartCreate.Text = "Enter Year Started";

            }
        }

        private void tbxYearEndCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearEndCreate.Text == "Enter Year Ended")
            {
                tbxYearEndCreate.Text = "";
            }
            else { }
        }

        private void tbxYearEndCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearEndCreate.Text == "")
            {
                tbxYearEndCreate.Text = "Enter Year Ended";
            }
            else if (!int.TryParse(tbxYearEndCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxYearEndCreate.Text = "Enter Year Ended";

            }
        }

        private void tbxDescriptionCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxDescriptionCreate.Text == "Enter A Description")
            {
                tbxDescriptionCreate.Text = "";
            }
            else { }
        }

        private void tbxDescriptionCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxDescriptionCreate.Text == "")
            {
                tbxDescriptionCreate.Text = "Enter A Description";
            }
            else { }

            
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            allShows.Sort();

            lbxGallery.ItemsSource = null;

            lbxGallery.ItemsSource = allShows;
        }
    }
}
