using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{


    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private float speedBase;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float JumpAlt;

    [SerializeField] private GameObject gun;
    private bool isGrounded;
    private bool isSprinting;
    private CapsuleCollider collider;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = 5.0f;
        gravity = -9.81f;
        JumpAlt = 2.5f;
        speedBase = speed;
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        bool isMoving = (input.x != 0 || input.y != 0);

        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2.0f;

        animator.SetBool("Walk", isMoving);
    }


    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(2 * JumpAlt * -gravity);
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
        // animator.SetBool("Shooting", true);

    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
