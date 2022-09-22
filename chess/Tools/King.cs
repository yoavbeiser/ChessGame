using System;

namespace chess
{
    public class King: ITool
    {
        public King()
        {
        }
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            if(steps>1)
            {
                throw new NotImplementedException();
            }
            return new Tuple<Direction, int>(direction, steps);
        }
        public string GetName()
        {
            return "King";
        }
        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }
    }
}