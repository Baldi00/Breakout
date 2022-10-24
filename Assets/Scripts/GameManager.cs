using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject ball;
    public GameObject lifes;
    public GameObject player;
    public GameObject tiles;
    public GameObject youWin;
    public GameObject youLose;

    void Awake()
    {
        //Spawn tiles
        for(int row = 0; row < tileRows; row++)
        {
            for(int col = 0; col < tileCols; col++)
            {
                Vector2 tilePosition = new Vector2(TILE_LEFT_START + col * tileSizeX, TILE_UP_START + row * tileSizeY);
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().color = tileColors[row];
                tile.transform.parent = tiles.transform;
            }
        }
    }

    void Start()
    {
        StartCoroutine(WaitThenStartGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Breakout");
        }

        if(tiles.transform.childCount == 0)
        {
            Time.timeScale = 0;
            youWin.SetActive(true);
        }
    }

    IEnumerator WaitThenStartGame()
    {
        yield return new WaitForSeconds(2);
        ball.transform.position = new Vector2(player.transform.position.x, 0) ;
        ball.GetComponent<Ball>().angle = -90;
        ball.SetActive(true);
    }

    public void PlayerDied()
    {
        ball.SetActive(false);
        int currentLifes = lifes.transform.childCount;
        if (currentLifes > 0)
        {
            Destroy(lifes.transform.GetChild(0).gameObject);
        }

        if(currentLifes == 1)
        {
            Time.timeScale = 0;
            youLose.SetActive(true);
        }
        else
        {
            StartCoroutine(WaitThenStartGame());
        }
    }
}
