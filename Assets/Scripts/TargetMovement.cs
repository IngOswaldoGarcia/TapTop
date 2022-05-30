using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    [Header("Movimieto De Target")]
    public float horizontalDirection;
    private Rigidbody2D rb2d;
    public float desplazamiento = 0.001f;
    public float screenSize;

    public float verticalDirection;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Use this for initialization
    void Start()
    {
        screenSize = ((float)Screen.width / (float)110);
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


}
