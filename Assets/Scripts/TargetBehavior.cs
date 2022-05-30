using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    bool clickChecker;
    bool activeTouch;

    private Animator anim;
    private SpriteRenderer rend;

    TargetMovement targetMovement;

    private void Awake() {
        anim = GetComponent<Animator>();    
        targetMovement = gameObject.GetComponentInParent<TargetMovement>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        clickChecker = false;
        activeTouch = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Clicker")) {            
            activeTouch = true;
        } 
    }

    void OnMouseDown(){
        if(activeTouch){
            targetMovement.verticalDirection = 0;
            clickChecker = true;
            activeTouch = false;
            rend.sortingOrder = 1;
            anim.SetTrigger("Clicked");
            GameController.instance.UpdateScore();
            SpawnWaves();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Clicker")) {
            activeTouch = false;
          if(!clickChecker){
                GameController.instance.SetEndMessage();
            }
        }
    } 

    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(1f);
        Debug.Log("Eliminado");
        Destroy(targetMovement);
    }
}
