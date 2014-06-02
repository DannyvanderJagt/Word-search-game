using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Input;

namespace Word_search_game.Classes
{
    class Boggle
    {
        #region Variables
        // Static board for overall access.
        public static Board board;

        // Normal variables.
        public Word[] words; // All the words that are placed in the board.
        public static Level settings; // These settings are abstracted from the levels.cs
        public String difficulty = "easy";
        public int level = 1;
        private StackPanel gridPanel; // UI Element to hold all the chars/tiles.
        private StackPanel wordPanel; // UI Element to hold all the words. (Word list)
        private TextBlock timerPanel;
        private int placedWordCount = 0; // Check how many words are placed. *used at this.onlyPlacedWords()

        #endregion

        #region Constructor
        /*
         * Constructor.
         * @param String difficulty - The name of the difficulty.
         * @param int level - The number of the level within the difficulty.
         * @param Stackpanel panel - The panel from the View.
         * @savedAt this.settings
         */
        public Boggle(String difficulty, int level, StackPanel gridPanel, StackPanel wordPanel, TextBlock timerPanel)
        {
            // Check if the difficulty exists.
            if (Array.IndexOf(Levels.types, difficulty) != -1)
            {
                this.difficulty = difficulty;
                this.level = level;
                this.gridPanel = gridPanel;
                this.wordPanel = wordPanel;
                this.timerPanel = timerPanel;
                // Get the settings for this type of difficulty and this level.
                settings = Levels.data[difficulty][level];
               
                // Select some words.
                this.selectWords();

                // Create the board.
                this.create(settings.columns, settings.rows);

                // Place the first word.
                Boolean first = this.placeFirstWord();

                // Place the other words to.
                if (first.Equals(true))
                {
                    this.placedWordCount++;
                    this.placeOtherWords();
                }

                // Fill the empty gaps.
                this.fillGaps();

                // Replace this.words with only the words that are placed.
                 this.onlyPlacedWords();
   
                // Show the board to the user.
                this.display();
                this.start();
            }
            else
            {
                // This difficulty doens't exist.
                // TODO: Make an error and send the user to the levels page.
            }
        }
        #endregion

        #region Display

        /*
         * Display all the element for the game page.
         * @alert! Tiles, words and chars must been made before calling this method.
         */
        private void display()
        {
            // Get the grid and show it.
            Grid grid = board.show();
            this.gridPanel.Children.Add(grid);
            // Display the words.
            grid = this.createWordGrid();
            for (int i = 0, len = this.words.Length; i < len; i++)
            {
                // Create the board elements.
                if (this.words[i] != null)
                {
                    Grid background = this.words[i].getBackground(i);
                    TextBlock text = this.words[i].getText(i);
                    grid.Children.Add(background);
                    grid.Children.Add(text);
                }
            }
            this.wordPanel.Children.Add(grid);
        }

        /*
         * Create a grid for the display method above.
         */
        private Grid createWordGrid()
        {

            Grid grid = new Grid();

            for (int i = 0; i < this.words.Length; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.MinHeight = 50;
                rowDefinition.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDefinition);

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.MinWidth = 200;
                columnDefinition.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(columnDefinition);
            }

            return grid;
        }

        #endregion

        #region Create

        /*
        * Create a new board instance.
        * @param int columns - the number of columns.
        * @param int rows - the number of rows.
        */
        private void create(int columns, int rows)
        {
            board = new Board(columns, rows, this);
        }

        /*
         * Fill all the empty gaps.
         */
        private void fillGaps()
        {
            // Generate a random int.
            Random random = new Random();
            String[] chars = new String[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            // Loop through the board to search for empty tiles.
            for (int xi = 0, xlen = Boggle.board.columns; xi < xlen; xi++)
            {
                for (int yi = 0, ylen = Boggle.board.rows; yi < ylen; yi++)
                {
                    // Get the tile.
                    Tile tile = Boggle.board.tiles[xi, yi];
                    if (String.IsNullOrEmpty(tile.value))
                    {
                        // Empty tile founded, lets fill it up.
                        int ri = random.Next(0, 26);
                        tile.value = chars[ri];
                    }
                }   
            }
        }


        #endregion

        #region Select

        /*
         * Select a number of random word from the WordList
         * and convert the String into a Word
         * and add to them this.words array
         * 
         * @savedAt this.words.
         */
        private void selectWords()
        {
            Random random = new Random();
            this.words = new Word[settings.words];
            for (int i = 0, len = settings.words; i < len; i++)
            {
                int randomPos = random.Next(0, WordList.words.Length - 1);
                String value = WordList.words[randomPos];
                words[i] = new Word(value);
            }

        }

        /*
         * Filter the this.word list. 
         * After this function this list is shortend to 
         * a list with only the words that are placed into the board.
         * @alert! - this.words needs to be filled before calling this method.
         */
        private void onlyPlacedWords()
        {
            Word[] placedWords = new Word[this.placedWordCount];
            int count = 0;
            for (int i = 0, len = this.words.Length; i < len; i++)
            {
                Word w = this.words[i];
              
                if (w.chars[0].tile != null)
                {
                    if (this.words[i] != null)
                    {
                        placedWords[count] = this.words[i];
                        count++;
                    }
                }
            }
            this.words = placedWords;
        }

        #endregion  

        #region Place
        
        /*
         * Try to place to first word, when this failed we try until it's placed. 
         * Yes this is an infinite loop but it won't loop more than 4-5 times in real life.
         * @var int firstWordTrys - Count to number of tries.
         */
        private Boolean placeFirstWord()
        {
            Word word = this.words[0]; // The first word.
            // Find a random spot to put him.
            Random random = new Random();
            int rx = random.Next(0, board.columns);
            int ry = random.Next(0, board.rows);
            int rpos = random.Next(0, word.length-1);

            // Try to place to word into the field.
            Boolean result = word.place(rx, ry, rpos);
            if (result.Equals(false))
            {
                // Retry to place the first word.
                this.placeFirstWord();
            }
            return result;
        }

        /*
         * Try to place all the other word into the board.
         * @alert! - this.words needs to be filled before using this method.
         * @alert! - this.placedFirstWord needs to be called an be successfull before calling this method.
         */
        private Boolean placeOtherWords()
        {
            // Loop through the word and try to place them one by one.
            for (int ci = 1, clen = this.words.Length - 1; ci < clen; ci++)
            {
                // 
                Word word = this.words[ci];

                // Search for some pace.
                List<int[]> searchSpaces = Boggle.board.search(word);

                // Try to place the word accourding to the spaces.
                for (int si = 0, slen = searchSpaces.Count; si < slen; si++)
                {
                    int[] space = searchSpaces[si];       
                    // Try to place the word at this space.
                    Boolean result = word.place(space[0], space[1], space[2]);
                    if (result.Equals(false))
                    {
                        // Unplace all the characters.
                        word.unplaceAll();
                    }
                    else
                    {
                        // Go to the next word.
                        System.Diagnostics.Debug.WriteLine("Placed word:" + word.value + "   pos: " + space[0] + ":" + space[1] + ":" + space[2]);
                        this.placedWordCount++;
                        break;
                    }

                }
            }
            return false;
        }

        #endregion

        // State of the game.
        #region Start

        private void start()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public int timer_ticks = 0;
        void timer_Tick(object sender, object e)
        {
            this.timerPanel.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () => { timerPanel.Text = timer_ticks.ToString(); ; });
            timer_ticks++;
        }

        #endregion


      

    }
}
