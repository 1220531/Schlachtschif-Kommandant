using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int x;
    public int y;
    public bool occupied;
    public GameObject ship;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
        occupied = false;
        ship = null;
    }
}
public class Ship : MonoBehaviour
{
    public int length;
    public Tile startTile;
    public Tile endTile;
}


public class TileManager : MonoBehaviour
{
    public int gridSize;
    public GameObject tilePrefab;
    public GameObject[] ships;
    public bool canClick = false;

    private Tile[,] tiles;

    // list of ships based on set number in unity 
    public List<Ship> GetShips()
    {
        List<Ship> shipsList = new List<Ship>();

        foreach (GameObject shipObject in ships)
        {
            Ship ship = shipObject.GetComponent<Ship>();
            shipsList.Add(ship);
        }

        return shipsList;
    }


    // Generates grids 
    private void Awake()
    {
        tiles = new Tile[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                tileObject.transform.SetParent(transform);

                Tile tile = new Tile(x, y);
                tiles[x, y] = tile;

                TileClickHandler clickHandler = tileObject.GetComponent<TileClickHandler>();
                clickHandler.tileManager = this;
                clickHandler.tile = tile;
            }
        }
    }
    //handels ship length rotation and placement
    public bool PlaceShip(GameObject shipPrefab, Tile startTile, Tile endTile)
    {
        int length = Mathf.Max(Mathf.Abs(startTile.x - endTile.x), Mathf.Abs(startTile.y - endTile.y)) + 1;
        bool horizontal = startTile.y == endTile.y;
        bool vertical = startTile.x == endTile.x;

        if (length != shipPrefab.GetComponent<Ship>().length)
        {
            return false;
        }

        if (!horizontal && !vertical)
        {
            return false;
        }

        if (horizontal && startTile.x > endTile.x)
        {
            Tile temp = startTile;
            startTile = endTile;
            endTile = temp;
        }

        if (vertical && startTile.y > endTile.y)
        {
            Tile temp = startTile;
            startTile = endTile;
            endTile = temp;
        }

        if (horizontal)
        {
            for (int x = startTile.x; x <= endTile.x; x++)
            {
                if (tiles[x, startTile.y].occupied)
                {
                    return false;
                }
            }

            GameObject shipObject = Instantiate(shipPrefab, new Vector3((startTile.x + endTile.x) / 2f, 0.5f, startTile.y), Quaternion.identity);
            Ship ship = shipObject.GetComponent<Ship>();
            ship.startTile = startTile;
            ship.endTile = endTile;

            for (int x = startTile.x; x <= endTile.x; x++)
            {
                tiles[x, startTile.y].occupied = true;
                tiles[x, startTile.y].ship = shipObject;
            }

            return true;
        }

        if (vertical)
        {
            for (int y = startTile.y; y <= endTile.y; y++)
            {
                if (tiles[startTile.x, y].occupied)
                {
                    return false;
                }
            }

            GameObject shipObject = Instantiate(shipPrefab, new Vector3(startTile.x, 0.5f, (startTile.y + endTile.y) / 2f), Quaternion.identity);
            Ship ship = shipObject.GetComponent<Ship>();
            ship.startTile = startTile;
            ship.endTile = endTile;

            for (int y = startTile.y; y <= endTile.y; y++)
            {
                tiles[startTile.x, y].occupied = true;
                tiles[startTile.x, y].ship = shipObject;
            }

            return true;
        }

        return false;
    }
}
//refrence to  click handler
public class TileClickHandler : MonoBehaviour
{
    public TileManager tileManager;
    public Tile tile;
    private void OnMouseDown()
    {
        if (tileManager.canClick)
        {
            tileManager.PlaceShip(tileManager.ships[0], tile, tile);
        }
    }
}
