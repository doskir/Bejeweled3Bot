﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bejeweled3Bot
{
    internal class Move
    {
        public int FromRow;
        public int FromColumn;
        public int ToRow;
        public int ToColumn;

        public Move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            FromRow = fromRow;
            FromColumn = fromColumn;
            ToRow = toRow;
            ToColumn = toColumn;

        }
    }
}