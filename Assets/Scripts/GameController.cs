using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.IO;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject target;
    public GameObject startButton;
    public Vector3 spawnValues;
    public TextAsset jsonFile;
/*     public int targetsCount; */
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float screenSize;
    public float counter = 0;

    float[] waveWaitArray = {1f,0.5f,0.255f,0.225f,1,0.55f,0.15f,0.15f,0.55f,0.55f,0.55f,0.55f,0.225f,0.225f,0.225f,0.225f,0.225f,0.225f,0.225f,0.15f,0.3f,0.3f,0.3f,0.3f
    ,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.25f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.3f,0.2f,0.2f,0.2f,0.2f,0.2f,0.2f,0.2f,0.1f,0.1f
    ,1.00f,2.00f,3.00f};
    int[] targetsCount = {0,3,1,1,2,3,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,140,1,1,1,1,15,1,1,1,1,15,1,1,1,1,15,1,1,1,1,7,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,1,10000};

    float[,] targetBehaviour = {{1, 0.41f},{1, 0.23f},{1, 0.18f},{1, 0.41f},{1, 0.4f},{1, 0.4f},{1, 0.22f},{1, 0.22f},{1, 0.22f},{1, 0.22f},{1, 0.4f},{1, 0.4f},{1, 0.23f},{1, 0.20f},{1, 0.4f},{1, 0.3f},{1, 0.42f},{1, 0.4f},{1, 0.4f},{1, 0.5f},
{1, 0.31f},{1, 0.23f},{1, 0.18f},{1, 0.41f},{1, 0.4f},{1, 0.4f},{1, 0.22f},{1, 0.22f},{1, 0.22f},{1, 0.22f},{1, 0.4f},{1, 0.4f},{1, 0.23f},{1, 0.20f},{1, 0.4f},{1, 0.3f},{1, 0.42f},{1, 0.4f},{1, 0.4f},{1, 0.5f}};
    public bool activeMusic;


/*     string info = JsonUtility.FromJson<GameController>(textAsset.text); */
    
    void Awake()
    {
        if (GameController.instance == null){
            GameController.instance = this;
        }else if(GameController.instance != this){
            Destroy(gameObject);
        }
    }

    void Start()
    {
/*         StartCoroutine(SpawnWaves()); */
        screenSize = ((float)Screen.width / (float)110);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame (){
        StartCoroutine(SpawnWaves());
        SoundSystem.instance.PlayMusic();
        startButton.SetActive(false);
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);

        /* Trigger enterTrigger = JsonUtils.ImportJson<Trigger>("Json/enter"); */
        for (int j = 0; j < targetBehaviour.Length; j++){
            for (int i = 0; i < targetBehaviour[j, 0]; i++){
                float random_direction = Random.Range(-1, 2);
                target.GetComponent<TargetMovement>().horizontalDirection = 0;
                if (j > 57){
                    spawnWait = 0.0001f;
                }
                Vector3 spawnPosition = new Vector3(Random.Range(-screenSize, screenSize), spawnValues.y, spawnValues.z);
                Instantiate(target, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(targetBehaviour[j,1]);
        }
    }

    
            
}


