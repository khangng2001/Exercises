using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class ChessBoard : MonoBehaviour, IBoard
    {
        private int _boardSize = 8;
        private Dictionary<Vector2Int, IMovable> _piecePos = new Dictionary<Vector2Int, IMovable>();
        
        
        //is it out of bound 
        public bool IsPositionValid(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < _boardSize
                              && pos.y >= 0 && pos.y < _boardSize;
        }

        public bool IsPositionOccupied(Vector2Int pos)
        {
            return _piecePos.ContainsKey(pos);
        }

        public void RemoveChessPiece(Vector2Int pos)
        {
            if (_piecePos.ContainsKey(pos))
            {
                _piecePos.Remove(pos);
            }
        }

        public IMovable GetPieceAtPosition(Vector2Int currentPos)
        {
            return _piecePos.GetValueOrDefault(currentPos);
        }
        
        public void RegisterChessPiece(IMovable chessPiece)
        {
            if (chessPiece == null)
            {
                Debug.LogError("[ChessBoard] Attempted to register null chess piece!", this);
                return;
            }
            _piecePos.TryAdd(chessPiece.Position, chessPiece);
           //Debug.Log($"[ChessBoard] Registering piece at position: {chessPiece.Position}", this);
        }
       

        public void UpdatePiecePosition(IMovable chessPiece, Vector2Int oldPos, Vector2Int newPos)
        {
            if (_piecePos.ContainsKey(oldPos))
            {
                _piecePos.Remove(oldPos);
            }

            _piecePos[newPos] = chessPiece;
        }
    }
}
