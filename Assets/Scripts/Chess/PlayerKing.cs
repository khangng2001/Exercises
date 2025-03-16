using System;
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
            EnableInput();
        }

        private void HandleMoveDirection(Vector2Int dir)
        {
            Vector2Int targetPos = Position + dir;
            // if(!_board.IsPositionValid(targetPos)) return;
            // if (_board.IsPositionOccupied(targetPos))
            // {
            //     IMovable enemy = _board.GetPieceAtPosition(targetPos);
            //     if (enemy is ChessPiece chessPiece)
            //     {
            //         bool defeated = chessPiece.TakeDamage(1);
            //         if (defeated)
            //         {
            //             AttemptMove(targetPos);
            //         }
            //         return;
            //     }
            // }
            AttemptMove(targetPos);
            
        }
        
        private void OnDestroy()
        {
            if (_inputHandler != null)
            {
               DisableInput();
            }
        }

        public void EnableInput()
        {
            if (_inputHandler != null)
            {
                _inputHandler.OnMoveDirection += HandleMoveDirection;
            }
        }

        public void DisableInput()
        {
            if (_inputHandler != null)
            {
                _inputHandler.OnMoveDirection -= HandleMoveDirection;
            }
        }
    }
}
