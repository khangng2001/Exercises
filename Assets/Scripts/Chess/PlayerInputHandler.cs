using System;
using UnityEngine;

namespace Chess
{
    public class PlayerInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<Vector2Int> OnMoveDirection;

        private void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            // Normal movement
            if(Input.GetKeyDown(KeyCode.A))
                OnMoveDirection?.Invoke(Vector2Int.left);
            else if (Input.GetKeyDown(KeyCode.D))
                OnMoveDirection?.Invoke(Vector2Int.right);
            else if (Input.GetKeyDown(KeyCode.W))
                OnMoveDirection?.Invoke(Vector2Int.up);
            else if (Input.GetKeyDown(KeyCode.S))
                OnMoveDirection?.Invoke(Vector2Int.down);
            
            //Diagonal movement
            else if (Input.GetKeyDown(KeyCode.Q))
                OnMoveDirection?.Invoke(new Vector2Int(-1, 1));
            else if (Input.GetKeyDown(KeyCode.E))
                OnMoveDirection?.Invoke(new Vector2Int(1,1));
            else if (Input.GetKeyDown(KeyCode.Z))
                OnMoveDirection?.Invoke(new Vector2Int(-1 ,-1));
            else if(Input.GetKeyDown(KeyCode.C))
                OnMoveDirection?.Invoke(new Vector2Int(1,-1));
        }

    }
}
