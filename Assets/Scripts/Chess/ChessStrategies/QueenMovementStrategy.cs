using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class QueenMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();
            Vector2Int[] directions =
            {
                Vector2Int.left, 
                Vector2Int.right,
                Vector2Int.up,
                Vector2Int.down,
                
                new Vector2Int(1,1),
                new Vector2Int(-1,1),
                new Vector2Int(1,-1),
                new Vector2Int(-1,-1)
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
        
         // New method for attack validation
        public bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board)
        {
            // Queens attack like rooks and bishops combined
            
            // First, check if the move is valid (horizontal, vertical, or diagonal)
            int dx = Mathf.Abs(targetPosition.x - currentPosition.x);
            int dy = Mathf.Abs(targetPosition.y - currentPosition.y);
            
            bool onSameRowOrCol = (targetPosition.x == currentPosition.x || targetPosition.y == currentPosition.y);
            bool onDiagonal = (dx == dy);
            
            if (!onSameRowOrCol && !onDiagonal)
                return false;
                
            // Determine the movement direction
            Vector2Int direction;
            
            if (targetPosition.x == currentPosition.x)
            {
                // Moving vertically
                int sign = (targetPosition.y > currentPosition.y) ? 1 : -1;
                direction = new Vector2Int(0, sign);
            }
            else if (targetPosition.y == currentPosition.y)
            {
                // Moving horizontally
                int sign = (targetPosition.x > currentPosition.x) ? 1 : -1;
                direction = new Vector2Int(sign, 0);
            }
            else
            {
                // Moving diagonally
                int signX = (targetPosition.x > currentPosition.x) ? 1 : -1;
                int signY = (targetPosition.y > currentPosition.y) ? 1 : -1;
                direction = new Vector2Int(signX, signY);
            }
            
            // Check for clear path to the target (excluding the target itself)
            Vector2Int checkPos = currentPosition + direction;
            while (checkPos != targetPosition)
            {
                if (board.IsPositionOccupied(checkPos))
                    return false; // Path is blocked by another piece
                checkPos += direction;
            }
            
            return true; 
        }
    }
}
