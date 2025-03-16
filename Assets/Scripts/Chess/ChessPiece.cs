using System;
using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chess
{
    public abstract class ChessPiece : MonoBehaviour, IMovable, IDamageable
    {
        [SerializeField] protected int maxHealth;
        
        protected IBoard _board;
        protected IMovementStrategy _movementStrategy;
        
        public Vector2Int Position { get; private set; }
        public int  CurrentHealth { get; private set; }

        private Vector2Int _initialPos;
        public bool HasMoved { get; private set; }

        [SerializeField] private ChessType chessType;
        public ChessType ChessType => chessType;

        public event Action<ChessPiece> OnPieceDestroyed;
        public event Action<ChessPiece> OnPieceMoved; 
        
        public event Action<ChessPiece, IDamageable> OnEnemyDefeated;

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
                OnPieceMoved?.Invoke(this);
                return true;
            }

            if (_board.IsPositionValid(newPosition) && _board.IsPositionOccupied(newPosition))
            {
                IMovable enemyPiece = _board.GetPieceAtPosition(newPosition);
                if (enemyPiece is IDamageable chessPiece)
                {
                    bool defeated = Attack(chessPiece, 1);
                    if (defeated)
                    {
                        Vector2Int oldPos = Position;
                        Position = newPosition;
                        _board.UpdatePiecePosition(this, oldPos, newPosition);
                        HasMoved = true;
                        UpdateVisualPos();
                        return true;
                    }
                    // Attack happened but didn't defeat enemy
                    return false;
                }
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
        
        public bool TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            // Add this detailed debug log to see which piece is taking damage
            Debug.Log($"({ChessType}) took {damageAmount} damage! Health: {CurrentHealth}/{maxHealth}");
            
            if (CurrentHealth <= 0)
            {
                Debug.Log($"({ChessType}) was defeated!");
                _board.RemoveChessPiece(Position);
                Die();
                return true; 
            }
            return false; 
        }

        private bool Attack(IDamageable target, int damage)
        {
            OnPieceMoved?.Invoke(this);
            if (target is ChessPiece targetPiece)
            {
                Debug.Log($"({ChessType}) attacking ({targetPiece.ChessType}) at position {targetPiece.Position}");
            }
            
            bool defeated = target?.TakeDamage(damage) ?? false;

            if (defeated)
            {
                OnEnemyDefeated?.Invoke(this, target);
                if (this is PlayerKing && TurnManager.Instance != null && TurnManager.Instance.GetRemainingEnemyCount() == 0)
                {
                    Debug.Log("<color=green>[COMBAT] Player defeated the last enemy! VICTORY!</color>");
                }
            }
            // Add result of attack
            // Debug.Log($"[COMBAT] Attack result: {(defeated ? "Enemy defeated!" : "Enemy survived!")}");
    
            return defeated;
        }

        protected virtual void Die()
        {
            OnPieceDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
