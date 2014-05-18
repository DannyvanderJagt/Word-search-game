using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    class Char
    {
        public Word word;
        private int position = -1;
        public String value;
        public int x = -1;
        public int y = -1;
        public Tile tile;

        /*
         * Constructor
         * @param Word word - A reference to the word where this char is a part of.
         * @param int position - The position of this char in the value of the word.
         * @savedAt this.word, this.position
         */
        public Char(Word word, int position)
        {
            this.word = word;
            this.position = position;
            this.value = new String(this.word.value[this.position],1);
        }


        public Boolean place(int x, int y)
        {
            this.x = x;
            this.y = y;
            // Assign a tile.
            this.tile = Boggle.board.tiles[x, y].place(this);
            return true;
        }

        public Boolean unplace()
        {
            this.x = -1;
            this.y = -1;
            if (this.tile != null)
            {
                this.tile.unplace(this.word.value);
            }
            return true;
        }
    }
}
