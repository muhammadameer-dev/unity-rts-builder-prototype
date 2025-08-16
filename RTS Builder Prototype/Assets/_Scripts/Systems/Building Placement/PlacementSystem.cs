using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PreviewRenderer previewRenderer;
    [SerializeField] private ResourceManager resourceManager;

    private BuildingData currentBuilding;
    private Vector2Int currentGridPos;
    private int rotationY = 0;
    private bool isPlacing = false;

    void Update()
    {
        if (!isPlacing) return;

        UpdateMouseGridPosition();
        bool canPlace = gridManager.CanPlace(currentGridPos, currentBuilding.size);
        previewRenderer.UpdatePreview(gridManager.BuildingSnapPosition(currentBuilding.size, currentGridPos), rotationY, canPlace);

        if (Input.GetKeyDown(KeyCode.R)) RotatePreview();
        if (Input.GetMouseButtonDown(0) && canPlace) PlaceBuilding();
        if (Input.GetMouseButtonDown(1)) CancelPlacement();
    }

    public void StartPlacing(BuildingData building)
    {
        currentBuilding = building;
        isPlacing = true;
        rotationY = 0;
        previewRenderer.SpawnPreview(building.buildingPrefab);
    }

    private void UpdateMouseGridPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            currentGridPos = gridManager.WorldToGrid(hit.point);
    }

    private void RotatePreview()
    {
        rotationY = (rotationY + 90) % 360;
    }

    private void PlaceBuilding()
    {
        if (!resourceManager.HasEnoughResources(currentBuilding.ResourceCosts)) return;

        resourceManager.SpendResources(currentBuilding.ResourceCosts);

        Vector3 worldPos = gridManager.BuildingSnapPosition(currentBuilding.size,currentGridPos);
        GameObject placed = Instantiate(currentBuilding.buildingPrefab, worldPos, Quaternion.Euler(0, rotationY, 0));
        gridManager.RegisterObject(currentGridPos, currentBuilding.size, placed);

        FinishPlacement();
    }

    private void CancelPlacement()
    {
        previewRenderer.ClearPreview();
        FinishPlacement();
    }

    private void FinishPlacement()
    {
        isPlacing = false;
        currentBuilding = null;
    }
}
