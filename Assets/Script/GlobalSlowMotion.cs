using UnityEngine;

public class GlobalSlowMotion : MonoBehaviour
{
    [Header("Configurações")]
    [Range(0.1f, 1f)]
    public float slowMotionScale = 0.8f;

    private float originalTimeScale;
    private float originalFixedDeltaTime;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isMoving = (horizontal != 0 || vertical != 0);

        if (isMoving)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = slowMotionScale;
        }

        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        UpdateAnimatorsSpeed();
    }

    void UpdateAnimatorsSpeed()
    {
        Animator[] allAnimators = FindObjectsOfType<Animator>();
        foreach (Animator animator in allAnimators)
        {
            animator.speed = Time.timeScale;
        }
    }

    void OnDestroy()
    {
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}