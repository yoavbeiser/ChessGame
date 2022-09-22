using System;

namespace chess
{
    public class Player
    {
        #region Properties
        private readonly string _name;
        
        #endregion

        #region Ctor
        public Player(string name)
        {
            _name = name;
        }
        #endregion
        

        #region Gets

        public string GetName()
        {
            return _name;
        }

        #endregion
        

        #region OverrideMethods

        public override string ToString()
        {
            return $"player {GetName()}";
        }

        public override bool Equals(object obj)
        {
            Player newObj = (Player)obj;
            return newObj!=null
                   && newObj.GetName()==this.GetName();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }

        #endregion
        
    }
}