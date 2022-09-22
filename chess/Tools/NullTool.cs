using System;

namespace chess
{
    public class NullTool: ITool
    {
        public Tuple<Direction, int> Move(Direction direction, int steps)
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "null";
        }

        public override bool Equals(object obj)
        {
            ITool newObj = (ITool)obj;
            return newObj!=null
                && newObj.GetName()==this.GetName();
        }
    }
}