using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void Start()
    {
    }

    private void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBot");
        
        if (enemies.Length == 0)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Nome da próxima cena não definido!");
        }
    }
}