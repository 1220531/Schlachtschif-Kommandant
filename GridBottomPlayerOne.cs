using UnityEngine;
using UnityEngine.UI;

namespace GirdBottomPlayerOne
{
    public class GridScriptBottomPlayerOne : MonoBehaviour
    {
        // Reference to the TileManager script
        public TileManager tileManager;

        // Reference to the UI panel containing the grid
        public GameObject gridPanel;

        // Reference to the UI image used for the tiles
        public Image tileImage;

        // Variables to store the width, height, and spacing of the grid
        public int width;
        public int height;
        public int spacing;

        // Variables to store the size of the tiles and the tile prefab
        public int tileSize;
        public GameObject tilePrefab;

        // 2D array to store the GridTile objects representing each tile on the grid
        private GridTile[,] tiles;

        // Boolean to determine if the grid is clickable
        private bool clickable;

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the grid
            InitializeGrid();
        }

        // Update is called once per frame
        void Update()
        {
            // Check if the grid is clickable
            if (clickable)
            {
                // Handle user input for placing ships and tracking shots
                HandleInput();
            }
        }

        // Function to initialize the grid
        private void InitializeGrid()
        {
            // Create the 2D array of GridTile objects
            tiles = new GridTile[width, height];

            // Loop through each tile and instantiate a tile prefab
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Instantiate a tile prefab and set its position and parent
                    GameObject tileObj = Instantiate(tilePrefab);
                    tileObj.transform.SetParent(gridPanel.transform);
                    tileObj.transform.localPosition = new Vector3(x * (tileSize + spacing), y * (tileSize + spacing), 0);

                    // Set the tile's GridTile object in the tiles array
                    GridTile gridTile = new GridTile(x, y, tileObj.GetComponent<Image>());
                    tiles[x, y] = gridTile;
                }
            }
        }

        // Function to handle user input for placing ships and tracking shots
        private void HandleInput()
        {
            // TODO: Handle user input
        }

        // Function to set the clickable state of the grid
        public void SetClickable(bool state)
        {
            clickable = state;
        }
    }
}
