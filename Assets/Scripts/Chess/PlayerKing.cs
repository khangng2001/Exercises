using UnityEngine;

namespace Chess
{
    public class PlayerKing : ChessPiece
    {
        private IInputHandler _inputHandler;

        public void SetInputHandler(IInputHandler inputHandler)
        {
            if (this._inputHandler != null)
            {
                this._inputHandler.OnMoveDirection -= HandleMoveDirection;
            }

            this._inputHandler = inputHandler;
            this._inputHandler.OnMoveDirection += HandleMoveDirection;
        }

        private void HandleMoveDirection(Vector2Int dir)
        {
            Vector2Int targetPos = Position + dir;
            AttemptMove(targetPos);
        }
        
        private void OnDestroy()
        {
            if (_inputHandler != null)
            {
                _inputHandler.OnMoveDirection -= HandleMoveDirection;
            }
        }
    }
}
