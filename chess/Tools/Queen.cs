using System;

namespace chess
{
    public class Queen: ITool
    {
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            return new Tuple<Direction, int>(direction, steps);
        }

        public string GetName()
        {
            return "Queen";
        }
        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }
    }
}