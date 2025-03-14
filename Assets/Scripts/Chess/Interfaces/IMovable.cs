using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public interface IMovable
    {
        Vector2Int Position { get; }
        bool AttemptMove(Vector2Int newPosition);
        List<Vector2Int> GetPossibleMoves();
    }
}
