using System;
using UnityEngine;

namespace Chess
{
    public interface IInputHandler
    {
        void HandleInput();
        event Action<Vector2Int> OnMoveDirection;
    }
}
    