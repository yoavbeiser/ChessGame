using System;
using System.Collections.Generic;

namespace chess
{
    public class Board
    {
        #region Properties
        private readonly ITool[,] _tools;
        private readonly Player _player1, _player2;
        private readonly IList<ITool> _player1Tools,_player2Tools;
        private readonly int _boardSize;
        private readonly Player _playerTurn;
        #endregion

        #region Ctor
        public Board(ITool[][] tools, Player player1, Player player2,int boardSize, Player playerTurn)
        {
            _player1 = player1;
            _player2 = player2;
            _boardSize = boardSize;
            _playerTurn = playerTurn;
            _player1Tools = BuildPlayerTools();
            _player2Tools = BuildPlayerTools();
            _tools = BuildBoard();
        }

        #endregion

        #region Gets

        public Player GetPlayerTurn()
        {
            return _playerTurn;
        }
        public int GetBoardSize()
        {
            return _boardSize;
        }
        public Player GetPlayer1()
        {
            return _player1;
        }
        public Player GetPlayer2()
        {
            return _player2;
        }
        public IList<ITool> GetPlayer1Tools()
        {
            return _player1Tools;
        }
        public IList<ITool> GetPlayer2Tools()
        {
            return _player2Tools;
        }
        public ITool[,] GetBoard()
        {
            return _tools;
        }
        #endregion

        #region Methods
        private ITool[,] BuildBoard()
        {
            ITool[,] tools = new ITool[GetBoardSize(),GetBoardSize()];
            for (int row = 0; row < GetBoardSize(); row++)
            {
                for (int colum = 0; colum < GetBoardSize(); colum++)
                {
                    tools[row, colum] = new NullTool();
                }
            }
            return tools;
        }

        public void RemoveToolFromPlayer1List(int row,int colum)
        {
            ITool tool = _tools[row, colum];
            IList<ITool> toolList = GetPlayer1Tools();
            toolList.Remove(tool);
        }
        public void RemoveToolFromPlayer2List(int row,int colum)
        {
            ITool tool = _tools[row, colum];
            IList<ITool> toolList = GetPlayer2Tools();
            toolList.Remove(tool);
        }
        private IList<ITool> BuildPlayerTools()
        {
            IList<ITool> tools = new List<ITool>();
            for (int i = 0; i < GetBoardSize(); i++)
            {
                tools.Add(new Runner());
            }
            tools.Add(new Queen());
            tools.Add(new King());
            return tools;
        }

        public void AddTool(ITool tool, int row, int colum)
        {
            if (row>=GetBoardSize() || colum>= GetBoardSize() || !_tools[row,colum].Equals(new NullTool()))
                Console.WriteLine($"Can not add {tool.GetName()} to this place");
            else
                _tools[row, colum] = tool;
        }

        private void RemoveTool(int row, int colum)
        {
            if (row >= GetBoardSize() || colum >= GetBoardSize() || _tools[row, colum].Equals(new NullTool()))
            {
                Console.WriteLine("Can not remove tool from this position");
                if (GetPlayerTurn().Equals(GetPlayer1()))
                    RemoveToolFromPlayer1List(row,colum);
                else
                    RemoveToolFromPlayer2List(row,colum);
            }
            else
                _tools[row, colum] = new NullTool();
        }

        public void MoveTool(Player player,ITool tool, int row, int colum, Direction direction, int steps)
        {
            if (!player.Equals(GetPlayerTurn())) return;
            
            if(IsPlaceOk(row,colum) &&!_tools[row,colum].Equals(new NullTool()))
            {
                RemoveTool(row,colum);
            }
            Tuple<Direction,int> toolNewPlace =tool.Move(direction, steps);
            DecideDirectionForTool(player,tool,row,colum,toolNewPlace.Item1,toolNewPlace.Item2);
        }

