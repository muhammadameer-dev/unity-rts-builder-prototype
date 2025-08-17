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
            Mathf.FloorToInt(worldPos.x / cellSize),
            Mathf.FloorToInt(worldPos.z / cellSize)
        );
    }

    public Vector3 BuildingSnapPosition(Vector2Int buildingSize, Vector2Int gridPos)
    {
        // Center offset for even/odd sizes
        Vector2 offset = new Vector2(
            (buildingSize.x % 2 == 0) ? (buildingSize.x / 2f - 0.5f) : (buildingSize.x / 2f),
            (buildingSize.y % 2 == 0) ? (buildingSize.y / 2f - 0.5f) : (buildingSize.y / 2f)
        );

        // Shift grid position to match building footprint
        Vector2 adjustedGridPos = new Vector2(
            gridPos.x + offset.x,
            gridPos.y + offset.y
        );

        return new Vector3(adjustedGridPos.x * cellSize, 0, adjustedGridPos.y * cellSize);
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
                Gizmos.DrawWireCube(pos + 0.5f * cellSize * Vector3.one, Vector3.one * cellSize);
            }
        }
    }

}
