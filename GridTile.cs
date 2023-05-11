using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    public int x;
    public int y;
    public bool isOccupied = false;
    public bool isHit = false;
    public bool isMiss = false;

    private Image tileImage;

    private void Awake()
    {
        tileImage = GetComponent<Image>();
    }

    // Constructor that takes three arguments
    public GridTile(int x, int y, Image tileImage)
    {
        this.x = x;
        this.y = y;
        this.tileImage = tileImage;
    }

    public void SetColor(Color color)
    {
        tileImage.color = color;
    }

    public void SetHit()
    {
        isHit = true;
        SetColor(Color.red);
    }

    public void SetMiss()
    {
        isMiss = true;
        SetColor(Color.gray);
    }

    public void SetShip()
    {
        isOccupied = true;
        SetColor(Color.blue);
    }

    public void SetSunk()
    {
        SetColor(Color.black);
    }

    public bool HasShip()
    {
        return isOccupied;
    }
}
