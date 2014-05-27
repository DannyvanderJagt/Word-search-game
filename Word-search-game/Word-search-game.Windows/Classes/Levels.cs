using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Word_search_game.Classes
{
    static class Levels
    {
        /*
         * The level "array" with all the settings.
         */
        public static String[] types = new String[]{"easy","immediate","expert"};

        public static Dictionary<String, Level[]> data = new Dictionary<String, Level[]>()
        {
            {"easy", new Level[]{
                new Level(
                    8, // width - columns
                    8, // height - rows
                    20, // number of words.
                    new int[]{0,1,2,3}, // Directions
                    60 // Time
                ),
                new Level(
                    6,
                    6,
                    12,
                    new int[]{0,1,2,3},
                    60
                )
                }// End of Level[].
            },// End of easy.
            {"immediate", new Level[]{
                 new Level(
                    8,
                    8,
                    20,
                    new int[]{4,5,6,7},
                    60
                ),
                new Level(
                    6,
                    6,
                    12,
                    new int[]{4,5,6,7},
                    60
                )
            }}, // End of immediate.
            {"expert", new Level[]{
                 new Level(
                    8,
                    8,
                    20,
                    new int[]{0,1,2,3,4,5,6,7},
                    60
                ),
                new Level(
                    6,
                    6,
                    12,
                    new int[]{0,1,2,3,4,5,6,7},
                    60
                )
            }}// End of expert.
        };
       
     

    }

    // A format for storing all the settings for each level.
    class Level
    {
        public int columns = 0;
        public int rows = 0;
        public int words = 0;
        public int[] directions;
        public int time = 0;

        public Level(int columns, int rows, int words, int[] directions, int time)
        {
            this.columns = columns;
            this.rows = rows;
            this.words = words;
            this.directions = directions;
            this.time = time;
        }

    }
}
