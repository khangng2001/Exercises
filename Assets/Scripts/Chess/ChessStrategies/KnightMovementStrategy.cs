using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class KnightMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();

            Vector2Int[] directions =
            {
                new Vector2Int(2, 1),
                new Vector2Int(2, -1),
                new Vector2Int(-2, 1),
                new Vector2Int(-2, -1),
                new Vector2Int(1, 2),
                new Vector2Int(1, -2),
                new Vector2Int(-1, 2),
                new Vector2Int(-1,-2)
            };
            
            foreach (var direction in directions)
            {
                Vector2Int newPos = currentPosition + direction;
                if (!board.IsPositionOccupied(newPos) && board.IsPositionValid(newPos))
                {
                    possibleMoves.Add(newPos);
                }
            }
            return possibleMoves;
        }
        
        public bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board)
        {
            // Knights have L-shaped moves and can jump over pieces
            int dx = Mathf.Abs(targetPosition.x - currentPosition.x);
            int dy = Mathf.Abs(targetPosition.y - currentPosition.y);
            
            // Knight can attack if the target is in an L-shape pattern (2,1) or (1,2)
            return (dx == 1 && dy == 2) || (dx == 2 && dy == 1);
        }        
    }
}
