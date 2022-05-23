using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    public static SoundSystem instance;

    public AudioClip[] music = new AudioClip[10];
    public AudioSource audioSource;

    private void Awake() {
        if (SoundSystem.instance == null){
            SoundSystem.instance = this;
        }else if(SoundSystem.instance != this){
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(){
        audioSource.clip = music[0];
        audioSource.Play();
    }

    public void StopMusic(){
        audioSource.Stop();
    }

    private void OnDestroy() {
        if(SoundSystem.instance == this){
            SoundSystem.instance = null;
        }
    }
}
