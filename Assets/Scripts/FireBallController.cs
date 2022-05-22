using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    private float spawnTime;
    private bool alreadyDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
        if (Time.time - spawnTime > 4)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && alreadyDamaged == false)
        {
            alreadyDamaged = true;
            col.gameObject.GetComponent<EnemyController>().HP -= 20 + (Player.currLevel * 3);
            Destroy(gameObject);
        }
    }
}
