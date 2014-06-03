using Word_search_game.Common;
using System;
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
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Word_search_game.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class StatisticsPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Statistics stats = new Statistics(); // Stats.


        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /*
         * Constructor.
         */
        public StatisticsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            // Check if a new score needs to be added.
            if (PageSwitcher.statistics.Equals(true))
            {
                popup.IsOpen = true;
                scoreTextBlock.Text = PageSwitcher.score.ToString();
            }
            // Show the scores.
            showScore();
        }


        /*
         * Popup: The cancel button.
         */
        private void cancel(object sender, TappedRoutedEventArgs e)
        {
            popup.IsOpen = false;
            PageSwitcher.statistics = false;
            PageSwitcher.score = -1;
        }
        
        /*
         * Popup: The save button - Save the score and display the new scores.
         */
        async private void save(object sender, TappedRoutedEventArgs e)
        {
            await stats.add(name_tb.Text, PageSwitcher.score);
            showScore();
            popup.IsOpen = false;
            PageSwitcher.statistics = false;
            PageSwitcher.score = -1;
        }


        /*
         * Display the scores in a list.
         */
        async public void showScore()
        {
            await stats.read();
        
           // Dictionary<String, int> scores = stats.sortedScores;
            List<KeyValuePair<string, int>> scores = stats.scores;
            
            int count = scores.Count;
            System.Diagnostics.Debug.WriteLine("length"+count);
            if (scores.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine(scores.Count());
                int pos = 0;
                // Loop and fill the grid.
                Grid nameGrid = this.createScoreGrid(scores.Count(), 800);
                Grid scoreGrid = this.createScoreGrid(scores.Count(), 200);
                foreach(KeyValuePair<string, int> entry in scores)
                {
                    System.Diagnostics.Debug.WriteLine(entry.Key+":"+ entry.Value.ToString());
                        // Name.
                        Grid background = getBackground(pos);
                        TextBlock text = getText(background, entry.Key, pos);
                        nameGrid.Children.Add(background);
                        nameGrid.Children.Add(text);
                        // Score
                        Grid backgroundS = getBackground(pos);
                        TextBlock textS = getText(background,entry.Value.ToString(), pos);
                        scoreGrid.Children.Add(backgroundS);
                        scoreGrid.Children.Add(textS);
                        pos++;
                    
                }
                namePanel.Children.Clear();
                scorePanel.Children.Clear();
                nameGrid.Height = (50 * scores.Count());
                scoreGrid.Height = (50 * scores.Count());
                namePanel.Height = (50 * scores.Count());
                scorePanel.Height = (50 * scores.Count());
                namePanel.Children.Add(nameGrid);
                scorePanel.Children.Add(scoreGrid);
            }
        }

        /*
         * Create a background.
         * @param The positions of this background in the list.
         */
        public Grid getBackground(int pos)
        {
            Grid background = new Grid();
            background.Background = Colors.green;
            background.Margin = new Thickness(6);
            Grid.SetColumn(background, 0);
            Grid.SetRow(background, pos);           
            return background;
        }

        /*
         * Create the text element for the UI.
         * @param int number - the position of the word within the list of words from the Board.cs
         */
        public TextBlock getText(Grid background, String value, int pos)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = value;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.MinWidth = background.ActualWidth;
            textBlock.MinHeight = background.ActualHeight;
            textBlock.FontSize = 20;
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, pos);
            return textBlock;
        }

        /*
         * Create the score grid.
         * @param the width of the grid.
         * @param the height of the grid.
         */
        private Grid createScoreGrid(int length, int width)
        {

            Grid grid = new Grid();

            for (int i = 0; i < length; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.MinHeight = 50;
                rowDefinition.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDefinition);

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.MinWidth = width;
                columnDefinition.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(columnDefinition);
            }

            return grid;
        }
        
        
        
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        
    }
}
