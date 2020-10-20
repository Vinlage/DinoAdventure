using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 9)
        {
            PlayerController.instance.enabled = false;
            PlayerController.instance.GetAnim().Play("Player_Idle", 0);
            PlayerController.instance.GetAnim().speed = 0;
            StartCoroutine(EndGame.instance.EndScreenWait(true));
        }
    }
}
