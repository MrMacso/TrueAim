using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI waveText;
    private float spawnTime = 10;
    private float currentTime;

    private float enemyAmount = 1;

    private float maxNumOfWaves = 10;
    private float currNumOfWaves = 0;

    private float totalDeadMonster;
    private float currentDeadMonster = 0;

    private bool isGameRunning = true;
    private bool isLevelComplete = false;
    private bool isLevelStarted = false;




    private void Start()
    {
        currentTime = spawnTime;
        waveText.text = "Wave " + currNumOfWaves.ToString() + "/" + maxNumOfWaves.ToString();
    }
    void Update()
    {
        if (isLevelStarted)
        {
            SetupPortal(3,1,10);
            totalDeadMonster = enemyAmount * maxNumOfWaves;
            isLevelStarted = false;
        }
        if (isGameRunning)
        {
            if (currNumOfWaves < maxNumOfWaves)
            {
                if (currentTime <= 0f)
                {
                    StartCoroutine(SummonWave());
                    currentTime = spawnTime;
                }
            }
            currentTime -= Time.deltaTime;

            if (currentDeadMonster == totalDeadMonster && !isLevelComplete)
            {
                isLevelComplete = true;
            }
        }
    }
    IEnumerator SummonWave()
    {
        currNumOfWaves++;
        waveText.text = "Wave: " + currNumOfWaves.ToString() + "/" + maxNumOfWaves.ToString();

        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(3.0f);
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
    private void SetupPortal(int spawnT, int eAmount, int maxWave)
    {
        spawnTime = spawnT;
        enemyAmount = eAmount;
        maxNumOfWaves = maxWave;
    }
    public void AddToDeadCounter()
    {
        currentDeadMonster++;
    }
    public void SetIsGameRunning(bool isrunning)
    {
        isGameRunning = isrunning;
    }
    public void StartLevel()
    {
        isLevelStarted = true;
    }
}
