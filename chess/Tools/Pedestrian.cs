using System;

namespace chess
{
    public class Pedestrian: ITool
    {
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            if(direction!= Direction.Up || steps is < 1 or > 2)
            {
                throw new NotImplementedException();
            }
            return new Tuple<Direction, int>(direction, steps);
        }

        public string GetName()
        {
            return "Pedestrian";
        }
        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }
    }
}