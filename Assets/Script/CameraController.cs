using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera camera;
    private float xRotation;

    private float xSense = 25.0f;
    private float ySense = 25.0f;


    public void Start()
    {
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * UnityEngine.Time.deltaTime * ySense;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * UnityEngine.Time.deltaTime) * xSense);

    }


}
