using System;
using System.Collections.Generic;
using Chess;
using Chess.ChessStrategies;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Chess
{
    public class ChessSpawner : MonoBehaviour
    {
        [Header("Chess pieces")] 
        [SerializeField] private PlayerKing playerKingPrefab;
        [SerializeField] private List<PiecesData> piecesData = new List<PiecesData>();
        [SerializeField] private int maxNumPiece = 7;
        private Vector2Int _playerKingPos;
        
        private ChessBoard _chessBoard;
        private PlayerInputHandler _playerInputHandler;
        private void Awake()
        {
            _chessBoard = FindObjectOfType<ChessBoard>();
        }

        private void Start()
        {
            SpawnPlayerKing();
            SpawnEnemy();
        }

        private PiecesData GetRandomPiece()
        {
            float totalWeight = 0;
            float accumlateWeight = 0;
            foreach (var pieces in piecesData)
            {
                totalWeight += pieces.weight;
            }

            if (totalWeight <= 0) return default;
            float randomValue = Random.Range(0, totalWeight);
           foreach (var piece in piecesData)
           {
               accumlateWeight += piece.weight;
               if (randomValue < accumlateWeight && piece.currentAppearance < piece.maxAppearance)
               {
                   piece.currentAppearance++;
                   return piece;
               }
           }
           return default;
        }

        private Vector2Int GetRandomKingPos()
        {
            return new Vector2Int(Random.Range(0, 8), Random.Range(0, 2)); // limit king pos in 2 last row
        }

        private Vector3 GetWorldPos(Vector2Int currentPos)
        {
            return new Vector3(currentPos.x, 0.125f, currentPos.y);
        }

        //No chess in tile and tile not out of bound
        private bool IsPositionAvailable(Vector2Int pos)
        {
            return !_chessBoard.IsPositionOccupied(pos) && _chessBoard.IsPositionValid(pos);
        }
        
        //tile next to king ?
        private bool IsAdjacentToKing(Vector2Int currentPos)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector2Int adjacentPos = new Vector2Int(currentPos.x + i, currentPos.y + j);
                    if(adjacentPos == _playerKingPos)
                        return true;
                }
            }
            return false;
        }

        private void SpawnPlayerKing()
        {
            _playerKingPos = GetRandomKingPos();
            PlayerKing playerKing = Instantiate(playerKingPrefab, GetWorldPos(_playerKingPos), Quaternion.identity, this.transform);
            if (playerKing)
            {
                playerKing.Initialized(_chessBoard, MovementStrategyFactory.Instance.GetMovementStrategy(ChessType.King), _playerKingPos);
                _playerInputHandler = GetComponentInChildren<PlayerInputHandler>();
                playerKing.SetInputHandler(_playerInputHandler);
            }
        }

        private void SpawnPieces(AIPieces piecePrefab, Vector2Int position)
        {
            /*if (!IsPositionAvailable(position) || IsAdjacentToKing(position))
            {
                return;
            }*/
            AIPieces aiPieces = Instantiate(piecePrefab, GetWorldPos(position), Quaternion.identity, this.transform);
            if (aiPieces)
            {
                aiPieces.Initialized(_chessBoard, MovementStrategyFactory.Instance.GetMovementStrategy(piecePrefab.ChessType), position);
            }
        }

        private void SpawnEnemy()
        {
            int attempts = 0;
            int maxAttempt = 1000;
            int currentSpawnNum = 0;
            while (currentSpawnNum < maxNumPiece && attempts < maxAttempt)
            {
                Vector2Int randomPos = new Vector2Int(Random.Range(0, 8), Random.Range(0, 8));
                if (IsPositionAvailable(randomPos) && !IsAdjacentToKing(randomPos))
                {
                    PiecesData randomPiece = GetRandomPiece();
                    if (randomPiece != null && randomPiece.piecePrefab != null)
                    {
                        SpawnPieces(randomPiece.piecePrefab, randomPos);
                        currentSpawnNum++;
                    }
                    attempts++;
                }
            }
        }
        
    }
}

[Serializable]
public class PiecesData
{
    public AIPieces piecePrefab;
    public int maxAppearance;
    public float weight;
    [HideInInspector] public int currentAppearance;
}
