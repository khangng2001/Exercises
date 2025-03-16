using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class AIPieces : ChessPiece
    {
        public void TakeTurn(Vector2Int playerKingPos)
        {
            // Step 1: Check if we can attack the king using proper chess rules
            if (_board.IsPositionValid(playerKingPos) && _board.IsPositionOccupied(playerKingPos))
            {
                // Try direct attack if it's a valid chess move
                if (CanAttackPosition(playerKingPos))
                {
                    AttemptMove(playerKingPos);
                    return;
                }
            }
            
            // Step 2: If we can't attack the king, pick a random move
            List<Vector2Int> possibleMoves = GetPossibleMoves();
            
            if (possibleMoves.Count == 0)
            {
                return; // No moves available
            }
            
            // Pick a random move
            int randomIndex = Random.Range(0, possibleMoves.Count);
            Vector2Int targetPos = possibleMoves[randomIndex];
            
            AttemptMove(targetPos);
        }

        // Helper method to check if a position can be attacked according to chess rules
        private bool CanAttackPosition(Vector2Int targetPos)
        {
            // Check based on piece type and rules
            switch (ChessType)
            {
                case ChessType.Pawn:
                    // Pawns attack diagonally forward
                    int forwardDir = 1; // Assuming AI pawns move upward
                    return (targetPos.y - Position.y) == forwardDir && 
                           Mathf.Abs(targetPos.x - Position.x) == 1;
                    
                case ChessType.Knight:
                    // Knights have L-shaped moves
                    int dx = Mathf.Abs(targetPos.x - Position.x);
                    int dy = Mathf.Abs(targetPos.y - Position.y);
                    return (dx == 1 && dy == 2) || (dx == 2 && dy == 1);
                    
                case ChessType.Bishop:
                    // Bishops move diagonally
                    dx = Mathf.Abs(targetPos.x - Position.x);
                    dy = Mathf.Abs(targetPos.y - Position.y);
                    
                    // Must be on a diagonal
                    if (dx != dy) return false;
                    
                    // Check for clear path
                    return HasClearPath(targetPos);
                    
                case ChessType.Rook:
                    // Rooks move horizontally or vertically
                    if (targetPos.x != Position.x && targetPos.y != Position.y)
                        return false;
                        
                    // Check for clear path
                    return HasClearPath(targetPos);
                    
                case ChessType.Queen:
                    // Queens combine bishop and rook movements
                    dx = Mathf.Abs(targetPos.x - Position.x);
                    dy = Mathf.Abs(targetPos.y - Position.y);
                    
                    // Must be on a straight line (horizontal, vertical, or diagonal)
                    if ((targetPos.x != Position.x && targetPos.y != Position.y) && (dx != dy))
                        return false;
                        
                    // Check for clear path
                    return HasClearPath(targetPos);
                    
                case ChessType.King:
                    // Kings move one square in any direction
                    dx = Mathf.Abs(targetPos.x - Position.x);
                    dy = Mathf.Abs(targetPos.y - Position.y);
                    return dx <= 1 && dy <= 1;
                    
                default:
                    return false;
            }
        }
        
        // Helper method to check if there's a clear path to the target position
        private bool HasClearPath(Vector2Int targetPos)
        {
            Vector2Int direction = new Vector2Int(
                (targetPos.x - Position.x) == 0 ? 0 : (targetPos.x - Position.x) / Mathf.Abs(targetPos.x - Position.x),
                (targetPos.y - Position.y) == 0 ? 0 : (targetPos.y - Position.y) / Mathf.Abs(targetPos.y - Position.y)
            );
            
            Vector2Int currentPos = Position + direction;
            
            // Check each position along the path except the final target
            while (currentPos != targetPos)
            {
                if (_board.IsPositionOccupied(currentPos))
                {
                    return false; // Path is blocked
                }
                currentPos += direction;
            }
            
            return true; // Clear path to target
        }
    }
}