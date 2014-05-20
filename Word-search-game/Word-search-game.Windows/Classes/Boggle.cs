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
        public static Level settings;
        private StackPanel gridPanel;
        private StackPanel wordPanel;

        #endregion

        #region Constructor
        /*
         * Constructor.
         * @param String difficulty - The name of the difficulty.
         * @param int level - The number of the level within the difficulty.
         * @param Stackpanel panel - The panel from the View.
         * @savedAt this.settings
         */
        public Boggle(String difficulty, int level, StackPanel gridPanel, StackPanel wordPanel)
        {
            // Check if the difficulty exists.
            if (Array.IndexOf(Levels.types, difficulty) != -1)
            {
                this.gridPanel = gridPanel;
                this.wordPanel = wordPanel;
                // Get the settings for this type of difficulty and this level.
                settings = Levels.easy[0];

                // Select some words.
                this.selectWords();

                // Create the board.
                this.create(settings.columns, settings.rows);

                // Place the first word.
                Boolean first = this.placeFirstWord();

                // Place the other words to.
                if (first.Equals(true))
                {
                 //   this.placeOtherWords();
                }

                // Replace this.words with only the words that are placed.
                //this.onlyPlacedWords();
   
                // Show the board to the user.
                this.display();
            }
            else
            {
                // This difficulty doens't exist.
            }
        }
        #endregion

        #region Display

        private void display()
        {
            // Get the grid and show it.
            Grid grid = board.show();
            this.gridPanel.Children.Add(grid);
        }

        #endregion

        #region Create

        /*
        * Create a new board instance.
        * @param int columns - the number of columns.
        * @param int rows - the number of rows.
        * @panel Stackpanel - The panel from the view.
        * @type Static! <--------
        */
        private void create(int columns, int rows)
        {
            board = new Board(columns, rows);
        }

        #endregion

        #region Place

        #endregion

        #region Select

        /*
         * Select a number of random word from the WordList
         * and convert the String into a Word
         * and add to them this.words array
         * 
         * @savedAt this.words.
         * //27375
         */
        public void selectWords()
        {
            Random random = new Random();
            this.words = new Word[settings.words];
            for (int i = 0, len = settings.words; i < len; i++)
            {
                int randomPos = random.Next(0, WordList.words.Length - 1);
                String value = WordList.words[randomPos];
                System.Diagnostics.Debug.WriteLine(randomPos);
                words[i] = new Word(value);
            }

        }

        #endregion  

        /*
         * Try to place to first word.
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
            System.Diagnostics.Debug.WriteLine("Place first word result:" + result + " : " + word.value);
            return result;
        }


        private Boolean placeOtherWords()
        {
            // Loop through the word and try to place them one by one.
            for (int ci = 1, clen = this.words.Length - 1; ci < clen; ci++)
            {
                // 
                Word word = this.words[ci];

                // Search for some pace.
                List<int[]> searchSpaces = Boggle.board.search(word.value);

                // Try to place the word accourding to the spaces.
                for (int si = 0, slen = searchSpaces.Count; si < slen; si++)
                {
                    int[] space = searchSpaces[si];       
                    // Try to place the word at this space.
                    Boolean result = word.place(space[0], space[1], space[2]);
                    if (result.Equals(false))
                    {
                        // Try again.
                    }
                    else
                    {
                        // Go to the next word.
                        break;
                    }

                }
            }
            return false;
        }

    }
}
