using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class KingMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();
            
            //check all possible tiles the king can move
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0) continue;

                    Vector2Int newPos = new Vector2Int(currentPosition.x + i, currentPosition.y + j);

                    if (!board.IsPositionOccupied(newPos) && board.IsPositionValid(newPos))
                    {
                        possibleMoves.Add(newPos);
                    }
                }
            }
            return possibleMoves;
        }
        
        public bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board)
        {
            // Kings attack one square in any direction
            int dx = Mathf.Abs(targetPosition.x - currentPosition.x);
            int dy = Mathf.Abs(targetPosition.y - currentPosition.y);
            
            // King can attack if target is one square away in any direction
            return dx <= 1 && dy <= 1 && !(dx == 0 && dy == 0);
        }
        
    }
}
