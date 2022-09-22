using System;

namespace chess
{
    public class Steeple: ITool
    {
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            if (direction is Direction.Right or Direction.Left or Direction.Up or Direction.Down)
            {
                return new Tuple<Direction, int>(direction, steps);
            }
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "Steeple";
        }
        
        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }
    }
}