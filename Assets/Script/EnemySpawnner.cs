using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    [SerializeField] private int maxEnemies;
    [SerializeField] private int ContBots;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform playerPos;
    private float tempo;
    private EnemyController enemyController;
    void Start()
    {
        maxEnemies = 100;
        ContBots = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempo >= 0.1f)
        {
            SpawnBot();
            tempo = 0f;
        }
        else
            tempo += Time.deltaTime;
    }


    private void SpawnBot()
    {
        if (ContBots < maxEnemies)
        {
            GameObject bot = Instantiate(prefab, new Vector3(Random.Range(0, 400), Random.Range(1, 4), Random.Range(0, 400)), Quaternion.identity);
            enemyController = bot.GetComponent<EnemyController>();
            enemyController.setPos(ref playerPos);
            ContBots++;
        }
    }

}
