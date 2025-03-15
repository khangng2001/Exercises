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
    }
}
