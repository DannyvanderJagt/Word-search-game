﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Word_search_game.Classes;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Word_search_game.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
            System.Diagnostics.Debug.WriteLine("Fire up! Level: "+ Classes.PageSwitcher.level);

            // Pass to method.
            redirect.someFunc = goToStatistics;

            // Difficulty, Level, grid for the tiles Element, grid for the words element.
            Boggle boggle = new Boggle(Classes.PageSwitcher.Difficulty, Classes.PageSwitcher.level, tileGrid, wordList, timerPanel);
            

        }

        public void goToStatistics(){
            while (Frame.BackStackDepth > 0)
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
            
          //  Frame.CacheSize = 0;

            PageSwitcher.statistics = true;
            Frame.Navigate(typeof(Pages.StatisticsPage));
        }

    }
}
