using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.1f;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, destroyDelay);
    }
}