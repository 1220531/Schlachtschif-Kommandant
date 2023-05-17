using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { Building, Playing, GameOver };
        public GameState gameState;

        public GameObject startButton;
        public GameObject player1Grid;
        public GameObject player2Grid;
        public GameObject player1TrackingGrid;
        public GameObject player2TrackingGrid;

        private TileManager player1TileManager;
        private TileManager player2TileManager;

        private int currentPlayer;
        private int player1ShipsRemaining;
        private int player2ShipsRemaining;

        private TileManager tileManager;
        private List<Ship> shipsList;
        //building grid part
        private void Start()
        {
            player1TileManager = player1Grid.GetComponent<TileManager>();
            player2TileManager = player2Grid.GetComponent<TileManager>();

            player1ShipsRemaining = player1TileManager.GetShips().Count;
            player2ShipsRemaining = player2TileManager.GetShips().Count;

            currentPlayer = Random.Range(1, 2);

            gameState = GameState.Building;
        }
        //handels start of the game and first player turn
        public void StartGame()
        {
            startButton.SetActive(false);
            player1Grid.SetActive(true);
            player2Grid.SetActive(true);

            tileManager = FindObjectOfType<TileManager>();
            shipsList = tileManager.GetShips();
            if (currentPlayer == 1)
            {
                player1TileManager.canClick = true;
            }
            else
            {
                player2TileManager.canClick = true;
            }

            gameState = GameState.Playing;
        }
        //handels if its a players turn they can click on a grid
        public void EndTurn()
        {
            if (currentPlayer == 1)
            {
                player1TileManager.canClick = false;
                player2TileManager.canClick = true;
                currentPlayer = 2;
            }
            else
            {
                player2TileManager.canClick = false;
                player1TileManager.canClick = true;
                currentPlayer = 1;
            }
        }
        //handels number of placed ships and win condition
        public void ShipPlaced()
        {
            if (currentPlayer == 1)
            {
                player1ShipsRemaining--;
            }
            else
            {
                player2ShipsRemaining--;
            }

            if (player1ShipsRemaining == 0 || player2ShipsRemaining == 0)
            {
                gameState = GameState.GameOver;

                if (player1ShipsRemaining == 0)
                {
                    Debug.Log("Player 2 wins!");
                }
                else
                {
                    Debug.Log("Player 1 wins!");
                }
            }
            else
            {
                EndTurn();
            }
        }
    }
}
