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
    class Tile
    {
        #region Variables
        // Display element.
        public Grid background;
        private TextBlock text;

        // Class instance data.
        private int x = -1; // The x position wihtin the board.
        private int y = -1; // The y position within the board.
        public String value; // The value of this tile. Only 1 character long!
        private Dictionary<String, Char> chars = new Dictionary<String, Char>(); // Store all the characters that are placed at this tile.
        public Boolean active = false; // Store if this tile is active.
        public Boolean completed = false; // Store if this tile is part of a completed word.
        #endregion

        #region Constructor
        /*
         * Constuctor
         * @param int x - The x position of this tile within the board.
         * @param int y - The y position of this tile within the board.
         */
        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Display
        /*
         * Create the display element: background.
         * @used this.x, this.y
         * @savedAt this.background
         * @return Grid this.background - The background element.
         */
        public Grid getBackground(){
            Grid background = new Grid();
            background.Name = this.x + "," + this.y;
            background.Background = Colors.green;
            background.Margin = new Thickness(6);
            Grid.SetColumn(background, this.x);
            Grid.SetRow(background, this.y);
            this.background = background;
            return this.background;
        }

        /*
         * Create the display element: text.
         * @used this.background, this.x, this.y
         * @savedAt this.text
         * @return TextBlock this.text - The text element.
         * @alert! -> The click handler is added at board.cs
         */
        public TextBlock getText()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Name = this.x + "," + this.y;
            textBlock.Text = this.value == null ? "".ToString(): this.value;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.MinWidth = background.ActualWidth;
            textBlock.MinHeight = background.ActualHeight;
            textBlock.FontSize = 24;
            Grid.SetColumn(textBlock, this.x);
            Grid.SetRow(textBlock, this.y);
            this.text = textBlock;
            return this.text;
        }

        #endregion

        #region Place

        /*
         * Try to place the character into this tile.
         * @param Char character - The character that has to be placed.
         */
        public Boolean place(Char character)
        {
            if(this.value == character.value || String.IsNullOrEmpty(this.value)){
                if(this.chars.ContainsKey(character.word.value).Equals(false)){
                    // The character can be placed.
                     return  this.addChar(character);
                }
                return false;
            }
            return false;
        }

        /*
         * (Try to) unplace the character from this tile.
         * 
         * @param Char character - The character has to be unplaced.
         */
        public Boolean unplace(Char character)
        {
           if (this.chars.ContainsKey(character.word.value))
           {
              this.chars.Remove(character.word.value);
           }
           if (this.chars.Count == 0)
           {
               this.value = "";
           }
           return false;
        }


        /*
         * Add a char to this tile. (this.chars array)
         * @param Char character - The character that has to be placed.
         */
        private Boolean addChar(Char character)
        {
            this.chars.Add(character.word.value, character);
            if (this.chars.Count == 1)
            {
                this.value = character.value;
            }
            return true;
        }

        /*
         * Remove a character from this tile. (this.chars array)
         * @param Char character - The character that has to be removed.
         */
        public Boolean removeChar(Char character)
        {
            if (this.chars.ContainsKey(character.word.value))
            {
                this.chars.Remove(character.word.value);
                return true;
            }
            return false;
        }

        /*
         * Check if a char can be placed at this tile.
         */
        public Boolean check(Char character)
        {
            if (this.value == character.value || String.IsNullOrEmpty(this.value))
            {
                if (this.chars.ContainsKey(character.word.value).Equals(false))
                {
                    // The character can be placed.
                    return true;
                }
                return false;
            }
            return false;
        }

        #endregion

        #region Events

        /*
         * When this tile is clicked alert to character that belong to this character.
         */
        public void clicked()
        {
            // When this tile not active make it active.
            if (this.active.Equals(false))
            {
                this.active = true;
                foreach(String s in chars.Keys){
                    this.chars[s].setActive();
                }
            }
            else
            {
                // When this tile is active make it not active.
                this.active = false;
                // Alert the words that this tile is deselected.
                foreach (String s in chars.Keys)
                {
                    this.chars[s].setInactive();
                }
            }
        }

        /*
         * Reset the character from this tile when a word is completed.
         */
        public void reset()
        {
            // Reset all this tile for all the words.
            foreach(String key in this.chars.Keys)
            {
                this.chars[key].active = false; // Needs some more actions I think!
                this.chars[key].tile.active = false;
                this.chars[key].tile.completed = true;
            }
        }


        #endregion

    }
}
