using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{

    public static MobileController instance;

    [SerializeField]
    private GameObject navigation = default;
    [SerializeField]
    private GameObject button = default;
    [SerializeField]
    private float limitBtnPosX = 0.06f;
    [SerializeField]
    private float sensitivity = 0.0005f;
    
    private Vector2 touchPosIni;
    private Vector2 touchPos;
    private Vector2 buttonPos;
    private Vector2 buttonDistance;
    private Vector2 navigationInitPosition;
    private bool moving;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        navigationInitPosition = navigation.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on current position of the touch each frame
            if(touch.position.x > (Screen.width/2) || touch.phase == TouchPhase.Ended)
            {
                print("reset");
                ResetNavigation();
                return;
            }
            else if(touch.phase == TouchPhase.Began)
            {
                print("began");
                moving = true;
                touchPosIni = touch.position;
                navigation.transform.position = touchPosIni;
            }

            if(moving)
            {
                touchPos = touch.position;
                buttonPos = touchPos;
                buttonDistance = touchPos - touchPosIni;
                buttonDistance.x = Mathf.Clamp(buttonDistance.x, -limitBtnPosX * Screen.width, limitBtnPosX * Screen.width);
                buttonPos.x = touchPosIni.x + buttonDistance.x;

                button.transform.position = new Vector2(buttonPos.x, touchPosIni.y);
            }
        }

        

        
    }

    public void ResetNavigation()
    {
        moving = false;
        navigation.transform.localPosition = navigationInitPosition;
        button.transform.localPosition = Vector2.zero;
    }

    public float GetControllerPosition()
    {
        if(navigation.activeSelf)
        {
            return button.transform.localPosition.x * sensitivity;
        }

        return 0;
    }
}
