using UnityEngine;

namespace Chess
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject blackTile;
        [SerializeField] private GameObject whiteTile;
        
        private int tileSize = 1;
        private void Start()
        {
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    bool isWhiteTile = (row + col) % 2 == 0;
                    GameObject tilePrefab = isWhiteTile ? whiteTile : blackTile;
            
                    Vector3 position = new Vector3(col * tileSize, 0, row * tileSize);
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                    tile.transform.parent = transform;
                }
            }
        }
    
    }
}
