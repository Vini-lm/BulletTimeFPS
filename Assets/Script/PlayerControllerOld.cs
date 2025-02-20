using UnityEngine;

public class PlayerControllerOld : MonoBehaviour
{

    [SerializeField]private float eyeSpeed;
    private Quaternion baseOrientation;
    private float mouseH = 0;
    private float mouseV = 0;
    private Vector3 pos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseOrientation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pos = new Vector3(Screen.width/2.0f,Screen.height/2.0f,Camera.main.nearClipPlane);  
    }

    // Update is called once per frame
    void Update()
    {
        mouseH += Input.GetAxis("Mouse X");
        mouseV += Input.GetAxis("Mouse Y");
        Quaternion rotX,rotY;
        float angleY = mouseH * eyeSpeed;
        float angleX = mouseV * eyeSpeed;

        rotY = Quaternion.AngleAxis(angleY,Vector3.up);
        rotX = Quaternion.AngleAxis(angleX,Vector3.left);


        transform.localRotation = baseOrientation * rotX * rotY;



    }
}
