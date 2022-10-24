using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float TILE_LEFT_START = -9.12f;
    private const float TILE_UP_START = 3f;

    public GameObject tilePrefab;
    public float tileSizeX = 0.96f;
    public float tileSizeY = 0.4f;
    public int tileRows = 6;
    public int tileCols = 20;
    public Color [] tileColors;

    void Start()
    {
        //Spawn tiles
        for(int row = 0; row < tileRows; row++)
        {
            for(int col = 0; col < tileCols; col++)
            {
                Vector2 tilePosition = new Vector2(TILE_LEFT_START + col * tileSizeX, TILE_UP_START + row * tileSizeY);
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().color = tileColors[row];
            }
        }
    }
}
