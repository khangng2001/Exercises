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
        
        public bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board)
        {
            // Rooks attack horizontally or vertically
            
            // Must be on same row or column
            if (targetPosition.x != currentPosition.x && targetPosition.y != currentPosition.y)
                return false;
                
            // Determine the movement direction
            Vector2Int direction;
            if (targetPosition.x == currentPosition.x)
            {
                // Moving vertically
                int sign = (targetPosition.y > currentPosition.y) ? 1 : -1;
                direction = new Vector2Int(0, sign);
            }
            else
            {
                // Moving horizontally
                int sign = (targetPosition.x > currentPosition.x) ? 1 : -1;
                direction = new Vector2Int(sign, 0);
            }
            
            // Check for clear path to the target (excluding the target itself)
            Vector2Int checkPos = currentPosition + direction;
            while (checkPos != targetPosition)
            {
                if (board.IsPositionOccupied(checkPos))
                    return false; // Path is blocked by another piece
                checkPos += direction;
            }
            
            return true; // Clear path to target
        }
    }
}