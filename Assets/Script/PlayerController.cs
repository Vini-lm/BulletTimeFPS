using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{


    private CharacterController controller;
    private Vector3 playerVelocity;
    private float speedBase;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float JumpAlt;

    [SerializeField] private GameObject gun;
    private bool isGrounded;
    private bool isSprinting;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = 5.0f;
        gravity = -9.81f;
        JumpAlt = 2.5f;
        speedBase = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); // move o player de acordo com o input

        playerVelocity.y += gravity * Time.fixedDeltaTime; // aplica a gravidade no player
        controller.Move(playerVelocity * Time.fixedDeltaTime);

        if (isGrounded && playerVelocity.y < 0) // "impede" a gravidade de atuar na velocidade do player se ele estiver no chão
            playerVelocity.y = -2.0f;

        //Debug.Log(playerVelocity.y);
        //Debug.Log(isGrounded);
        //Debug.Log(isSprinting);


    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(2 * JumpAlt * -gravity); // calcula velocidade incial do pulo
        }
    }


    public void Sprint(bool sprint)
    {

        isSprinting = sprint;

        if (sprint)
            speed = speedBase + 3.0f; //8.0f
        else
            speed = speedBase; //5.0f

    }

    public void Shoot()
    {
        gun.GetComponent<ShootingSystem>().OnFire();
    }
}
