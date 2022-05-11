using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    [Header("Movimieto De Target")]
    public float horizontalDirection;
    private Rigidbody2D rb2d;
    private float desplazamiento = 0.001f;
    public float screenSize;
    public float verticalDirection;

    bool clickChecker;
    bool activeTouch;

    private Animator anim;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        screenSize = ((float)Screen.width / (float)110);
        clickChecker = false;
        activeTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.transform.Translate(Vector2.down * (verticalDirection * (desplazamiento  * 2)));
        rb2d.transform.Translate(Vector2.right * (horizontalDirection * (desplazamiento  * 2)));

        if(rb2d.transform.position.x > screenSize || rb2d.transform.position.x < ( -1 * screenSize)){
            horizontalDirection = -1 * horizontalDirection;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Clicker")) {            
            activeTouch = true;
        } 

    }

    void OnMouseDown(){
        if(activeTouch){
            Debug.Log("Click");
/*             Destroy(this.gameObject); */
            verticalDirection = 0;
            clickChecker = true;
            activeTouch = false;
            anim.SetTrigger("Clicked");
            GameController.instance.UpdateScore();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Clicker")) {
            activeTouch = false;
            Debug.Log("Salida" + clickChecker);
          if(!clickChecker){
                GameController.instance.SetLoserMessage();
            }
        }
    } 
}
