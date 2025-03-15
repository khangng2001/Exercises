using System.Collections.Generic;
using Chess.Interfaces;
using UnityEngine;

namespace Chess.ChessStrategies
{
    public class MovementStrategyFactory
    {
        private readonly Dictionary<ChessType, IMovementStrategy> _movementStrategies =
            new Dictionary<ChessType, IMovementStrategy>();

        private static MovementStrategyFactory _instance;
        
        public static MovementStrategyFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MovementStrategyFactory();
                }
                return _instance;
            }
        }

        private MovementStrategyFactory()
        {
            _movementStrategies[ChessType.Pawn] = new PawnMovementStrategy();
            _movementStrategies[ChessType.Knight] = new KnightMovementStrategy();
            _movementStrategies[ChessType.Queen] = new QueenMovementStrategy();
            _movementStrategies[ChessType.Rook] = new RookMovementStrategy();
            _movementStrategies[ChessType.Bishop] = new BishopMovementStrategy();
            _movementStrategies[ChessType.King] = new KingMovementStrategy();
        }

        public IMovementStrategy GetMovementStrategy(ChessType chessType)
        {
            return _movementStrategies.GetValueOrDefault(chessType);
        }
        
    }
}