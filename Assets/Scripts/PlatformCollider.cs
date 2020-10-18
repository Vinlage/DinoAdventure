using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.transform.parent = null;
        }
    }
}
