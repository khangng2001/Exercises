using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class BishopMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();
            
            // Bishops move diagonally in four directions
            Vector2Int[] directions =
            {
                new Vector2Int(1, 1),   // top-right
                new Vector2Int(-1, 1),  // top-left
                new Vector2Int(1, -1),  // bottom-right
                new Vector2Int(-1, -1)  // bottom-left
            };
            
            foreach (var direction in directions)
            {
                for (int distance = 1; distance < 8; distance++)
                {
                    Vector2Int newPos = currentPosition + distance * direction;
                    
                    // Check if the position is valid and not occupied
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
           
            int dx = Mathf.Abs(targetPosition.x - currentPosition.x);
            int dy = Mathf.Abs(targetPosition.y - currentPosition.y);
            
           
            if (dx != dy)
                return false;
                
            
            Vector2Int direction = new Vector2Int(
                (targetPosition.x - currentPosition.x) / dx,
                (targetPosition.y - currentPosition.y) / dy
            );
            
            Vector2Int checkPos = currentPosition + direction;
            while (checkPos != targetPosition)
            {
                if (board.IsPositionOccupied(checkPos))
                    return false; 
                checkPos += direction;
            }
            
            return true; 
        }
        
    }
}
