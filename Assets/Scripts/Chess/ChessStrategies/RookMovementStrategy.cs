using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class RookMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();
            Vector2Int[] directions =
            {
                Vector2Int.down, 
                Vector2Int.left, 
                Vector2Int.up, 
                Vector2Int.right, 
            };
            
            foreach (var direction in directions)
            {
                for (int dir = 1; dir < 8; dir++)
                {
                    Vector2Int newPos = currentPosition + dir * direction;
                    if (board.IsPositionValid(newPos) && !board.IsPositionOccupied(newPos))
                    {
                        possibleMoves.Add(newPos);
                    }
                }
                return possibleMoves;
            }
            return possibleMoves;
        }
    }
}
