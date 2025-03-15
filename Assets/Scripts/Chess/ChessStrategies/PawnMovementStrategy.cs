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

            Vector2Int forwardOne = new Vector2Int(currentPosition.x + 1, currentPosition.y);

            if (board.IsPositionValid(forwardOne) && !board.IsPositionOccupied(forwardOne))
            {
                possibleMoves.Add(forwardOne);
            }
            
            return possibleMoves;
        }
    }
}
