using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody component for physics
    private Rigidbody2D _rb;

    //Speed variables to be adjusted in inspector
    public float speed;
    public float jumpForce;
    public float rollForce;

    //Gets running input from user, and determines which direction the user is running in
    private float moveInput;

    //Logic for jump stuff
    private bool canJumpOffThis;
    private bool isJumping = false;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTime;
    private float jumpTimeCounter;
    private Animator animator;
    private bool isRolling = false;
    private CircleCollider2D crouchCol;

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crouchCol = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //Checks to see if surface at feet is a 'jump' surface (and not something like sludge)
        canJumpOffThis = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (!isRolling) {

            //Controls animation for running/jumping
            animator.SetFloat("speed", Mathf.Abs(moveInput));
            animator.SetBool("isJump", Mathf.Abs(_rb.velocity.y) > 1 ? true : false);

            //Changes direction ninja is facing based on input
            if (moveInput > 0) {
                transform.eulerAngles = new Vector3(0, 0, 0);
                rollForce = Mathf.Abs(rollForce);
            } else if (moveInput < 0 && transform.eulerAngles != new Vector3(0, 180, 0)) {
                transform.eulerAngles = new Vector3(0, 180, 0);
                rollForce = rollForce * -1;
            }

            if (canJumpOffThis && !isJumping && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jumpForce;
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isJumping) {
                if (jumpTimeCounter > 0) {
                    _rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                } else {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
                isJumping = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                isRolling = true;
                StartCoroutine("RollNinja");
            }
        }
    }
    void FixedUpdate()
    {
        if (!isRolling){
            moveInput = Input.GetAxisRaw("Horizontal");
            _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);
        }
    }

    IEnumerator RollNinja()
    {
        animator.SetBool("isRolling", isRolling);
        crouchCol.enabled = false;
        _rb.velocity = new Vector2(rollForce, _rb.velocity.y);
        yield return new WaitForSeconds(0.4f);
        isRolling = false;  
        animator.SetBool("isRolling", isRolling);
        crouchCol.enabled = true;
    }
}


        // if (_rb.velocity.y < 0)
        // {
        //     Debug.Log("Coming Down");
        //     _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        // }
        // else if (_rb.velocity.y > 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        // {
        //     Debug.Log("Going up!");
        //     _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        // }
