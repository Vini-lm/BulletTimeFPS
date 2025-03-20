using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float bulletSpeed = 30f;

    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool useTimeScaledPitch = true; 

    public void OnFire() => Shoot();

    public void Shoot()
    {
        if (bulletPrefab && muzzle)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            
            if (bullet.TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = muzzle.forward * bulletSpeed;
            }

            PlayShootSound();
            Destroy(bullet,3f);
        }
    }

    private void PlayShootSound()
    {
        if (!audioSource || !shootSound) return;

        if (useTimeScaledPitch)
        {
            audioSource.pitch = Mathf.Clamp(UnityEngine.Time.timeScale, 0.1f, 1f);
        }
        else
        {
            audioSource.pitch = 1f;
        }

        audioSource.PlayOneShot(shootSound);
    }

    void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }
}