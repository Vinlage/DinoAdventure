using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    [SerializeField]
    private float maxWalkSpeed = 2;
    [SerializeField]
    private float maxRunSpeed = 3;
    [SerializeField]
    private float walkAccel = 0.1f;
    [SerializeField]
    private float runAccel = 0.2f;
    [SerializeField]
    private float jumpMultiplier = 0.2f;
    [SerializeField]
    private Animator playerAnimator = default;
    [SerializeField]
    private new Rigidbody2D rigidbody2D;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private Button jumpBtn = default;

    private float speed = 0;
    private float accel = 0;
    private float maxSpeed = 0;
    private bool jumping = false;
    private bool falling = false;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        jumpBtn.onClick.AddListener(delegate{Jump();});
    }

    private void OnEnable()
    {
        playerAnimator.Play("Player_Idle", 0);
        playerAnimator.speed = 1;
        jumpBtn.interactable = true;
        jumping = false;
        falling = false;
    }

    private void OnDisable()
    {
        if(jumpBtn)
        {
            jumpBtn.interactable = false;
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        OnGround();
        GetInputs();
    }

    public void GetInputs()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Mathf.Abs(MobileController.instance.GetControllerPosition()) > 0.03f)
        {
            accel = runAccel;
            maxSpeed = maxRunSpeed;
        }
        else
        {
            accel = walkAccel;
            maxSpeed = maxWalkSpeed;
        }

        if(MobileController.instance.GetControllerPosition() < 0)
        {
            speed = Mathf.Max(speed-accel, -maxSpeed);
            Walk(speed*Time.deltaTime, 180);
        }
        else if(MobileController.instance.GetControllerPosition() > 0)
        {
            speed = Mathf.Min(speed+accel, maxSpeed);
            Walk(speed*Time.deltaTime, 0);
        }
        else
        {
            speed = speed > 0 ? Mathf.Max(speed-(accel*2), 0) : Mathf.Min(speed+(accel*2), 0);
        }

        if(speed == 0)
        {
            playerAnimator.SetInteger("speed", 0);
        }
        else if(Mathf.Abs(speed) <= maxWalkSpeed)
        {
            playerAnimator.SetInteger("speed", 1);
        }
        else
        {
            playerAnimator.SetInteger("speed", 2);
        }
        
    }

    public void Walk(float speed, float angle)
    {
        
        transform.eulerAngles = new Vector3(0, angle, 0);
        this.transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public void Jump()
    {
        if(!jumping)
        {
            rigidbody2D.velocity += Vector2.up * jumpMultiplier;
            jumping = true;
            playerAnimator.Play("Player_Jump", 0);
        }
        
    }

    public void OnGround()
    {
        if(rigidbody2D.velocity.y < 0)
        {
            falling = true;
        }

        if(rigidbody2D.velocity.y == 0 && falling)
        {
            jumping = false;
            falling = false;
            playerAnimator.Play("Player_Idle", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == 8 || other.gameObject.layer == 10)
        {
            Die();
        }
    }

    public void Die()
    {
        playerAnimator.Play("Player_Dead", 0);
        transform.gameObject.layer = 11;
        this.enabled = false;
        this.gameObject.transform.parent = null;

        StartCoroutine(EndGame.instance.EndScreenWait(true, false));
    }

    public Animator GetAnim()
    {
        return playerAnimator;
    }

}
