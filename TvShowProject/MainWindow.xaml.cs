﻿using System;
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
            Show s1 = new Show() { Title = "Breaking Bad", Seasons = 5, Episodes = 62, YearStart = 2008, YearEnd = 2013, Description = "Breaking bad sees Chemistry teacher Walter White and drop out student Jesse Pinkman team up making meth. Not everything goes to plan." };
            Show s2 = new Show() { Title = "Rick and Morty", Seasons = 4, Episodes = 41, YearStart = 2013, YearEnd = 2021, Description = "Animated adventures of mad scientist Rick Sanchez and schoolboy Morty Smith" };
            Show s3 = new Show() { Title = "The Office", Seasons = 9, Episodes = 201, YearStart = 2005, YearEnd = 2013, Description = "Mockumentary of a normal day to day in an paper company" };
            Show s4 = new Show() { Title = "Paradise PD", Seasons = 3, Episodes = 18, YearStart = 2018, YearEnd = 2021, Description = "Animated ventures of local police team" };
            Show s5 = new Show() { Title = "Aunty Donnas Big Ol House Of Fun", Seasons = 1, Episodes = 6, YearStart = 2020, YearEnd = 2021, Description = "Morning Brown" };
            
            //add to collection
            allShows.Add(s1);
            allShows.Add(s2);
            allShows.Add(s3);
            allShows.Add(s4);
            allShows.Add(s5);

            //sort bands
            allShows.Sort();

            lbxShows.ItemsSource = allShows;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void lbxShows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show selectedShow = lbxShows.SelectedItem as Show;

            if(selectedShow != null)
            {
                tbkTitle.Text = selectedShow.Title;
                tbkSeasons.Text = selectedShow.Seasons.ToString();
                tbkEpisodes.Text = selectedShow.Episodes.ToString();
                tbkYearStarted.Text = selectedShow.YearStart.ToString();
                tbkYearEnded.Text = selectedShow.YearEnd.ToString();
                tbkDescription.Text = selectedShow.Description;
                
            }
        }
    }
}
