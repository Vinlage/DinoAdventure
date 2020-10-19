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
    
    private Vector2 mousePosIni;
    private Vector2 mousePos;
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
        if (Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.x > (Screen.width/2))
            {
                return;
            }
            moving = true;
            mousePosIni = Input.mousePosition;
            navigation.transform.position = mousePosIni;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ResetNavigation();
        }

        if(moving)
        {
            mousePos = Input.mousePosition;
            buttonPos = mousePos;
            buttonDistance = mousePos - mousePosIni;
            buttonDistance.x = Mathf.Clamp(buttonDistance.x, -limitBtnPosX * Screen.width, limitBtnPosX * Screen.width);
            buttonPos.x = mousePosIni.x + buttonDistance.x;

            button.transform.position = new Vector2(buttonPos.x, mousePosIni.y);
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
