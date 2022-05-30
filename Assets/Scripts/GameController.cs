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
    public GameObject[] respawnsTargets;
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

    Coroutine targetMovementCoroutine;
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
        screenSize = ((float) Screen.width / (float) 110);
    }

    public void StartGame()
    {
        DeleteSpareTargets();
        SoundSystem.instance.PlayMusic();
        targetMovementCoroutine = StartCoroutine(SpawnWaves());

        startButton.SetActive(false);
        loserMessage.gameObject.SetActive(false);

        scorePoints = 0;
        textScore.text = "Score: " + scorePoints; 
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(constants.START_AWAIT);

        for (int j = 0; j < (constants.TARGET_BEHAVIOR.Length / 2); j++)
        {
            for (int i = 0; i < constants.TARGET_BEHAVIOR[j, 0]; i++)
            {
                float random_direction = Random.Range(-1, 2);
                target.GetComponent<TargetMovement>().horizontalDirection = 0;
                target.GetComponent<TargetMovement>().verticalDirection = velocity; 
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

    public void SetEndMessage() {
        StopCoroutine(targetMovementCoroutine);
        loserMessage.gameObject.SetActive(true);
        SoundSystem.instance.StopMusic();

        startButton.SetActive(true);
        respawns = GameObject.FindGameObjectsWithTag("TargetContainer");
        respawnsTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject respawn in respawns)
        {
            respawn.GetComponent<TargetMovement>().verticalDirection = 0;
        }
        foreach (GameObject respawnsTarget in respawnsTargets)
        {
            respawnsTarget.GetComponent<Animator>().SetTrigger("Clicked");
        }
    }

    public void DeleteSpareTargets() {
        respawns = GameObject.FindGameObjectsWithTag("TargetContainer");
        if (respawns.Length > 0) {
            foreach (GameObject respawn in respawns){
                Destroy(respawn);
            }
        }
    }
}
