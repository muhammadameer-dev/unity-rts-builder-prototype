using UnityEngine;

public class PreviewRenderer : MonoBehaviour
{
    private GameObject previewObject;
    private Material previewMat;

    public void SpawnPreview(GameObject prefab)
    {
        if (previewObject != null) Destroy(previewObject);
        previewObject = Instantiate(prefab);
        previewMat = new Material(previewObject.GetComponent<Renderer>().sharedMaterial);
        previewObject.GetComponent<Renderer>().material = previewMat;
        previewMat.color = new Color(0, 1, 0, 0.5f);
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
