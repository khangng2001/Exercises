using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance { get; private set; }

        [SerializeField] private float moveDelay = 0.5f;
        
       [SerializeField] private TurnState CurrentTurn { get; set; }

       [SerializeField] private PlayerKing _playerKing;

       [SerializeField] private List<AIPieces> _aiPieces = new List<AIPieces>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            CurrentTurn = TurnState.PlayerTurn;
            Debug.Log("<color=cyan>[TURN] Game started. Current turn: Player</color>");
        }

        private void Start()
        {
            _playerKing = FindObjectOfType<PlayerKing>();
            if (_playerKing)
            {
                _playerKing.OnPieceMoved += OnPieceMove;
            }
            UpdateAIPiecesList();
        }
        
        public int GetRemainingEnemyCount()
        {
            return _aiPieces.Count;
        }   

        private void UpdateAIPiecesList()
        {
            _aiPieces.Clear();
            _aiPieces.AddRange(FindObjectsOfType<AIPieces>());
            
            foreach (var piece in _aiPieces)
            {
                piece.OnPieceDestroyed += OnAIPieceDestroyed;
            }
        }

        private void OnAIPieceDestroyed(ChessPiece piece)
        {
            _aiPieces.Remove(piece as AIPieces);

            if (_aiPieces.Count != 0) return;
            Debug.Log($"<color=cyan>[TURN] AI piece destroyed. Remaining: {_aiPieces.Count}</color>");
            CurrentTurn = TurnState.GameOver;
        }

        private void OnPieceMove(ChessPiece obj)
        {
            if (CurrentTurn == TurnState.PlayerTurn)
            {
                StartEnemyTurn();
            }
        }

        private void StartEnemyTurn()
        {
            CurrentTurn = TurnState.EnemyTurn;
            Debug.Log($"<color=yellow>[TURN] Current turn: Enemy</color>");
            _playerKing.DisableInput();
            StartCoroutine(ProcessEnemyTurns());
        }

        private IEnumerator ProcessEnemyTurns()
        {
            yield return new WaitForSeconds(0.2f);
            var enemies = new List<AIPieces>(_aiPieces);
            
            foreach (var enemy in enemies)
            {
                
                if (enemy == null) continue;
                
                enemy.TakeTurn(_playerKing.Position);
                
                yield return new WaitForSeconds(moveDelay);
                
                if (_playerKing == null || _playerKing.CurrentHealth <= 0)
                {
                    Debug.Log("Player defeated! Game over!");
                    CurrentTurn = TurnState.GameOver;
                    yield break; 
                }
            }
            
            StartPlayerTurn();
        }

        private void StartPlayerTurn()
        {
            CurrentTurn = TurnState.PlayerTurn;
            Debug.Log($"<color=cyan>[TURN] Current turn: Player</color>");
            _playerKing.EnableInput();
        }
    }
}