using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Camera cam = default;
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private float offsetY = 1.5f;
    [SerializeField]
    private Transform leftLimit = default;
    [SerializeField]
    private Transform rightLimit = default;
    [SerializeField]
    private float minHeight = 0;

    private float posX;
    private float posY;
    private float height;
    private float width;
    
    // Start is called before the first frame update
    void Start()
    {
        height = 2f * cam. orthographicSize;
        width = height * cam. aspect;
    }

    private void LateUpdate() {
        posX = Mathf.Clamp(player.transform.position.x, leftLimit.position.x + (width/2), rightLimit.position.x - (width/2));
        posY = Mathf.Max(player.transform.position.y+offsetY, minHeight);
        this.transform.position = new Vector3(posX, posY, this.transform.position.z);
    }
}
