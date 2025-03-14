using System;
using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chess
{
    public abstract class ChessPiece : MonoBehaviour, IMovable
    {
        [SerializeField] protected int maxHealth;
        
        protected IBoard _board;
        protected IMovementStrategy _movementStrategy;

        public Vector2Int Position { get; private set; }
        public int  CurrentHealth { get; private set; }
        
        private Vector2Int _initialPos;
        public bool HasMoved { get; private set; }

        public virtual void Initialized(IBoard board, IMovementStrategy movementStrategy, Vector2Int initialPos)
        {
            this._board = board;
            this._movementStrategy = movementStrategy;
            this._initialPos = initialPos;
            
            Position = _initialPos;
            CurrentHealth = maxHealth;
            _board.RegisterChessPiece(this);
        }
        
        protected virtual void Awake()
        {
            HasMoved = false;
        }

        public bool AttemptMove(Vector2Int newPosition)
        {
            List<Vector2Int> possibleMoves = GetPossibleMoves(); //fetch all possible moves
            if (possibleMoves.Contains(newPosition)) // --> check if the new desiredPos contains in possibleMoves list
            {
                //if yes --> update new pos
                Vector2Int oldPos = Position;
                Position = newPosition;
                _board.UpdatePiecePosition(this, oldPos, newPosition);
                HasMoved = true;
                
                UpdateVisualPos();
                return true;
            }
            return false;
        }

        public List<Vector2Int> GetPossibleMoves()
        {
            return _movementStrategy.GetPossibleMoves(Position, _board);
        }

        private void UpdateVisualPos()
        {
            transform.position = new Vector3(Position.x, 0.125f, Position.y);
        }
    }
}
