using UnityEngine;

public class TileDrag : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 mouseOffset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        startPosition = transform.position;
        mouseOffset = startPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
            newPosition.z = 0; // Keep it in 2D space
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        FindClosestTileAndSwap();
    }

   void FindClosestTileAndSwap()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
    Debug.Log($"Checking swap for {gameObject.name} - Found {colliders.Length} colliders");

    foreach (Collider2D col in colliders)
    {
        if (col.gameObject != this.gameObject && col.gameObject.CompareTag("Tile"))
        {
            Debug.Log($"Swapping {gameObject.name} with {col.gameObject.name}");
            SwapTiles(col.gameObject);
            return;
        }
    }

    // No valid swap found, return to original position
    Debug.Log($"No valid swap for {gameObject.name}, returning to original position.");
    transform.position = startPosition;
}


    void SwapTiles(GameObject otherTile)
{
    Vector3 otherPosition = otherTile.transform.position;
    otherTile.transform.position = startPosition;
    transform.position = otherPosition;

    // Use the new Unity method for finding the GameBoard
    FindFirstObjectByType<GameBoard>().CheckForMatches();
}


}
