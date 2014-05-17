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
    class Board
    {
        // Some variables.
        private int rows = 0;
        private int columns = 0;
        private int height = 75; // The width and height of a tile when it is displayed.
        private int width = 75;
        private Tile[,] tiles; // This will hold all the boxes that the board will contain.

        // Display element.
        private Grid grid;
        
        /*
         * Constructor.
         * @param int rows - The number of rows the board needs to be. (y-axis, height)
         * @param int columns - The number of columns the board needs to be. (x-axis, width)
         * @savedAt this.rows, this.colums
         */
        public Board(int rows, int columns)
        {
            if (rows > 0 && columns > 0)
            {
                // Set the data.
                this.rows = rows;
                this.columns = columns;
                // Initialize the array.
                this.tiles = new Tile[rows, columns];
                // Create the tiles.
                this.createTiles();
                // Create the board elements.
                this.createGrid();
                this.show();

            }
        }

        /*
         * Create all the tiles.
         * @savedAt this.tiles.
         */
        private void createTiles()
        {
            // Loop through the height/columns to create the all to columns.
            for (int xi = 0, xlen = this.columns; xi < xlen; xi++)
            {
                // Loop through the width/rows to create all the rows.
                for (int yi = 0, ylen = this.rows; yi < ylen; yi++)
                {
                    this.tiles[xi, yi] = new Tile(xi, yi);
                }
            }
        }

        /*
         * Create the grid. 
         * @savedAt this.grid
         * @used this.width, this.height
         * @return Grid this.grid OR null
         */
        private Grid createGrid()
        {
            this.grid = new Grid();

            for (int i = 0; i < this.rows; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.MinHeight = (height > -1) ? height : 0;
                rowDefinition.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDefinition);

                rowDefinition = null;
            }

            for (int i = 0; i < this.columns; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.MinWidth = (width > -1) ? width : 0;
                columnDefinition.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(columnDefinition);

                columnDefinition = null;
            }

            if (grid.RowDefinitions.Count() == rows &&
                grid.ColumnDefinitions.Count() == columns)
            {
                return this.grid;
            }
            else
            {
                return null;
            }
        }

        /*
         * Fill and show the grid.
         * @used this.background, this.text
         */
        private void show()
        {
            for (int xi = 0; xi < this.columns; xi++) {
                for (int yi = 0; yi < this.rows; yi++)
                {
                    Tile tile = new Tile(xi, yi);
                    Grid background = tile.getBackground();
                    background.Tapped += clicked;
                    TextBlock text = tile.getText();
                    text.Tapped += clicked;
                    grid.Children.Add(background);
                    grid.Children.Add(text);
                }
            
            }
        }

        /*
         * Get the grid variable.
         * @used this.grid
         * @return Grid this.grid - The filled grid thats created out of all the tiles.
         */
        public Grid getGrid()
        {
            return grid;
        }

        /*
         * Click handler for when an tile is clicked.
         * TODO: Make this work!
         */
        public void clicked(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("clicked!"+sender);
        }


    }
}
