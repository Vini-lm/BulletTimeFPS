using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float gravity;
    [SerializeField] private float JumpAlt;

    [SerializeField] private GameObject gun;
    [SerializeField] private float fireRate = 0.35f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 currentVelocity = Vector3.zero;
    private float speedBase;
    private float nextFireTime = 0f;
    private bool isGrounded;
    private bool isSprinting;
    private CapsuleCollider collider;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speedBase = speed;
        gravity = -9.81f;
        JumpAlt = 1.5f;
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            currentVelocity = Vector3.Lerp(currentVelocity, transform.TransformDirection(moveDirection) * speed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
        controller.Move(currentVelocity * Time.deltaTime);
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
        speed = sprint ? speedBase + 3.0f : speedBase;
    }

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            gun.GetComponent<ShootingSystem>().OnFire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBot")
            GameObject.Destroy(this);
    }
}
