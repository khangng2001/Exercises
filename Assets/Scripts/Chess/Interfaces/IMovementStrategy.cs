using System.Collections.Generic;
using UnityEngine;

namespace Chess.Interfaces
{
    public interface IMovementStrategy
    {
        List<Vector2Int> GetPossibleMoves(Vector2Int currentPosition, IBoard board);
        bool CanAttackPosition(Vector2Int currentPosition, Vector2Int targetPosition, IBoard board);
    }
}
