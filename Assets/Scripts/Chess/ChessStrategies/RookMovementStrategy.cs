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
                for (int distance = 1; distance < 8; distance++)
                {
                    Vector2Int newPos = currentPosition + distance * direction;
                    if (board.IsPositionValid(newPos))
                    {
                        if (!board.IsPositionOccupied(newPos))
                        {
                            possibleMoves.Add(newPos);
                        }
                        else
                        {
                            // If we hit a piece, we can't move further in this direction
                            break;
                        }
                    }
                    else
                    {
                        // If we're off the board, we can't move further in this direction
                        break;
                    }
                }
            }
            return possibleMoves;

        }
    }
}