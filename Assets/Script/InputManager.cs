using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{


    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerController = GetComponent<PlayerController>();
        onFoot.Jump.performed += temp => playerController.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerController.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
