using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float minDistance = 5f;
    private Animator animator;
    
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzle;
    private float lastShotTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lastShotTime = -fireRate;
        
        agent.stoppingDistance = minDistance; 
    }

    void Update()
    {
        agent.destination = playerPos.position;

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

        animator.SetBool("Walk", agent.remainingDistance > agent.stoppingDistance);
        
        if(CanSeePlayer() && UnityEngine.Time.time >= lastShotTime + fireRate)
        {
            Shoot();
            lastShotTime = UnityEngine.Time.time;
        }

        if(animator.GetBool("Killed"))
        {
            agent.isStopped = true;
            this.enabled = false;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (playerPos.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            lookRotation, 
            UnityEngine.Time.deltaTime * 5f
        );
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (playerPos.position - muzzle.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if(angle < 30f)
        {
            RaycastHit hit;
            if(Physics.Raycast(muzzle.position, directionToPlayer, out hit, Mathf.Infinity))
            {
                return hit.collider.CompareTag("Player");
            }
        }
        return false;
    }

    void Shoot()
    {
        if(projectilePrefab && muzzle)
        {
            GameObject projectile = Instantiate(
                projectilePrefab,
                muzzle.position,
                Quaternion.LookRotation(playerPos.position - muzzle.position)
            );

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if(rb) rb.linearVelocity = (playerPos.position - muzzle.position).normalized * 20f;
            Destroy(projectile, 3f);
        }
    }
}