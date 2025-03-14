using UnityEngine;

namespace Chess
{
    public interface IBoard
    {
        bool IsPositionValid(Vector2Int pos);
        bool IsPositionOccupied(Vector2Int pos);
        void RegisterChessPiece(IMovable chessPiece);
        void UpdatePiecePosition(IMovable chessPiece, Vector2Int oldPos, Vector2Int newPos);
    }
}
