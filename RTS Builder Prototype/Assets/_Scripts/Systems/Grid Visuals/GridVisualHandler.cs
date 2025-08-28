using UnityEngine;
using UnityEngine.UI;

public class GridVisualHandler : MonoBehaviour
{
    [Header("Grid Setup")]
    [SerializeField] private GameObject gridCanvasPrefab;
    [SerializeField] private Sprite gridImageSprite;
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
    [SerializeField] private float cellSize = 1f;

    [Header("Visuals")]
    [SerializeField] private Color showColor = Color.white;
    [SerializeField] private Color hideColor = Color.clear;
    [SerializeField] private float colorTransitionSpeed = 5f;

    [Header("Test Mode")]
    [SerializeField] private GridMode currentMode = GridMode.None;

    private GameObject canvas;
    private Image gridImage;

    void Start()
    {
        Initialize(new Vector3(0, 0.2f, 0), new Vector3(90, 0, 0));
    }

    void Update()
    {
        UpdateVisibility();
    }

    public void Initialize(Vector3 position, Vector3 rotation)
    {
        EnsureCanvasExists();
        SetupCanvasTransform(position, rotation);
        SetupGridTexture();
    }

    private void EnsureCanvasExists()
    {
        if (canvas) return;
        var existing = transform.Find("Grid Canvas(Clone)");
        canvas = existing ? existing.gameObject : Instantiate(gridCanvasPrefab, Vector3.zero, Quaternion.identity, transform);
    }

    private void SetupCanvasTransform(Vector3 position, Vector3 rotation)
    {
        canvas.transform.position = position;
        canvas.transform.eulerAngles = rotation;

        Vector2 size = new Vector2(gridSize.x * cellSize, gridSize.y * cellSize);
        var rect = canvas.GetComponent<RectTransform>();
        rect.sizeDelta = size;

        var gridTexture = canvas.transform.GetChild(0).GetComponent<RectTransform>();
        gridTexture.sizeDelta = size;
    }

    private void SetupGridTexture()
    {
        gridImage = canvas.transform.GetChild(0).GetComponent<Image>();
        gridImage.sprite = gridImageSprite;
        gridImage.type = Image.Type.Tiled;
        gridImage.pixelsPerUnitMultiplier = 100 / cellSize;
        gridImage.color = showColor;
    }

    private void UpdateVisibility()
    {
        if (!gridImage) return;

        // Simple test: show only on Build mode
        bool shouldShow = currentMode == GridMode.Build;
        Color target = shouldShow ? showColor : hideColor;
        gridImage.color = Color.Lerp(gridImage.color, target, colorTransitionSpeed * Time.deltaTime);
    }
}
