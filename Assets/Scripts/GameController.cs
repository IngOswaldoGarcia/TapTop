using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.IO;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{
    public GameObject target;
    public Vector3 spawnValues;
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

    float[,] targetBehaviour = {{0, 1f},{3, 0.5f},{1, 0.255f},{1, 0.225f},{2, 1},{3, 0.55f},{1, 0.15f},{1, 0.15f},{1, 0.55f},{2, 0.55f},{2, 0.55f}};


/*     string info = JsonUtility.FromJson<GameController>(textAsset.text); */
        
    [System.Serializable]

    public class TimesList {
        public string[] target_times;
    }

    public TimesList completeTimesList = new TimesList();

    void Start()
    {
        StartCoroutine(SpawnWaves());
        screenSize = ((float)Screen.width / (float)110);
/*         string json = File.ReadAllText(Application.dataPath + "/Scripts/Json/times_game_01.json");
        string data = JsonSerializer.Deserialize<IList<GameController>>(json); */
/*         TimesList data = JsonUtility.FromJson<TimesList>(json); */
/*         Debug.Log(data[0]); */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        SoundSystem.instance.PlayMusic();
        /* Trigger enterTrigger = JsonUtils.ImportJson<Trigger>("Json/enter"); */
        for (int j = 0; j < targetBehaviour.Length; j++){
/*             Debug.Log(waveWaitArray[j]); */
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

        
/*         while (true) {
            for (int i = 0; i < targetsCount; i++)
            { */
/*                 float random_velocity = Random.Range(1, 10); */
/*                 float random_direction = Random.Range(-1, 2);
                target.GetComponent<TargetMovement>().incremento = 5;
                target.GetComponent<TargetMovement>().horizontalDirection = random_direction; */
/*                 if(i == 2){
                    target.GetComponent<TargetMovement>().incremento = 2f;
                }else if(i == 6){
                    target.GetComponent<TargetMovement>().incremento = 6f;Esta
                }else if(i == 10){
                    target.GetComponent<TargetMovement>().incremento = 10f;
                }else if(i == 15){
                    target.GetComponent<TargetMovement>().incremento = 2f;
                } */
                
/*                 Vector3 spawnPosition = new Vector3(Random.Range(-screenSize, screenSize), spawnValues.y, spawnValues.z);
                Instantiate(target, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            } */
            
}


