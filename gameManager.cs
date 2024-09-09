using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject highManager;
    public TextMeshProUGUI timerText;
    public float timer;
    public int[] timeLimit;
    public int level;
    public GameObject obstacleSpawner;
    public int[] obstacleAmount;
    public GameObject barrier;
    public GameObject arena;
    public float[] arenaSize; //sets the size of the barrier object
    public GameObject enemyManager;
    public int[] waveAmount; //number of waves in the level

    //this was an attempt to make an array of arrays to determine which enemies would appear in each level. It's a work in progress.
    /*public GameObjectStruct[] enemyTypes;
    [System.Serializable]
    public struct GameObjectStruct
    {
        public GameObject[] enemiesInLevel;
    }*/


    void Start()
    {
        //determining which level it is by finding the high manager and reading it's variable
        highManager = GameObject.Find("HighManager");
        level = highManager.GetComponent<highManager>().currentLevel;
        NewLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        //switching to the WorldMap cutscene when the timer is up and the level is over
        if(timer>0)
        {
            timer = timer - Time.deltaTime;
            timerText.text = timer.ToString("Timer: 0");
        }
        else
        {
            highManager.GetComponent<highManager>().currentLevel = level + 1;
            SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }

    void NewLevel(int level)
    {
        //setting up all the spawners and parameters for the new level
        timer = timeLimit[level];
        obstacleSpawner.GetComponent<ObstacleSpawner>().Shuffle(obstacleAmount[level], arenaSize[level]);
        barrier.transform.localScale = new Vector3(arenaSize[level], arenaSize[level], 1);
        arena.transform.localScale = new Vector3(arenaSize[level]/20.5f, arenaSize[level]/20.5f, 1);
        enemyManager.GetComponent<EnemySpawner>().waveAmount = waveAmount[level];
        //enemyManager.GetComponent<EnemySpawner>().timeBetweenWaves = timeLimit[level]/waveAmount[level];
        enemyManager.GetComponent<EnemySpawner>().SpawnEnemy();
        enemyManager.GetComponent<EnemySpawner>().InvokeRepeating("SpawnEnemy", timeLimit[level] / waveAmount[level], timeLimit[level] / waveAmount[level]);
        //enemyManager.GetComponent<EnemySpawner>().enemies = enemyTypes[level];
    }
}
