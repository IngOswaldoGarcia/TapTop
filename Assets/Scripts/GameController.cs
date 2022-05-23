using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.IO;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject[] respawns;

    public GameObject target;

    public GameObject startButton;

    public Vector3 spawnValues;

    public TextAsset jsonFile;

    public float spawnWait;

    public float waveWait;

    public float screenSize;

    public float counter = 0;

    public bool activeMusic;

    public float velocity = 5.0f;

    public Text textScore;
    public Text loserMessage;

    int scorePoints = 0;

    ConstantsBehavior constants = new ConstantsBehavior();

    /*     string info = JsonUtility.FromJson<GameController>(textAsset.text); */
    void Awake()
    {
        if (GameController.instance == null)
        {
            GameController.instance = this;
        }
        else if (GameController.instance != this)
        {
            Destroy (gameObject);
        }
    }

    void Start()
    {
        /*         StartCoroutine(SpawnWaves()); */
        screenSize = ((float) Screen.width / (float) 110);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        StartCoroutine(SpawnWaves());
        SoundSystem.instance.PlayMusic();
        startButton.SetActive(false);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(constants.START_AWAIT);

        /* Trigger enterTrigger = JsonUtils.ImportJson<Trigger>("Json/enter"); */
        for (int j = 0; j < constants.TARGET_BEHAVIOR.Length; j++)
        {
            for (int i = 0; i < constants.TARGET_BEHAVIOR[j, 0]; i++)
            {
                float random_direction = Random.Range(-1, 2);
                target.GetComponent<TargetMovement>().horizontalDirection = 0;
                 target.GetComponent<TargetMovement>().verticalDirection = velocity; 
                if (j > 57)
                {
                    spawnWait = 0.0001f;
                }
                Vector3 spawnPosition =
                    new Vector3(Random.Range(-screenSize, screenSize),
                        spawnValues.y,
                        spawnValues.z);
                Instantiate(target, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(constants.TARGET_BEHAVIOR[j, 1]);
        }
    }

    public void UpdateScore() {
        textScore.text = "Score: " + ++scorePoints; 
    }

    public void SetLoserMessage() {
        loserMessage.gameObject.SetActive(true);
        SoundSystem.instance.StopMusic();
        respawns = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject respawn in respawns)
        {
            Destroy(respawn);
        }
    }
}
