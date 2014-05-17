﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_search_game.Classes
{
    static class Levels
    {
        public static String[] types = new String[]{"easy","medium","hard","expert"};
        public static Level[] easy = new Level[]{
            new Level(
                5,
                5,
                10,
                new String[]{"left","right","top","bottom"},
                60
            ),
            new Level(
                6,
                6,
                12,
                new String[]{"left","right","top","bottom"},
                60
            )
        };
    }

    class Level
    {
        public int columns = 0;
        public int rows = 0;
        public int words = 0;
        public String[] directions;
        public int time = 0;

        public Level(int columns, int rows, int words, String[] directions, int time)
        {
            this.columns = columns;
            this.rows = rows;
            this.words = words;
            this.directions = directions;
            this.time = time;
        }

    }
}