        private void DecideDirectionForTool(Player player, ITool tool, int row, int colum, Direction direction,
            int steps)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveUp(player,tool,row,colum,steps);
                    break;
                case Direction.Down:
                    MoveDown(player,tool,row,colum,steps);
                    break;
                case Direction.Right:
                    MoveRight(player,tool,row,colum,steps);
                    break;
                case Direction.Left:
                    MoveLeft(player,tool,row,colum,steps);
                    break;
                case Direction.UpRight:
                    MoveUpRight(player,tool,row,colum,steps);
                    break;
                case Direction.UpLeft:
                    MoveUpLeft(player,tool,row,colum,steps);
                    break;
                case Direction.DownRight:
                    MoveDownRight(player,tool,row,colum,steps);
                    break;
                case Direction.DownLeft:
                    MoveDownLeft(player,tool,row,colum,steps);
                    break;
            }
        }

        private void MoveUp(Player player,ITool tool,int oldRow,int oldColum,int steps)
        {
            _tools[oldRow, oldColum] = new NullTool();
            if (player.Equals(GetPlayer1()))
            {
                if (!IsPlaceOk(oldRow, oldColum - steps))
                {
                    Console.WriteLine("Cant move to that place");
                    return;
                }
                _tools[oldRow, oldColum - steps] = tool;
            }
            else
            {
                if (!IsPlaceOk(oldRow, oldColum + steps))
                {
                    Console.WriteLine("Cant move to that place");
                    return;
                }
                _tools[oldRow, oldColum + steps] = tool;
            }
        }

        private void MoveDown(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveUp(player,tool,oldRow,oldColum,-steps);
        }

        private void MoveLeft(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            _tools[oldRow, oldColum] = new NullTool();
            if (player.Equals(GetPlayer1()))
            {
                if (!IsPlaceOk(oldRow - steps, oldColum))
                {
                    Console.WriteLine("Cant move to that place");
                    return;
                }
                _tools[oldRow - steps, oldColum] = tool;
            }
            else
            {
                if (!IsPlaceOk(oldRow + steps, oldColum))
                {
                    Console.WriteLine("Cant move to that place");
                    return;
                }
                _tools[oldRow + steps, oldColum] = tool;
            }
        }

        private void MoveRight(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveLeft(player,tool,oldRow,oldColum,-steps);
        }

        private void MoveUpRight(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveRight(player,tool,oldRow,oldColum,steps);
            MoveUp(player,tool,oldRow,oldColum,steps);
        }
        private void MoveUpLeft(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveLeft(player,tool,oldRow,oldColum,steps);
            MoveUp(player,tool,oldRow,oldColum,steps);
        }
        
        private void MoveDownRight(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveRight(player,tool,oldRow,oldColum,steps);
            MoveDown(player,tool,oldRow,oldColum,steps);
        }
        private void MoveDownLeft(Player player, ITool tool, int oldRow, int oldColum, int steps)
        {
            MoveLeft(player,tool,oldRow,oldColum,steps);
            MoveDown(player,tool,oldRow,oldColum,steps);
        }

        private bool IsPlaceOk(int row,int colum)
        {
            return row < GetBoardSize() && row >= 0 && colum < GetBoardSize() && colum >= 0;
        }
        public void PrintBoard()
        {
            for (int i = 0; i < GetBoardSize(); i++)
            {
                for (int j = 0; j < GetBoardSize(); j++)
                {
                    if (_tools[i,j].GetName()=="null")
                    {
                        Console.Write("*");
                    }
                    Console.Write($"| {_tools[i, j].GetName()}   ");
                }
                Console.WriteLine();
            }
        }

        private bool IsBoardsTheSame(Board board1, Board board2)
        {
            if (board1.GetBoardSize() != board2.GetBoardSize()) return false;
            ITool[,] tools1 = board1.GetBoard(), tools2 = board2.GetBoard();
            int boardSize = board1.GetBoardSize();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (!tools1[i, j].Equals(tools2[i, j])) return false;
                }
            }
            return true;
        }

        #endregion

        #region OverrideMethods

        public override bool Equals(object obj)
        {
            Board newObj = (Board)obj;
            return newObj!=null
                && newObj.GetBoardSize()==this.GetBoardSize()
                && newObj.GetPlayer1().Equals(this.GetPlayer1())
                && newObj.GetPlayer2().Equals(this.GetPlayer2())
                && IsBoardsTheSame(newObj,this);
        }

        public override string ToString()
        {
            return $"This board is played by {GetPlayer1().ToString()} and {GetPlayer2().ToString()}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_player1,_player2,_tools,_boardSize,_player1Tools,_player2Tools,_playerTurn);
        }
        #endregion
    }
}