using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    class Char
    {
        #region Variables
        public Word word;
        private int position = -1;
        public String value;
        public int x = -1;
        public int y = -1;
        public Tile tile = null;
        public Boolean active = false;
        #endregion

        #region Constuctor
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
        #endregion

        #region Place
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
            if (this.tile != null)
            {
                this.tile.unplace(this);
                this.tile = null;
            }
            return false;
        }
        #endregion

        #region Events

        public Boolean setActive(){
            this.active = true;
            return this.word.stateChange();
        }
        public Boolean setInactive()
        {
            this.active = false;
            return this.word.stateChange();
        }


        #endregion  

    }
}
