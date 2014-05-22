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
    class Word
    {
        #region Variables
        public String value = null; // The value/'String' of this word.
        public int length = -1; // The length of the value.
        public Char[] chars; // The value is split up into characters/Char.cs instances.
        private Grid background; // UI - The colored background of the grid.
        private TextBlock text; // UI - The "string"/character of the grid.
        private Boolean founded = false; // Store if the user has founded this word.
        #endregion 

        #region Constructor
        /* 
         * Constructor.
         * @param String value - The value of this word.
         */
        public Word(String value)
        {
            this.value = value;
            this.length = value.Length;
            this.chars = new Char[this.length];
            // Convert the value into chars.
            this.createChars();
        }
        #endregion

        #region Create
        /*
         * Convert the value of this word into Char instances.
         * @savedAt this.chars
         */
        private void createChars()
        {
            for (int i = 0; i < this.length; i++)
            {
                this.chars[i] = new Char(this, i);
            }
        }
        #endregion

        #region Place
        /*
         * Try to place a word.
         * @param int x - The x value of the character that belong to the given position within the word.
         * @param int y - The y value of the character that belong to the given position within the word.
         * @param int pos - The position within the word. (Position of the character within the value of the word).
         */
        public Boolean place(int x, int y, int pos)
        {
            // ---- Place the first character ---- //
            // Check if the requested tile is suitable.
            Boolean space = Boggle.board.check(x,y,this.chars[pos]);

            // Place the first character.
            Boolean placed = this.chars[pos].place(Boggle.board.tiles[x,y]);

            // Current x and y pos.
            int curX = x;
            int curY = y;

            // ---- Place the other characters ---- //
            // Place all the character from the first placed character to the frist character of the word.
            for (int fi = pos - 1, flen = 0; fi >= flen; fi--)
            {
                int[] result = this.placeChar(curX, curY, this.chars[fi]);
                if (result != null)
                {
                    curX = result[0];
                    curY = result[1];
                }
                else
                {
                    return false;
                }
            }
            // Reset the x and y to the begin character.
            curX = x;
            curY = y;

            // Place all the characters from the first placed character to the last character.
            for (int bi = pos + 1, blen = this.chars.Length; bi < blen; bi++)
            {
                int[] result = this.placeChar(curX, curY, this.chars[bi]);
                if (result != null)
                {
                    curX = result[0];
                    curY = result[1];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /*
         * Place a char from a word.
         * 
         * @param int curX - The x position of the latested placed character.
         * @param int curY - The y position of the latested placed character.
         * @param Char character - The character that has to be placed.
         */
        private int[] placeChar(int curX, int curY, Char character)
        {
            // Find space.
            List<int[]> spaces = Boggle.board.space(curX, curY, character);
            // Check.
            for (int si = 0, slen = spaces.Count; si < slen; si++)
            {
                Boolean result = Boggle.board.check(spaces[si][0], spaces[si][1], character);
                if (result == true)
                {
                    // Place
                    character.place(Boggle.board.tiles[spaces[si][0], spaces[si][1]]);
                    curX = spaces[si][0];
                    curY = spaces[si][1];
                    slen = spaces.Count;
                    return new int[]{curX, curY}; // Same as return true.
                }
            }
            return null; // Same as return false.
        }

        /*
         * Unplace all the character from this word.
         * @used - this.chars
         */
        public Boolean unplaceAll()
        {
            for (int i = 0, len = this.chars.Length; i < len; i++)
            {
                Char l = this.chars[i];
                l.unplace();
            }
            return false;
        }
        #endregion

        #region Display

        /*
         * Create a background for the UI grid.
         * @param int number - the position of the word within the list of words from the Board.cs
         */
        public Grid getBackground(int number)
        {
            Grid background = new Grid();
            background.Name = "grid_" + this.value;
            background.Background = Colors.green;
            background.Margin = new Thickness(6);
            background.Tapped += clicked;
            Grid.SetColumn(background, 0);
            Grid.SetRow(background, number);
            this.background = background;
            return background;
        }

        /*
         * Create the text element for the UI.
         * @param int number - the position of the word within the list of words from the Board.cs
         */
        public TextBlock getText(int number)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Name = "textBlock_" + this.value;
            textBlock.Text = this.value;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.MinWidth = background.ActualWidth;
            textBlock.MinHeight = background.ActualHeight;
            textBlock.FontSize = 20;
            textBlock.Tapped += clicked;
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, number);
            this.text = textBlock;
            return this.text;
        }

        #endregion

        #region Events

        // Only for development!
        private void clicked(object sender, TappedRoutedEventArgs e)
        {
            if (this.chars[0].tile.background.Background == Colors.green)
            {
                this.background.Background = background.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
                foreach (Char c in this.chars)
                {
                    if (c.tile != null)
                    {
                        c.tile.background.Background = Colors.blue;
                    }
                }
            }
            else
            {
                this.background.Background = background.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
                foreach (Char c in this.chars)
                {
                    if (c.tile != null)
                    {
                        c.tile.background.Background = Colors.green;
                    }
                }
            }
        }

        /*
         * When the state changed of this word do something to save it.
         * @called - When the used clicked a tile.
         */ 
        public Boolean stateChange()
        {
            int activeCount = 0;
            // Check if all the characters are active.
            foreach(Char c in this.chars){
                if (c.active.Equals(true))
                {
                    activeCount++;
                }
            }
            if(activeCount == this.chars.Length){
                this.founded = true;
                // Remove this word from all the tiles.
                foreach (Char c in this.chars)
                {
                    // TODO: Alert the tiles.
                    c.tile.completed = true;
                    c.tile.active = false;
                }
                // TODO: Alert the other words that using these tiles to.
                Boggle.board.resetClicked(this);
                this.background.Background = Colors.blue;
                return true;
            }
            return false;
        }

        #endregion

    }
}
