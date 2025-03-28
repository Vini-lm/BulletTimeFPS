using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy = collision.gameObject.GetComponentInParent<EnemyController>();
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        Animator animator = collision.gameObject.GetComponentInParent<Animator>();
        if (enemy != null)
        {
            animator.SetBool("Killed", true);
            Destroy(enemy.gameObject, 3f);
            Destroy(gameObject);
        }
        if(player != null)
        {
            SceneManager.LoadScene("TelaInicial");
        }
    }
}