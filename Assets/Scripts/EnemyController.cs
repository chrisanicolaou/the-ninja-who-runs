using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Transform target;
    private Animator _animator;
    private Rigidbody2D _rigid;
    private bool isBusy = false;
    public float speed = 3f;
    public EnemyHealthCheck health;
    private CapsuleCollider2D _collider;

    public float maxHP = 50f;
    public float HP;


    void Start()
    {
        player = GameObject.Find("Ninja");
        target = player.GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        HP = maxHP;
        health.SetHealth(HP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        health.SetHealth(HP, maxHP);
        _animator.SetFloat("Speed", 1); // Mathf.Abs(_rigid.velocity.magnitude));
        if (!isBusy)
        {
        if(HP <= 0)
        {
            isBusy = true;
            Player.currEXP += 50;
            StartCoroutine("Death");
        }
        transform.rotation = Quaternion.Euler(0f, transform.position.x - target.position.x > 0 ? 0f: 180f, 0f);
        transform.position -= transform.right * Time.deltaTime * speed;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !isBusy)
        {
            StartCoroutine("AttackPlayer");
        }
    }

    IEnumerator AttackPlayer()
    {
        isBusy = true;
        Player.currHealth = Mathf.Max((Player.currHealth - 1f), 0f);
        Debug.Log(Player.currHealth);
        _animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("isAttacking", false);
        isBusy = false;
    }

    IEnumerator Death()
    {
        _animator.SetBool("isDead", true);
        _collider.enabled = false;
        _rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
