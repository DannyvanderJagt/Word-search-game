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
        // Display element.
        private Grid background;
        private TextBlock text;

        // Class instance data.
        private int x = -1;
        private int y = -1;
        private String value = "a".ToString();

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.value = (x + "" + y).ToString();
        }

        /*
         * Create the display element: background.
         * @used this.x, this.y
         * @savedAt this.background
         * @return Grid this.background - The background element.
         */
        public Grid getBackground(){
            Grid background = new Grid();
            background.Name = "grid_" + this.x + "," + this.y;
            background.Background = new SolidColorBrush(Color.FromArgb(255, 215, 208, 94));
            background.Margin = new Thickness(6);
            //subGrid.Tapped += textBlock_subGrid_Tapped;
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
            textBlock.Name = "textBlock_" + this.x + "_" + this.y;
            textBlock.Text = this.value;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.MinWidth = background.ActualWidth;
            textBlock.MinHeight = background.ActualHeight;
            textBlock.FontSize = 24;
            // Click handler!
           // textBlock.Tapped += clicked;
            Grid.SetColumn(textBlock, this.x);
            Grid.SetRow(textBlock, this.y);
            this.text = textBlock;
            return this.text;
        }


        






    }
}
