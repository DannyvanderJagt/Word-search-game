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


        public Boolean place(Tile tile)
        {
            Boolean result = tile.place(this);
            if (result == true)
            {
                this.tile = tile;
                return true;
            }
            return false;
        }

        public Boolean unplace()
        {
            return false;
        }
    }
}
