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
        #region Varibales
        // Some variables.
        public int rows = 0;
        public int columns = 0;
        private int height = 75; // The width and height of a tile when it is displayed.
        private int width = 75;
        public Tile[,] tiles; // This will hold all the boxes that the board will contain.

        // Display element.
        private Grid grid;

        #endregion

        #region Constructor
        /*
         * Constructor.
         * @param int rows - The number of rows the board needs to be. (y-axis, height)
         * @param int columns - The number of columns the board needs to be. (x-axis, width)
         * @savedAt this.rows, this.colums, this.panel
         */
        public Board(int rows, int columns)
        {
            if (rows > 0 && columns > 0)
            {
                // Set the data.
                this.rows = rows;
                this.columns = columns;
                System.Diagnostics.Debug.WriteLine("Board"+this.rows+""+this.columns);
                // Initialize the array.
                this.tiles = new Tile[rows, columns];
                // Create the tiles.
                this.createTiles();
            }
        }
        #endregion

        #region Create
        /*
         * Create all the empty tiles.
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
        #endregion

        #region Display
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
        public Grid show()
        {
            // Create the board elements.
            this.createGrid();
            for (int xi = 0; xi < this.columns; xi++) {
                for (int yi = 0; yi < this.rows; yi++)
                {
                    Tile tile = this.tiles[xi, yi];
                    Grid background = tile.getBackground();
                    background.Tapped += clicked;
                    TextBlock text = tile.getText();
                    text.Tapped += clicked;
                    grid.Children.Add(background);
                    grid.Children.Add(text);
                }
            }
            return grid;
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
        #endregion

        #region Events
        /*
         * Click handler for when an tile is clicked.
         * TODO: Make this work!
         */
        public void clicked(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("clicked!"+sender);
        }
        
#endregion

        #region Search
        // Search.
        // Search for all the letters from the word.
        // Output: List<int[]> -> int[]{x,y,indexOf}
        public List<int[]> search(String letters)
        {
            List<int[]> positionsFilled = new List<int[]>();
            List<int[]> positionEmpty = new List<int[]>();

            for (int xi = 0, xlen = this.columns; xi < xlen; xi++)
            {
                for (int yi = 0, ylen = this.rows; yi < ylen; yi++)
                {
                    Tile box = this.tiles[xi, yi];
                    // Check if there are letters that match!
                    if (!String.IsNullOrEmpty(box.value) && letters.IndexOf(box.value) != -1)
                    {
                        positionsFilled.Add(new int[3] { xi, yi, letters.IndexOf(box.value) });
                    }
                    else if (String.IsNullOrEmpty(box.value))
                    {
                        positionEmpty.Add(new int[3] { xi, yi, 0});
                    }
                }
            };

            // Combine.
            for (int i = 0, len = positionEmpty.Count; i < len; i++)
            {
                positionsFilled[positionsFilled.Count-1] = positionEmpty[i];
            }
            return positionsFilled;

        }
        #endregion

        #region Check
        // Check if the tile is availible for the requested char.
        public Boolean check(int x, int y, Char character)
        {
            if (this.tiles[x, y].value == character.value || String.IsNullOrEmpty(this.tiles[x,y].value))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Space
        // Posibile directions.



        public int[,] space(int x, int y, String value)
        {
            





            return new int[,]{{0,0}};
        #endregion
    }
}
