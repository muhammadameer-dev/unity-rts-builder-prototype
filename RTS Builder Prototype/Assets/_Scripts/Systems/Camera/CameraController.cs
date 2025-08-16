using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 20f;
    public float edgeSize = 25f; // pixels near screen edge to trigger movement

    [Header("Zoom Settings")]
    public float zoomSpeed = 1000f;
    public float minZoom = 20f;
    public float maxZoom = 120f;

    private Camera cam;
    private Vector3 moveDir;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        moveDir = Vector3.zero;

        // Keyboard movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveDir += Vector3.forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveDir += Vector3.back;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDir += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDir += Vector3.right;

        // Mouse edge movement
        if (Input.mousePosition.x >= Screen.width - edgeSize) moveDir += Vector3.right;
        if (Input.mousePosition.x <= edgeSize) moveDir += Vector3.left;
        if (Input.mousePosition.y >= Screen.height - edgeSize) moveDir += Vector3.forward;
        if (Input.mousePosition.y <= edgeSize) moveDir += Vector3.back;

        // Apply movement
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cam.fieldOfView -= scroll * zoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }
}
