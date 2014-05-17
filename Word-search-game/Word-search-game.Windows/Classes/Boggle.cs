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
        // Static board for overall access.
        public static Board board;

        // Normal variables.
        public Word[] words; // All the words that are placed in the board.
        public Level settings;

        /*
         * Constructor.
         * @param String difficulty - The name of the difficulty.
         * @param int level - The number of the level within the difficulty.
         * @param Stackpanel panel - The panel from the View.
         * @savedAt this.settings
         */
        public Boggle(String difficulty, int level, StackPanel panel)
        {
            // Check if the difficulty exists.
            if (Array.IndexOf(Levels.types, difficulty) != -1)
            {
                // Get the settings for this type of difficulty and this level.
                this.settings = Levels.easy[0];

                // Select some words.
                this.selectWords();

                // Create the board.
                createBoard(this.settings.columns, this.settings.rows, panel);

                // Place the first word.
                Boolean first = this.placeFirstWord();
   
                // Show the board to the user.
                board.show();
            }
            else
            {
                // This difficulty doens't exist.
            }
        }

        /*
         * Create a new board instance.
         * @param int columns - the number of columns.
         * @param int rows - the number of rows.
         * @panel Stackpanel - The panel from the view.
         * @type Static! <--------
         */
        public static void createBoard(int columns, int rows, StackPanel panel)
        {
            board = new Board(columns, rows, panel);
        }

        /*
         * Select a number of random word from the WordList.
         * @savedAt this.words.
         */
        public void selectWords()
        {
            Random random = new Random();
            this.words = new Word[this.settings.words];
            for (int i = 0, len = this.settings.words; i < len; i++)
            {
                int randomPos = random.Next(0, WordList.words.Length-1);
                String value = WordList.words[randomPos];
                words[i] = new Word(value);
            }

        }

        /*
         * Try to place to first word.
         * @var int firstWordTrys - Count to number of tries.
         */
        private int firstWordTrys = 0;
        private Boolean placeFirstWord()
        {
            Word word = this.words[0];
            int rx = new Random().Next(0, board.columns);
            int ry = new Random().Next(0, board.rows);
            int rpos = new Random().Next(0, word.length);
            // Try to place this word.
            Boolean result = word.place(rx, ry, rpos);
            // Check if the placing was successful.
            this.firstWordTrys++;
            if (!result)
            {
                if (this.firstWordTrys < 5)
                {
                    this.placeFirstWord();
                }
                else
                {
                    // The word can't be placed after 5 trys.
                    return false;
                }
            }
            return true;
        }
    }
}
