using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private float currentSpeed;

    void Update()
    {
        // Movimento não depende de Time.timeScale
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void SetSpeed(float scale)
    {
        currentSpeed = speed * scale;
    }
}