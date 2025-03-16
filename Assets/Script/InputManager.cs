using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{


    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerController playerController;
    private CameraController cameraController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerController = GetComponent<PlayerController>();
        cameraController = GetComponent<CameraController>();
        onFoot.Jump.performed += temp => playerController.Jump();
        onFoot.Shoot.performed += temp => playerController.Shoot();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerController.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        cameraController.RotateCamera(onFoot.CamMov.ReadValue<Vector2>());
        SprintController();

    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    private void SprintController()
    {
        if (onFoot.Sprint.ReadValue<float>() > 0)
            playerController.Sprint(true);
        else
            playerController.Sprint(false);
    }
}
