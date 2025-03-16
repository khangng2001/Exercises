using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class PawnMovementStrategy : IMovementStrategy
    {
        public List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();

            Vector2Int forwardOne = new Vector2Int(currentPosition.x, currentPosition.y + 1);

            if (board.IsPositionValid(forwardOne) && !board.IsPositionOccupied(forwardOne))
            {
                possibleMoves.Add(forwardOne);
            }
            
            return possibleMoves;
        }
        
        public bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board)
        {
            // Pawns attack diagonally forward
            int forwardDir = 1; // Assuming AI pawns move upward
            
            // Check if target is one square diagonally forward
            return (targetPosition.y - currentPosition.y) == forwardDir && 
                   Mathf.Abs(targetPosition.x - currentPosition.x) == 1;
        }
    }
}
