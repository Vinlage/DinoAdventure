using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAB : MonoBehaviour
{

    [SerializeField]
    private GameObject start = default;
    [SerializeField]
    private GameObject end = default;
    [SerializeField]
    private GameObject movingObject = default;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private SpriteRenderer startSprite = default;
    [SerializeField]
    private SpriteRenderer endSprite = default;

    private bool direction = true;
    private Vector3 objectInitialPosition = default;

    // Start is called before the first frame update
    private void Awake()
    {
        startSprite.enabled = false;
        endSprite.enabled = false;
        objectInitialPosition = movingObject.transform.localPosition;
    }

    void OnEnable()
    {
        movingObject.transform.localPosition = objectInitialPosition;
        direction = true;
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        if(direction)
        {
            movingObject.transform.position = new Vector2(movingObject.transform.position.x + (speed * Time.deltaTime), movingObject.transform.position.y);
        }
        else
        {
            movingObject.transform.position = new Vector2(movingObject.transform.position.x - (speed * Time.deltaTime), movingObject.transform.position.y);
        }

        if(movingObject.transform.position.x >= end.transform.position.x - offset)
        {
            direction = false;
        }
        else if(movingObject.transform.position.x <= start.transform.position.x + offset)
        {
            direction = true;
        }
    }
}
