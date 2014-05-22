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
        public Word word; // The base word where this character is from.
        private int position = -1; // The position of this charcter within the value of the base word.
        public String value; // The value of this character. ! Only 1 character long!
       // public int x = -1;
       // public int y = -1;
        public Tile tile = null; // The tile where this character is placed at in the UI.
        public Boolean active = false; // Store if the user has clicked/selected the tile with this character.
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
        /*
         * Place the word into the board/tile.
         * @param Tile tile - The tile where this character must be placed at.
         */
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

        /*
         * Unplace the word from the board/tile.
         */
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

        /*
         * Make this character active. (The user has clicked the tile with this character)
         */
        public Boolean setActive(){
            this.active = true;
            return this.word.stateChange();
        }

        /*
         * Make this character inactive. (The user has been clicked again)
         */
        public Boolean setInactive()
        {
            this.active = false;
            return this.word.stateChange();
        }


        #endregion  

    }
}
