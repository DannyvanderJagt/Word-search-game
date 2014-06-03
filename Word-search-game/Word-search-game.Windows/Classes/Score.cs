using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    static class Score
    {

        /*
         * Calculate the score for the user.
         * @param string difficulty - The name of the played difficulty.
         * @param int level - The level played.
         * @param int time - The time that is left.
         */
        static public int calculate(String difficulty, int level, int time)
        {
            // Get the settings of the difficulty/level.
            Level settings = Levels.data[difficulty][level];

            // Calculate some stuff.
            int cal1 = ((settings.columns * settings.rows) / settings.words);
            int cal2 = (cal1 * cal1) * settings.directions.Length;
            int cal3 = (cal2*settings.time) / time;

            return cal3 *100;
        }









    }
}
