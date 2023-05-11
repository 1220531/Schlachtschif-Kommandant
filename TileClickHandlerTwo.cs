using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TilePrefabClickHandler : MonoBehaviour, IPointerDownHandler
{
    public TileManager tileManager;
    public Tile tile;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tileManager.canClick)
        {
            tileManager.PlaceShip(tileManager.ships[0], tile, tile);
        }
    }
}