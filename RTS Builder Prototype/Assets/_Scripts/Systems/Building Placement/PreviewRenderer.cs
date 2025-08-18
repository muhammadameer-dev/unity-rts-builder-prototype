using UnityEngine;

public class PreviewRenderer : MonoBehaviour
{
    private GameObject previewObject;
    private Material previewMat;

    public void SpawnPreview(GameObject prefab)
    {
        if (previewObject != null)
            Destroy(previewObject);

        previewObject = Instantiate(prefab);

        // Try to get renderer from root first, fallback to children
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer == null)
            renderer = previewObject.GetComponentInChildren<Renderer>();

        if (renderer == null)
        {
            Debug.LogError($"[SpawnPreview] No Renderer found on prefab '{prefab.name}' or its children.");
            return;
        }

        // Create material instance
        previewMat = new Material(renderer.sharedMaterial);
        renderer.material = previewMat;

        // Apply transparent green preview
        previewMat.color = new Color(0f, 1f, 0f, 0.5f);
    }


    public void UpdatePreview(Vector3 worldPosition, int rotationY, bool canPlace)
    {
        if (previewObject == null) return;
        previewObject.transform.position = worldPosition;
        previewObject.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        previewMat.color = canPlace ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
    }

    public void ClearPreview()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
            previewObject = null;
        }
    }
}
