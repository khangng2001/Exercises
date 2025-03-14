using System;
using System.Collections.Generic;
using Chess;
using Chess.ChessStrategies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chess
{
    public class ChessSpawner : MonoBehaviour
    {
        [Header("Chess pieces")] 
        [SerializeField] private PlayerKing playerKingPrefab;
        [SerializeField] private List<PiecesData> piecesData = new List<PiecesData>();
        [SerializeField] private int maxNumPiece = 7;
        [SerializeField] private List<Vector2Int> occupiedPos = new List<Vector2Int>();
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
        }

        public GameObject GetRandomPiece()
        {
            float totalWeight = 0;
            foreach (var pieces in piecesData)
            {
                totalWeight += pieces.weight;
            }
            float randomValue = Random.Range(0, totalWeight);
            float accumlatedWeight = 0;
           foreach (var piece in piecesData)
           {
               accumlatedWeight += piece.weight;
               if (randomValue < accumlatedWeight && piece.currentCount < piece.maxAppearance)
               {
                   piece.maxAppearance++;
                   return piece.piecePrefab;
               }
           }
           return null;
        }

        private Vector2Int GetRandomKingPos()
        {
            return new Vector2Int(Random.Range(0, 8), Random.Range(0, 2)); // limit king pos in 2 last row
        }

        private Vector3 GetWorldPos(Vector2Int currentPos)
        {
            return new Vector3(currentPos.x, 0.125f, currentPos.y);
        }

        private bool IsPositionAvailable(Vector2Int pos)
        {
            return !_chessBoard.IsPositionOccupied(pos) && _chessBoard.IsPositionValid(pos);
        }

        private void SpawnPlayerKing()
        {
            _playerKingPos = GetRandomKingPos();
            PlayerKing playerKing = Instantiate(playerKingPrefab, GetWorldPos(_playerKingPos), Quaternion.identity, this.transform);
            if (playerKing)
            {
                playerKing.Initialized(_chessBoard, new KingMovementStrategy(), _playerKingPos);
                _playerInputHandler = GetComponentInChildren<PlayerInputHandler>();
                playerKing.SetInputHandler(_playerInputHandler);
                Debug.Log($"Player: {playerKing.CurrentHealth}");
            }
        }

        private void SpawnPieces(GameObject piecePrefab, Vector2Int position)
        {
            
        }

        private void GetAdjacent(Vector2Int currentPos)
        {
            
        }
        
    }
}

[Serializable]
public class PiecesData
{
    public ChessType chestType;
    public GameObject piecePrefab;
    public int maxAppearance;
    public float weight;
    [HideInInspector] public int currentCount;
}
