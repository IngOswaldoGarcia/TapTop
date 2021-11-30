using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEliminator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Target")) {
            Destroy(other.gameObject);
        }
    }
}
