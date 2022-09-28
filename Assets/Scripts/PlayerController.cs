using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //player movement
    public PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    private float walkSpeed = 10f;
    public Camera playerCamera;
    Vector3 cameraRotation;
    
    //Player jump
    Rigidbody rb;
    private float distanceToGround;
    private bool isGrounded = true;
    public float JumpHeight = 5f;

    //Player animation
    Animator playerAnimator;
    private bool isWalking = false;
    private bool isShooting = false;

    //Projectile bullets
    public GameObject bullet;
    public Transform projectilePos;


    //Awake to start
    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }

        //using controller from PlayerInputController
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Shoot.performed += cntxt => Shoot();

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private void Jump()
    {
            
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            isGrounded = false;
        }

    }

    public void Shoot()
    {

        Destroy(bullet);
        Rigidbody bulletRb = Instantiate(bullet,projectilePos.position,Quaternion.identity).GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 5f, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * move.y* Time.deltaTime * walkSpeed, Space.Self);

        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }
}
