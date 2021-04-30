using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Media;
using System.Windows.Input;
using System;

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
        List<Show> filteredShows = new List<Show>();
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

            if (selectedShow != null)
            {
                tbxTitle.Text = selectedShow.Title;
                tbxSeasons.Text = selectedShow.Seasons.ToString();
                tbxEpisodes.Text = selectedShow.Episodes.ToString();
                tbxYearStart.Text = selectedShow.YearStart.ToString();
                tbxDescription.Text = selectedShow.Description;
                tbxGenre.Text = selectedShow.Genre;


            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxTitle?.Clear();
            tbxSeasons?.Clear();
            tbxEpisodes?.Clear();
            tbxYearStart?.Clear();
            tbxDescription?.Clear();
            tbxGenre?.Clear();
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

                lbxGallery.ItemsSource = null;
                lbxGallery.ItemsSource = allShows;

                MessageBox.Show("Show deleted", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else { }
            


        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Show selectedShow = lbxShows.SelectedItem as Show;

            if (MessageBox.Show($"Are you sure you want to make changes to this show?", "Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                selectedShow.Title = tbxTitle.Text;
                selectedShow.Seasons = int.Parse(tbxSeasons.Text);
                selectedShow.Episodes = int.Parse(tbxEpisodes.Text);
                selectedShow.YearStart = int.Parse(tbxYearStart.Text);
                selectedShow.Description = tbxDescription.Text;
                selectedShow.Genre = tbxGenre.Text;

                db.SaveChanges();

                var query = from shows in db.Shows
                            select shows;

                allShows = query.ToList();

                allShows.Sort();

                lbxShows.ItemsSource = allShows;

                lbxGallery.ItemsSource = null;

                lbxGallery.ItemsSource = allShows;

                MessageBox.Show("Changes to Show have been saved", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else { }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
                Show addShow = new Show(tbxTitleCreate.Text, int.Parse(tbxSeasonsCreate.Text), int.Parse(tbxEpisodesCreate.Text), int.Parse(tbxYearStartCreate.Text), tbxDescriptionCreate.Text, tbxImageLinkCreate.Text, cbxGenreCreate.Text.ToString());
                db.Shows.Add(addShow);

                db.SaveChanges();

                btnClearCreate_Click(null, null);

                var query = from shows in db.Shows
                            select shows;

                allShows = query.ToList();

                //sort shows
                allShows.Sort();

                lbxShows.ItemsSource = allShows;

                cbxGenre.SelectedIndex = 0;

                lbxGallery.ItemsSource = null;

                lbxGallery.ItemsSource = allShows;

                MessageBox.Show("Show added to list", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                tbxSeasonsCreate.Text = "0";
                tbxEpisodesCreate.Text = "0";
                tbxYearStartCreate.Text = "2021";

        }

        private void btnClearCreate_Click(object sender, RoutedEventArgs e)
        {
            tbxTitleCreate.Clear();
            tbxDescriptionCreate.Clear();
            tbxImageLinkCreate.Clear();
            tbxYearStartCreate.Text = "2021";
            tbxSeasonsCreate.Text = "0";
            tbxEpisodesCreate.Text = "0";
            cbxGenreCreate.SelectedIndex = 0;
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
            if (tbxSeasonsCreate.Text == "0")
            {
                tbxSeasonsCreate.Text = "";
            }
            else { }
        }

        private void tbxSeasonsCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSeasonsCreate.Text == "")
            {
                tbxSeasonsCreate.Text = "0";
            }
            else if (!int.TryParse(tbxSeasonsCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxSeasonsCreate.Text = "0";

            }
        }

        private void tbxEpisodesCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "0")
            {
                tbxEpisodesCreate.Text = "";
            }
            else { }
        }

        private void tbxEpisodesCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "")
            {
                tbxEpisodesCreate.Text = "0";
            }
            else if (!int.TryParse(tbxEpisodesCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxEpisodesCreate.Text = "0";

            }
        }

        private void tbxYearStartCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "2021")
            {
                tbxYearStartCreate.Text = "";
            }
            else { }
        }

        private void tbxYearStartCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "")
            {
                tbxYearStartCreate.Text = "2021";

            }
            else if (!int.TryParse(tbxYearStartCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxYearStartCreate.Text = "2021";

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

        private void cbxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbxGenre.SelectedIndex)
            {
                case 0:
                    var queryAll = from shows in db.Shows
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryAll.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;
                    

                case 1:
                    var queryComedy = from shows in db.Shows
                                where shows.Genre.Contains("Comedy")
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryComedy.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                case 2:
                    var queryAction = from shows in db.Shows
                                where shows.Genre.Contains("Action")
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryAction.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                case 3:
                    var queryHorror = from shows in db.Shows
                                      where shows.Genre.Contains("Horror")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryHorror.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                case 4:
                    var queryThriller = from shows in db.Shows
                                      where shows.Genre.Contains("Thriller")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryThriller.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                case 5:
                    var queryFantasy = from shows in db.Shows
                                      where shows.Genre.Contains("Fantasy")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryFantasy.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                case 6:
                    var queryAnimated = from shows in db.Shows
                                      where shows.Genre.Contains("Animated")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryAnimated.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                default:
                    break;

            }

        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            if(lbxShows.SelectedItem != null)
            {
                string data = JsonConvert.SerializeObject(lbxShows.SelectedItem, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter("like.json"))
                {
                    sw.Write(data);

                    sw.Close();
                }

                MessageBox.Show("Selected Show is now your Liked Show", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        private void btnLikedShow_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader file = File.OpenText("like.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject token = (JObject)JToken.ReadFrom(reader);

                tblkLikedShow.Text = token.SelectToken("Title").ToString();
            }
        }

    }
}
