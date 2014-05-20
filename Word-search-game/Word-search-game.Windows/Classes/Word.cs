using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    class Word
    {
        #region Variables
        public String value = null;
        public int length = -1;
        public Char[] chars;
        private int direction = 0;
        public Tile tile;
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
        // Place the word.
        public Boolean place(int x, int y, int pos)
        {
            //System.Diagnostics.Debug.WriteLine("Place"+x+":"+y+":"+pos);
           
            // ---- Place the first character ---- //
            // Check if the requested tile is suitable.
            Boolean space = Boggle.board.check(x,y,this.chars[pos]);
           // System.Diagnostics.Debug.WriteLine("Space"+space);

            // Place the first character.
            Boolean placed = this.chars[pos].place(Boggle.board.tiles[x,y]);
            //System.Diagnostics.Debug.WriteLine("First placed at:"+x+":"+y);

            // Current x and y pos.
            int curX = x;
            int curY = y;

            // ---- Place the other characters ---- //
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
            curX = x;
            curY = y;

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
            //System.Diagnostics.Debug.WriteLine("Placed: " + placed);
            return true;
        }


        private int[] placeChar(int curX, int curY, Char character)
        {
            System.Diagnostics.Debug.WriteLine(character.value);
            // Find space.
            System.Diagnostics.Debug.WriteLine("CurX:" + curX + ": CurY" + curY);
            List<int[]> spaces = Boggle.board.space(curX, curY, character);
            System.Diagnostics.Debug.WriteLine("Spaces: " + spaces.Count);
            // Check.
            for (int si = 0, slen = spaces.Count; si < slen; si++)
            {
                Boolean result = Boggle.board.check(spaces[si][0], spaces[si][1], character);
                if (result == true)
                {
                    // Place
                    System.Diagnostics.Debug.WriteLine("Placed ---> " + character.value);
                    character.place(Boggle.board.tiles[spaces[si][0], spaces[si][1]]);
                    curX = spaces[si][0];
                    curY = spaces[si][1];
                    slen = spaces.Count;
                    return new int[]{curX, curY}; // Same as return true.
                }
            }
            return null; // Same as return false.
        }

        // UnplaceAll.
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
    }
}
