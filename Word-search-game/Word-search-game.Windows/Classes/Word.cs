using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    class Word
    {
        public String value = null;
        public int length = -1;
        public Char[] chars;
        private int direction = 0;

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

        // Place the word.
        public Boolean place(int x, int y, int pos)
        {
            Boolean result;
            
            // Place the first letter.
            Boolean first = this.chars[pos].place(x, y);
            
            if (first)
            {
                if (pos == 0)
                {
                    result = this.placeForward(pos);
                }
                else if (pos == this.chars.Length - 1)
                {
                    result = this.placeBackward(pos);
                }
                else
                {
                    result = this.placeMiddle(pos);
                }
                // Check the result.
                if (!result)
                {
                    this.unplaceAll();
                }
                return result;
            }
            else
            {
                this.unplaceAll();
            }
            return false;
        }

        // UnplaceAll.
        public Boolean unplaceAll()
        {
            int count = 0;
            for (int i = 0, len = this.chars.Length; i < len; i++)
            {
                Char l = this.chars[i];
                if (l.x != -1 && l.y != -1)
                {
                    if (l.unplace())
                    {
                        count++;
                    }
                }
            }
            if (count == this.chars.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Try to place all the letters of the word after a predefined position.
        public Boolean placeForward(int pos)
        {
            // this.chars.Length
            for (int i = pos + 1, len = this.chars.Length ; i < len; i++)
            {
                Boolean run = true;
                while (run)
                {
                    Char lcurrent = this.chars[i];
                    Char lback = this.chars[i - 1];
                    int[] space = this.findSpace(lback, lback.x, lback.y);

                    // Check if the letter can be placed.

                    if (space != null)
                    {
                        lcurrent.place(space[0], space[1]); // x , y
                        run = false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Try to place all the letters of the word before a predefined position.
        public Boolean placeBackward(int pos)
        {
            for (int i = pos - 1, len = 0; i >= len; i--)
            {
                Boolean run = true;
                while (run)
                {
                    Char lcurrent = this.chars[i];
                    Char lback = this.chars[i + 1];
                    int[] space = this.findSpace(lback, lback.x, lback.y);
                    // Check if the letter can be placed.
                    if (space != null)
                    {
                        lcurrent.place(space[0], space[1]); // x , y
                        run = false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Try to place all the letters of the word before and after a predefines position.
        public Boolean placeMiddle(int pos)
        {
            Boolean back = this.placeBackward(pos);
            Boolean front = false;
            if (back)
            {
                front = this.placeForward(pos);
            }
            if (back && front)
            {
                return true;
            }
            return false;
        }

        // Findspace. (WATCH OUT COULD BE AN ERROR HERE!).
        public int[] findSpace(Char character, int x, int y)
        {
            // Directions;
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

            // Directions that can be used. (From the settings).
            int[] dirs = Boggle.settings.directions;
            int[] result =  find(this.direction, dirs, directions, character);
            return result;
        }


        private int[] find(int dir, int[] dirs, int[][] directions, Char character)
        {
            int[] vd = directions[dirs[dir]];

            Tile tile = Boggle.board.tiles[0, 0];
            Boolean inbound = false;
            if (vd[0] >= 0 && vd[0] <= Boggle.board.columns-1 && vd[1] >= 0 && vd[1] <= Boggle.board.rows-1)
            {
                inbound = true;
                tile = Boggle.board.tiles[vd[0], vd[1]];
            }
            if (inbound != false)
            {
                if (tile.value == character.value || tile.value == null)
                {
                    return vd;
                }
                else
                {
                    // Find for next dir.
                    dir++;
                    if (dir >= dirs.Length)
                    {
                        return null;
                    }
                    return find(dir, dirs, directions, character);
                }
            }
            else
            {
                // Find for next dir.
                dir++;
                if (dir >= dirs.Length)
                {
                    return null;
                }
                return find(dir, dirs, directions, character);
            }
        }

    }
}
