using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class AIPieces : ChessPiece
    {
        public void TakeTurn(Vector2Int playerKingPos)
        {
            // Step 1: Check if we can attack the king directly using the strategy's attack logic
            bool canAttackKing = false;
            
            if (_board.IsPositionValid(playerKingPos) && _board.IsPositionOccupied(playerKingPos))
            {
                // Use the strategy's CanAttackPosition method to determine if attack is valid
                canAttackKing = _movementStrategy.CanAttackPosition(Position, playerKingPos, _board);
            }
            
            // Step 2: If we can attack the king, do it!
            if (canAttackKing)
            {
                AttemptMove(playerKingPos);
                return;
            }
            
            // Step 3: If we can't attack the king, pick a random move
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
    }
}