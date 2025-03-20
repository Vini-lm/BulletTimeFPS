using UnityEngine;

public class IniciaCursor : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
