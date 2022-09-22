using System;

namespace chess
{
    public class Runner: ITool
    {
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            if(direction is Direction.UpRight or Direction.UpLeft or Direction.DownLeft or Direction.DownRight)
            {
                return new Tuple<Direction, int>(direction, steps);
            }
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "Runner";
        }
        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }
    }
}