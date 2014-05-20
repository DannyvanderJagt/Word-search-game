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
    class Tile
    {
        #region Variables
        // Display element.
        public Grid background;
        private TextBlock text;

        // Class instance data.
        private int x = -1;
        private int y = -1;
        public String value;
        private Dictionary<String, Char> chars = new Dictionary<String, Char>();
        private Boolean active = false;
        #endregion

        #region Constructor
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
            // Click handler!
            Grid.SetColumn(textBlock, this.x);
            Grid.SetRow(textBlock, this.y);
            this.text = textBlock;
            return this.text;
        }

        #endregion

        #region Place

        // Done.
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

        private Boolean addChar(Char character)
        {
            this.chars.Add(character.word.value, character);
            if (this.chars.Count == 1)
            {
                this.value = character.value;
            }
            return true;
        }

        public Boolean removeChar(Char character)
        {
            if (this.chars.ContainsKey(character.word.value))
            {
                this.chars.Remove(character.word.value);
                return true;
            }
            return false;
        }

        #endregion

        #region Events

        // Comes from the board class.
        public void clicked()
        {
            System.Diagnostics.Debug.WriteLine("Active"+this.active);
            if (this.active.Equals(false))
            {
                this.background.Background = Colors.yellow;
                foreach(String s in chars.Keys){
                    this.chars[s].setActive();
                }
                this.active = true;
                // Change the color.
                
            }
            else
            {
                this.background.Background = Colors.green;
                // Alert the words that this tile is deselected.
                foreach (String s in chars.Keys)
                {
                    this.chars[s].setInactive();
                }
                this.active = false;
            }
        }

        #endregion

    }
}
