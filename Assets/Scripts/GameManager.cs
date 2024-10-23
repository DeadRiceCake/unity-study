using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float spawnTerm = 1;
    public TextMeshProUGUI scoreText;

    float timeAfterLastSpawn;
    float score;

    void Start()
    {
        timeAfterLastSpawn = 0;
        score = 0;
    }

    void Update()
    {
        timeAfterLastSpawn += Time.deltaTime;
        score += Time.deltaTime;

        if (timeAfterLastSpawn >= spawnTerm)
        {
            timeAfterLastSpawn -= spawnTerm;
            SpawnEnemy();
        }

        scoreText.text = ((int)score).ToString();
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-17f, 18f);
        float y = Random.Range(-6.5f, 7.5f);

        GameObject obj = GetComponent<ObjectPool>().Get();
        obj.transform.position = new Vector2(x, y);
        obj.GetComponent<EnemyController>().Spwan(player);
    }
}
