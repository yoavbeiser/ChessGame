using System;

namespace chess
{
    public interface ITool
    {
        public Tuple<Direction,int> Move(Direction direction,int steps);
        public string GetName();
    }
}