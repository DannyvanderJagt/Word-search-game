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
        private Boggle boggle;

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
        public Board(int rows, int columns, Boggle boggle)
        {
            if (rows > 0 && columns > 0)
            {
                // Set the data.
                this.boggle = boggle;
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
            String name;
            if (sender as Grid != null)
            {
                Grid elem = (Grid)sender;
                name = elem.Name;
            }
            else
            {
                TextBlock elem = (TextBlock)sender;
                name = elem.Name;
            }
            // Split the name and get the positions.
            string[] positions = name.Split(',');
            int x = Convert.ToInt32(positions[0]);
            int y = Convert.ToInt32(positions[1]);

            // Get the tile.
            Tile tile = this.tiles[x, y];
            if (canBeClicked(x, y).Equals(true))
            {
                if (tile.active.Equals(true))
                {
                    this.totalActive--;
                }
                else if(tile.active.Equals(false))
                {
                    this.totalActive++;
                }
                tile.clicked();
                if (this.totalActive.Equals(0))
                {
                    this.lastX = -1;
                    this.lastY = -1;
                }

                this.setColor(tile);
            }
        }

        private int lastX = -1; // The x position of the latest clicked tile.
        private int lastY = -1; // The y position of the latest clicked tile.
        private int totalActive = 0;
        
        /*
         * When a word is completed reset all the tile and there characters.
         * @param Word word - The word that is completed. We abstract all the characters and there tile to reset all the other tiles/chars.
         * @action - Check if all the words are completed.
         */
        public void resetClicked(Word word)
        {
            this.lastX = -1;
            this.lastY = -1;
            this.totalActive = 0;
            // Reset all the other words.
            foreach(Char c in word.chars)
            {
                // Reset all the other words.
                c.tile.reset();
                this.setColor(c.tile);
            }
            // Check if all the words are founded.
            this.checkCompleted();
        }


        /*
         * Give a tile a color accourding to the active/completed state of the tile.
         * @param Tile tile - The tile that has to be colored.
         */
        private void setColor(Tile tile)
        {
            if (tile.active.Equals(true))
            {
                tile.background.Background = Colors.yellow;
            }
            else if (tile.completed.Equals(true))
            {
                tile.background.Background = Colors.blue;
            }
            else
            {
                tile.background.Background = Colors.green;
            }
        }

        /*
         * Check if this tile/position can be clicked.
         * @param int x - The x position that has to be checked.
         * @param int y - The y position that has to be checked.
         * TODO: Check the checking according to direction of the selected level.
         */
        private Boolean canBeClicked(int x, int y)
        {
            if (lastX == -1 && lastY == -1)
            {
                // First time
                lastX = x;
                lastY = y;
                return true;
            }
            else 
            { 
                if((x == lastX || x == lastX - 1 || x == lastX +1) && (y == lastY || y == lastY -1 || y == lastY+1)){
                    lastX = x;
                    lastY = y;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Search
        /* 
         * Search the board for spaces where the chars of the word could be placed.
         * 
         * @param String letters - The value of a word.
         * @Output: List<int[]> -> int[]{x,y,indexOf}
         */
        public List<int[]> search(Word word)
        {
            String letters = word.value;
            List<int[]> positionsFilled = new List<int[]>();
            List<int[]> positionEmpty = new List<int[]>();

            for (int xi = 0, xlen = this.columns; xi < xlen; xi++)
            {
                for (int yi = 0, ylen = this.rows; yi < ylen; yi++)
                {
                    Tile tile = this.tiles[xi, yi];
                    // Check if there are letters that match!
                    int indexOf = tile.value != null ? letters.IndexOf(tile.value) : -1;
                    if (!String.IsNullOrEmpty(tile.value) && indexOf != -1)
                    {
                        if (tile.check(word.chars[indexOf]).Equals(true))
                        {
                            positionsFilled.Add(new int[3] { xi, yi, letters.IndexOf(tile.value) });
                        }
                    }
                    else if (String.IsNullOrEmpty(tile.value))
                    {
                        positionEmpty.Add(new int[3] { xi, yi, 0});
                    }
                }
            };

            // Combine.
            foreach (int[] posEmpty in positionEmpty)
            {
                positionsFilled.Add(posEmpty);
            }
            return positionsFilled;

        }
        #endregion

        #region Check
        /*
         * Check if the tile is availible for the requested char.
         * 
         * @param int x - The x position of the place that has to be checked.
         * @param int y - The y position of the place that has to be checked.
         */
        public Boolean check(int x, int y, Char character)
        {
            if (this.tiles[x, y].value == character.value || String.IsNullOrEmpty(this.tiles[x,y].value))
            {
                return true;
            }      
            return false;
        }

        /*
         * Check if all the words are completed.
         */
        private Boolean checkCompleted()
        {
            Word[] words = this.boggle.words;
            // Loop through the words.
            foreach (Word word in words)
            {
                if (word.founded.Equals(false))
                {
                    return false;
                }
            }
            System.Diagnostics.Debug.WriteLine("All the words are found!");
            return true;
        }

        #endregion

        #region Space

        /*
         * Check for spaces/tiles where the character could fit.
         * @param int x - The x position of the latest placed character.
         * @param int y - The y position of the latest placed character.
         */
        public List<int[]> space(int x, int y, Char character)
        {
            // Posible directions.
            int[][] directions = new int[][]{
                new int[]{x+1,y}, // Right.
                new int[]{x-1,y}, // Left.
                new int[]{x,y-1}, // Top.
                new int[]{x,y+1}, // Bottom.
                new int[]{x-1,y-1}, // Topleft.
                new int[]{x+1,y-1}, // Topright.
                new int[]{x-1,y+1}, // Bottomleft.
                new int[]{x+1,y+1}  // Bottomright.
            };

            // Get the directions from the settings.
            int[] settingsDirections = Boggle.settings.directions; // Ex: 1,2,3,4 or 4,5,6,7 or a combination.
            List<int[]> possibilities = new List<int[]>();

            // Find the tiles that can be a fit.
            foreach (int i in settingsDirections) {
                // Check if this tile is a fit.
                int[] dir = directions[i];

                // Check if the x and y are wihtin the boards boundries.
                if (dir[0] >= 0 && dir[0] < this.columns && dir[1] >= 0 && dir[1] < this.rows)
                {
                    if (this.check(dir[0], dir[1], character) == true)
                    {
                        possibilities.Add(new int[] { dir[0], dir[1] });
                    }
                }
            }

            return possibilities;
        }
        #endregion
    }
}
