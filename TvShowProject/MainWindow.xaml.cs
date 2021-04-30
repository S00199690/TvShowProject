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
        #region code
        //variable used to check if a valid number is entered in the add show section
        int number;

        //new instance of database
        ShowData db = new ShowData();

        //list that contains all shows from database
        List<Show> allShows = new List<Show>();

        //filtered show list used with the genre combobox
        List<Show> filteredShows = new List<Show>();
        public MainWindow()
        {
            InitializeComponent();
        }
        //execute upon running of program
        private void winTvShows_Loaded(object sender, RoutedEventArgs e)
        {
            //linq query
            var query = from shows in db.Shows
                        select shows;

            //sets all shows to the above query which returns all the shows from database
            allShows = query.ToList();
                      
            //sort shows
            allShows.Sort();

            //set both listboxes to all the shows in database
            lbxShows.ItemsSource = allShows;

            lbxGallery.ItemsSource = allShows;

            
        }

        //selection changed event handler
        private void lbxShows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //creates a show object
            Show selectedShow = lbxShows.SelectedItem as Show;

            //checks if its not null then displays the information for the selected show in the textboxes
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

        //Clear button for browse shows tab
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //sets textboxes to an empty string when clicked
            tbxTitle?.Clear();
            tbxSeasons?.Clear();
            tbxEpisodes?.Clear();
            tbxYearStart?.Clear();
            tbxDescription?.Clear();
            tbxGenre?.Clear();
        }

        //Delete button to remove shows from program and database
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {           
           //asks the user if they are sure they want to delete show, if yes is clicked it runs the code and if no it cancels the delete
           if(MessageBox.Show($"Are you sure you want to delete this show?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)            
            {
                //removes the show from the program and database
                db.Shows.Remove((Show)lbxShows.SelectedItem);
                allShows.Remove((Show)lbxShows.SelectedItem);

                //saves changes to database
                db.SaveChanges();

                //clears the current information in the textboxes
                btnClear_Click(null, null);

                //sorts the shows
                allShows.Sort();

                //refreshs both listboxes
                lbxShows.ItemsSource = null;
                lbxShows.ItemsSource = allShows;

                lbxGallery.ItemsSource = null;
                lbxGallery.ItemsSource = allShows;

                //delete sucessful message
                MessageBox.Show("Show deleted", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else { }
            


        }

        //Update button to edit information and update the database upon completion
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //creates show object
            Show selectedShow = lbxShows.SelectedItem as Show;

            //asks the user if they want to go ahead with changed, if yes proceeds with code and if no cancels update
            if (MessageBox.Show($"Are you sure you want to make changes to this show?", "Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //sets the selected shows properties to information that is contained in the textboxes
                selectedShow.Title = tbxTitle.Text;
                selectedShow.Seasons = int.Parse(tbxSeasons.Text);
                selectedShow.Episodes = int.Parse(tbxEpisodes.Text);
                selectedShow.YearStart = int.Parse(tbxYearStart.Text);
                selectedShow.Description = tbxDescription.Text;
                selectedShow.Genre = tbxGenre.Text;

                //saves changes to database
                db.SaveChanges();

                //sorts shows
                allShows.Sort();

                //refreshs both listboxes
                lbxShows.ItemsSource = null;
                lbxShows.ItemsSource = allShows;

                lbxGallery.ItemsSource = null;
                lbxGallery.ItemsSource = allShows;

                //message box to let the user know changes have been saved
                MessageBox.Show("Changes to Show have been saved", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else { }
        }

        //Add button to add a new show into database
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
                //creates a new show object which is populated using the information the user adds
                Show addShow = new Show(tbxTitleCreate.Text, int.Parse(tbxSeasonsCreate.Text), int.Parse(tbxEpisodesCreate.Text), int.Parse(tbxYearStartCreate.Text), tbxDescriptionCreate.Text, tbxImageLinkCreate.Text, cbxGenreCreate.Text.ToString());

                //adds show to database
                db.Shows.Add(addShow);
                
                //saves changes to database
                db.SaveChanges();

                //clears current information so the user can start adding another show
                btnClearCreate_Click(null, null);

                //queries database to return all shows including the added show
                var query = from shows in db.Shows
                        select shows;
                //sets all shows to the query
                allShows = query.ToList();

                //sort shows
                allShows.Sort();

                //refreshes show listbox
                lbxShows.ItemsSource = null;
                lbxShows.ItemsSource = allShows;
                
                //sets genre combobox on browse shows tab to all
                cbxGenre.SelectedIndex = 0;

                //refreshes gallery listbox
                lbxGallery.ItemsSource = null;
                lbxGallery.ItemsSource = allShows;

                //messagebox to confirm show has been added
                MessageBox.Show("Show added to list", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                //sets int properties to default value
                tbxSeasonsCreate.Text = "0";
                tbxEpisodesCreate.Text = "0";
                tbxYearStartCreate.Text = "2021";

        }

        //clear button for the add show page
        private void btnClearCreate_Click(object sender, RoutedEventArgs e)
        {
            //clears and sets default values to textboxes
            tbxTitleCreate.Clear();
            tbxDescriptionCreate.Clear();
            tbxImageLinkCreate.Clear();
            tbxYearStartCreate.Text = "2021";
            tbxSeasonsCreate.Text = "0";
            tbxEpisodesCreate.Text = "0";
            cbxGenreCreate.SelectedIndex = 0;
        }

        //lost focus event handler that checks if title textbox is empty
        private void tbxTitleCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            

            if (tbxTitleCreate.Text == "")
            {
                tbxTitleCreate.Text = "Enter Title";
            }
            else if (int.TryParse(tbxTitleCreate.Text, out number))
            {
                //checks if a string is not entered in textbox and displays message telling user that it is not a title and sets it back to default
                MessageBox.Show("Not a Title", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxTitleCreate.Text = "Enter Title";

            }
        }

        //got focus event handler that checks if title textbox contains information
        private void tbxTitleCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxTitleCreate.Text == "Enter Title")
            {
                tbxTitleCreate.Text = "";
            }
            else{ }
        }

        //lost focus event handler that checks if Image Link textbox is empty
        private void tbxImageLinkCreate_LostFocus(object sender, RoutedEventArgs e)
        {

            if (tbxImageLinkCreate.Text == "")
            {
                tbxImageLinkCreate.Text = "Enter Link To Image";
            }
            else { }
        }

        //got focus event handler that checks if Image Link textbox contains information
        private void tbxImageLinkCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxImageLinkCreate.Text == "Enter Link To Image")
            {
                tbxImageLinkCreate.Text = "";
            }
            else { }
            
        }

        //got focus event handler that checks if title textbox contains information
        private void tbxSeasonsCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSeasonsCreate.Text == "0")
            {
                tbxSeasonsCreate.Text = "";
            }
            else { }
        }

        //lost focus event handler that checks if Seasons textbox is empty
        private void tbxSeasonsCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSeasonsCreate.Text == "")
            {
                tbxSeasonsCreate.Text = "0";
            }
            //checks if a number is not entered in textbox and displays message telling user that it is not a number and sets it back to default
            else if (!int.TryParse(tbxSeasonsCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxSeasonsCreate.Text = "0";

            }
        }

        //got focus event handler that checks if Episodes textbox contains information
        private void tbxEpisodesCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "0")
            {
                tbxEpisodesCreate.Text = "";
            }
            else { }
        }

        //lost focus event handler that checks if Episodes textbox is empty
        private void tbxEpisodesCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxEpisodesCreate.Text == "")
            {
                tbxEpisodesCreate.Text = "0";
            }
            //checks if a number is not entered in textbox and displays message telling user that it is not a number and sets it back to default
            else if (!int.TryParse(tbxEpisodesCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxEpisodesCreate.Text = "0";

            }
        }

        //got focus event handler that checks if Year Start textbox contains information
        private void tbxYearStartCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "2021")
            {
                tbxYearStartCreate.Text = "";
            }
            else { }
        }

        //lost focus event handler that checks if Year Start textbox is empty
        private void tbxYearStartCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxYearStartCreate.Text == "")
            {
                tbxYearStartCreate.Text = "2021";

            }
            //checks if a number is not entered in textbox and displays message telling user that it is not a number and sets it back to default
            else if (!int.TryParse(tbxYearStartCreate.Text, out number))
            {
                MessageBox.Show("Not a Valid Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxYearStartCreate.Text = "2021";

            }
        }

        //got focus event handler that checks if Description textbox contains information
        private void tbxDescriptionCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxDescriptionCreate.Text == "Enter A Description")
            {
                tbxDescriptionCreate.Text = "";
            }
            else { }

            
        }

        //lost focus event handler that checks if Description textbox is empty
        private void tbxDescriptionCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxDescriptionCreate.Text == "")
            {
                tbxDescriptionCreate.Text = "Enter A Description";
            }
            else { }

            
        }

        //filtering functionality for genre combobox on browse shows tab
        private void cbxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //switch statement that checks what index it currently is on
            switch (cbxGenre.SelectedIndex)
            {
                //all shows
                case 0:
                    //returns and sorts all shows and refreshs show listbox
                    var queryAll = from shows in db.Shows
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryAll.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;
                    
                //comedy genre
                case 1:
                    //returns and sorts Comedy shows and refreshs show listbox
                    var queryComedy = from shows in db.Shows
                                where shows.Genre.Contains("Comedy")
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryComedy.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;
                
                //action genre
                case 2:
                    //returns and sorts Action shows and refreshs show listbox
                    var queryAction = from shows in db.Shows
                                where shows.Genre.Contains("Action")
                                select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryAction.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;
                
                //horor genre
                case 3:
                    //returns and sorts Horror shows and refreshs show listbox
                    var queryHorror = from shows in db.Shows
                                      where shows.Genre.Contains("Horror")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryHorror.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                //thriller genre
                case 4:
                    //returns and sorts Thriller shows and refreshs show listbox
                    var queryThriller = from shows in db.Shows
                                      where shows.Genre.Contains("Thriller")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryThriller.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;

                //fantasy genre
                case 5:
                    //returns and sorts Fantasy shows and refreshs show listbox
                    var queryFantasy = from shows in db.Shows
                                      where shows.Genre.Contains("Fantasy")
                                      select shows;
                    btnClear_Click(null, null);
                    filteredShows = queryFantasy.ToList();
                    filteredShows.Sort();
                    lbxShows.ItemsSource = null;
                    lbxShows.ItemsSource = filteredShows;
                    break;
                
                //animated genre
                case 6:
                    //returns and sorts Animated shows and refreshs show listbox
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

        //hperlink method that enables naviating to the requested uri
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        //Like button that writes and overwrites to the "like.json" file
        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            //checks if null
            if(lbxShows.SelectedItem != null)
            {
                //converts selected show into json form
                string data = JsonConvert.SerializeObject(lbxShows.SelectedItem, Formatting.Indented);

                //uses json file to write selected shows data to it
                using (StreamWriter sw = new StreamWriter("like.json"))
                {
                    sw.Write(data);

                    sw.Close();
                }
                //message box telling the user that the show selected is the liked show
                MessageBox.Show("Selected Show is now your Liked Show", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        //Liked show button that reads from the JSON file and displays title of show
        private void btnLikedShow_Click(object sender, RoutedEventArgs e)
        {
            //opens and reads from "like.json" file
            using (StreamReader file = File.OpenText("like.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject token = (JObject)JToken.ReadFrom(reader);
                //displays title in text block
                tblkLikedShow.Text = token.SelectToken("Title").ToString();
            }
        }
        #endregion code
    }
}
