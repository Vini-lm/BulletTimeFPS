using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float bulletSpeed = 50f;
    
    public void OnFire()
    {
        Shoot();
    }

    public void Shoot()
{
    if (bulletPrefab && muzzle)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = muzzle.forward * bulletSpeed;
            }
            
            Destroy(bullet, 3f);
        }
}
}