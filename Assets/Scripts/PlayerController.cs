using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public GameObject fireball;
    private bool isAttacking = false;
    private bool isFireballReady = true;
    private bool isPowerAttack = false;
    private Image _fireballHUD;
    private ManaManager _mana;
    
    private Transform _ninjaPos;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _ninjaPos = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _fireballHUD = GameObject.Find("FireballCooldown").GetComponent<Image>();
        _mana = GameObject.Find("ManaBarManager").GetComponent<ManaManager>();
        InvokeRepeating("PassiveManaGain", 1f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            // _horizontalSpeed = Input.GetAxisRaw("Horizontal") * speed;
            // //Start/stop running animation
            // _animator.SetFloat("Speed", Mathf.Abs(_horizontalSpeed));

            // direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            // if (isTurningLeft) {
            //     _rb.rotation = 45f;
            //     rollDir = -1;
            //     isTurningLeft = false;
            // }

            // if (isTurningRight)
            // {
            //     // _rb.SetRotation()
            //     rollDir = 1;
            //     isTurningRight = false;
            // }
            // //Jump logic/movement
            // if (isJumping)
            // {
            //     if (isGrounded)
            //     {
            //         isGrounded = false;
            //         _animator.SetBool("isJump", true);
            //         // _rb.velocity = new Vector2(0, 20);
            //         _rb.AddForce(new Vector2(0, 20));
            //     }
            // isJumping = false;
            // }

            // //Roll logic
            // if (isRolling)
            // {
            // StartCoroutine("RollNinja");
            // isRolling = false;
            // }

            // _rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
}
    void Update()
    {
        if (!isFireballReady)
        {
            _fireballHUD.color = new Color(0, 0, 0, 1);
        } 
        else if (isFireballReady && Player.currMana >= 10)
        {
            _fireballHUD.color = new Color(255, 255, 255, 255);
        }
        if (Player.currHealth <= 0)
        {
            StartCoroutine("NinjaDie");
        }
        // if (!isBusy){

            // RotateNinja();
            // //Running movement
            // _ninja.transform.position += direction * Time.deltaTime * speed;

            // if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            // {
            //     isJumping = true;
            // }

            // if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            // {
            //     isTurningLeft = true;
            //     // _ninjaPos.rotation = Quaternion.Euler(0f, 180f, 0f);
            //     // rollDir = -1;
            // }

            // if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            // {
            //     isTurningRight = true;
            //     // _ninjaPos.rotation = Quaternion.Euler(0f, 0f, 0f);
            //     // rollDir = 1;
            // }

            // if (Input.GetKeyDown(KeyCode.LeftShift))
            // {
            //     isRolling = true;
            // }

            //BasicAttack logic
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine("AttackNinja");
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking)
            {
                isAttacking = true;
                isPowerAttack = true;
                StartCoroutine("AttackNinja");
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && isFireballReady && Player.currMana >= 10)
            {
                isAttacking = true;
                Player.currMana -= 10;
                StartCoroutine("CastFireball");
            }
        // }
    }

    //Prevents double jump
    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.tag != "NoJump" && col.gameObject.tag != "CanDestroy")
    //     {
    //         _animator.SetBool("isJump", false);
    //         isGrounded = true;
    //     }
    // }

    // void OnCollisionExit2D(Collision2D col)
    // {
    //     if (_rb.velocity.y > 0)
    //     {
    //     isGrounded = false;
    //     }
    // }

    // void RotateNinja()
    // {
    //     //Controls which direction character is facing
    //     if (_horizontalSpeed > 0)
    //     {
    //         _ninjaPos.rotation = Quaternion.Euler(0f, 0f, 0f);
    //         rollDir = 1;
    //     }
    //     if (_horizontalSpeed < 0)
    //     {
    //         _ninjaPos.rotation = Quaternion.Euler(0f, 180f, 0f);
    //         rollDir = -1;
    //     }
    // }

    void PassiveManaGain()
    {
        if (Player.currMana < Player.maxMana)
        {
            Player.currMana = Mathf.Min(Player.currMana + 1, Player.maxMana);
        }
    }

    
    IEnumerator AttackNinja()
    {

        _animator.SetBool(isPowerAttack ? "isPowerAttacking" : "isBasicAttacking", true);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.right, isPowerAttack ? 3f : 1.6f);
        for(int i = 0; i < hit.Length; i++)
        {
            GameObject currentRay = hit[i].collider.gameObject;
            switch (currentRay.tag)
            {
                case "Enemy":
                    if (i == 0 || !(GameObject.ReferenceEquals(currentRay, hit[i-1].collider.gameObject)))
                    {
                        currentRay.GetComponent<EnemyController>().HP -= isPowerAttack ? 20 : 10;
                    }
                    break;
                case "CanDestroy":
                    Tilemap tilemap = GameObject.Find("DestroyTiles").GetComponent<Tilemap>();
                    tilemap.SetTile(tilemap.WorldToCell(new Vector3(transform.position.x + hit[i].distance, transform.position.y, transform.position.z)), null);
                    tilemap.SetTile(tilemap.WorldToCell(new Vector3(transform.position.x + hit[i].distance + 1, transform.position.y, transform.position.z)), null);
                    tilemap.SetTile(tilemap.WorldToCell(new Vector3(transform.position.x + hit[i].distance, transform.position.y + 1, transform.position.z)), null);
                    tilemap.SetTile(tilemap.WorldToCell(new Vector3(transform.position.x + hit[i].distance + 1, transform.position.y + 1, transform.position.z)), null);
                    break;
            }
        }
        yield return new WaitForSeconds(isPowerAttack ? 0.4f : 0.2f);
        _animator.SetBool(isPowerAttack ? "isPowerAttacking" : "isBasicAttacking", false);
        isPowerAttack = false;
        isAttacking = false;
    }

    IEnumerator CastFireball()
    {
        _animator.SetBool("isFireballing", true);
        isFireballReady = false;
        yield return new WaitForSeconds(0.3f);
        Instantiate(fireball, transform.position, _ninjaPos.rotation);
        yield return new WaitForSeconds(0.3f);
        _animator.SetBool("isFireballing", false);
        isAttacking = false;
        yield return new WaitForSeconds(4.4f);
        isFireballReady = true;
    }

    //Roll movement
    // IEnumerator RollNinja()
    // {
    //     isBusy = true;
    //     _animator.SetBool("isRolling", true);
    //     _crouchCol.enabled = false;
    //     // if (_rb.velocity.x  0)
    //     // {
    //     //     _ninja.transform.position += new Vector3(_ninja.transform.position.x + 0.2f, _ninja.transform.position.x + 0.2f);
    //     // }
    //     _rb.velocity = new Vector2(rollDir * 20f, 0);
    //     yield return new WaitForSeconds(0.4f);
    //     isBusy = false;
    //     _animator.SetBool("isRolling", false);
    //     _crouchCol.enabled = true;
    // }

    IEnumerator NinjaDie()
    {
        // isBusy = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("EndGame");
    }
}
