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
            System.Diagnostics.Debug.WriteLine("Place"+x+":"+y+":"+pos);
           
            // ---- Place the first character ---- //
            // Check if the requested tile is suitable.
            Boolean space = Boggle.board.check(x,y,this.chars[pos]);
            System.Diagnostics.Debug.WriteLine("Space"+space);

            // Place the first character.
            Boolean placed = this.chars[pos].place(Boggle.board.tiles[x,y]);

            // Current x and y pos.
            int curX = x;
            int curY = y;

            // ---- Place the other characters ---- //
            for (int fi = pos - 1, flen = 0; fi >= flen; fi--)
            {
                System.Diagnostics.Debug.WriteLine(this.chars[fi].value);
                // Get char.
                Char character = this.chars[fi];
                // Find space.
                Boggle.board.space(curX, curY, character.value);
                // Check.
                // Place.
                // Next.
            }

            for (int bi = pos + 1, blen = this.chars.Length; bi < blen; bi++)
            {
                System.Diagnostics.Debug.WriteLine(this.chars[bi].value);
            }
            System.Diagnostics.Debug.WriteLine("Placed: " + placed);

            return false;
        }

        // UnplaceAll.
        public Boolean unplaceAll()
        {
            //int count = 0;
           /* for (int i = 0, len = this.chars.Length; i < len; i++)
            {
                Char l = this.chars[i];
                l.unplace();
            }*/
            return false;
        }
        #endregion
    }
}
