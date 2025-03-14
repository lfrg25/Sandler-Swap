using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    public int width = 5;  // Number of columns
    public int height = 5; // Number of rows
    public float tileSpacing = 1.2f; // Adjust spacing between tiles
    public GameObject tilePrefab; // Tile prefab to spawn

    private GameObject[,] tiles; // 2D array to track tiles

    void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        tiles = new GameObject[width, height]; // Initialize the array

        float xOffset = (width - 1) / 2.0f;
        float yOffset = (height - 1) / 2.0f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2((x - xOffset) * tileSpacing, (y - yOffset) * tileSpacing);
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
                newTile.transform.parent = transform; // Organize under GameBoard
                tiles[x, y] = newTile; // Store tile in the array
            }
        }
    }

    public void CheckForMatches()
{
    List<GameObject> tilesToDestroy = new List<GameObject>();

    // **Check Horizontal Matches**
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width - 2; x++)
        {
            GameObject tile1 = tiles[x, y];
            GameObject tile2 = tiles[x + 1, y];
            GameObject tile3 = tiles[x + 2, y];

            if (tile1 != null && tile2 != null && tile3 != null)
            {
                Sprite s1 = tile1.GetComponent<SpriteRenderer>().sprite;
                Sprite s2 = tile2.GetComponent<SpriteRenderer>().sprite;
                Sprite s3 = tile3.GetComponent<SpriteRenderer>().sprite;

                if (s1 == s2 && s2 == s3) // Match found!
                {
                    tilesToDestroy.Add(tile1);
                    tilesToDestroy.Add(tile2);
                    tilesToDestroy.Add(tile3);
                }
            }
        }
    }

    // **Check Vertical Matches**
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height - 2; y++)
        {
            GameObject tile1 = tiles[x, y];
            GameObject tile2 = tiles[x, y + 1];
            GameObject tile3 = tiles[x, y + 2];

            if (tile1 != null && tile2 != null && tile3 != null)
            {
                Sprite s1 = tile1.GetComponent<SpriteRenderer>().sprite;
                Sprite s2 = tile2.GetComponent<SpriteRenderer>().sprite;
                Sprite s3 = tile3.GetComponent<SpriteRenderer>().sprite;

                if (s1 == s2 && s2 == s3) // Match found!
                {
                    tilesToDestroy.Add(tile1);
                    tilesToDestroy.Add(tile2);
                    tilesToDestroy.Add(tile3);
                }
            }
        }
    }

    // **Destroy matched tiles**
    foreach (GameObject tile in tilesToDestroy)
    {
        Destroy(tile);
    }

    // Log the number of tiles destroyed for debugging
    Debug.Log("Destroyed " + tilesToDestroy.Count + " tiles.");
}

}