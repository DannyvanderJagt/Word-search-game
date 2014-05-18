﻿using System;
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
        public static Level settings;

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
                settings = Levels.easy[0];

                // Select some words.
                this.selectWords();

                // Create the board.
                createBoard(settings.columns, settings.rows, panel);

                // Place the first word.
                Boolean first = this.placeFirstWord();

                // Place the other words to.
                this.placeOtherWords();
   
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
            this.words = new Word[settings.words];
            for (int i = 0, len = settings.words; i < len; i++)
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
            System.Diagnostics.Debug.WriteLine("FIRST:"+word.value);
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

        /*
         * Try to place as many words.
         */
        private void placeOtherWords()
        {
            List<int> placedTotal = new List<int>();
            List<int> notPlacedTotal = new List<int>();
            placedTotal.Add(0);

            // Add the positions of all the words to the notPlacedTotal list, except the first word because that is already placed.
            for (int i = 1, len = this.words.Length; i < len; i++)
            {
                notPlacedTotal.Add(i);
            }

            // Loop!
            for (int turn = 0; turn < 3; turn++)
            {
                List<int> placed = new List<int>();
                List<int> notPlaced = new List<int>();

                foreach (int i in notPlacedTotal)
                {
                    Boolean result = this.placeWord(i);
                    if (result)
                    {
                        placedTotal.Add(i);
                    }
                    else
                    {
                        notPlaced.Add(i);
                    }
                }
                // Swap the not placed word and try again.
                notPlacedTotal = notPlaced;
            }
            System.Diagnostics.Debug.WriteLine(placedTotal.Count + " : " + this.words.Length);


        }


        // Try to place a word.
        private Boolean placeWord(int pos)
        {
            Word word = this.words[pos];
            List<int[]> spaces = board.search(word.value);

            if (spaces.Count > 0)
            {
                // Loop throught the spaces and try to fit the word there.
                for (int i = 0, len = spaces.Count; i < len; i++)
                {
                    int[] space = spaces[i];
                    // Try to place the word.
                    Boolean result = word.place(space[0], space[1], space[2]);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
