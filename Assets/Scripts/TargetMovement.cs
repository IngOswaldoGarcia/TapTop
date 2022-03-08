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
            clickChecker = true;
            verticalDirection = 0;
            anim.SetTrigger("Clicked");
        } 

    }

    void OnMouseDown(){
        if(activeTouch){
            Debug.Log("Click");
/*             Destroy(this.gameObject); */
            clickChecker = true;
            anim.SetTrigger("Clicked");
            
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Clicker")) {
            activeTouch = false;
/*             if(clickChecker){
                Debug.Log("Salio clickeado");
            }else{
                Debug.Log("No salio clickeado");
            }  */
        }
    } 
}
