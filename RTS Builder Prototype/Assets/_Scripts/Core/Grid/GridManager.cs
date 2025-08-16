using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private bool showGizmos = true;

    public enum TileType { Grass, Water, Rock, Dirt }
    private TileType[,] map;
    private Dictionary<Vector2Int, GameObject> occupiedCells = new();


    void Awake()
    {
        map = new TileType[width, height];
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, 0, gridPos.y * cellSize);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / cellSize),
            Mathf.RoundToInt(worldPos.z / cellSize)
        );
    }

    public bool CanPlace(Vector2Int startPos, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int checkPos = new Vector2Int(startPos.x + x, startPos.y + y);
                if (occupiedCells.ContainsKey(checkPos))
                    return false;
            }
        }
        return true;
    }

    public void RegisterObject(Vector2Int startPos, Vector2Int size, GameObject obj)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                occupiedCells[new Vector2Int(startPos.x + x, startPos.y + y)] = obj;
            }
        }
    }

    public void RemoveObject(GameObject obj)
    {
        List<Vector2Int> keysToRemove = new List<Vector2Int>();
        foreach (var kvp in occupiedCells)
        {
            if (kvp.Value == obj)
                keysToRemove.Add(kvp.Key);
        }

        foreach (var key in keysToRemove)
            occupiedCells.Remove(key);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.white;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                Gizmos.DrawWireCube(pos + Vector3.one * cellSize * 0.5f, Vector3.one * cellSize);
            }
        }
    }

}
