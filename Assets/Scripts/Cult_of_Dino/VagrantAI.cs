using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagrantAI : MonoBehaviour {

    public GameObject[] waypoints;

    public float speed;

    Rigidbody2D _rb;
    Animator anim;

    int currentIndex;

    public GameObject target;

	void Start () {
        Physics2D.IgnoreLayerCollision(12, 12);
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentIndex = waypoints.Length - 1;
        MoveToNextWaypoint();
	}

    void Update()
    {
       
            Vector2 targetDirection = (target.transform.position - transform.position).normalized;
        _rb.velocity = targetDirection * speed;
        
        HandleAnimation();
    }

    public void MoveToNextWaypoint()
    {
        if (currentIndex == waypoints.Length - 1)
            currentIndex = 0;
        else
            currentIndex += 1;
        target = waypoints[currentIndex];
        _rb.velocity = Vector2.zero;
        Vector2 targetDirection = (target.transform.position - transform.position).normalized;
        _rb.velocity = targetDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Waypoint")
        {
            MoveToNextWaypoint();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameMaster.instance.GameOver(false);
        }
    }

    void HandleAnimation()
    {
        if(Mathf.Abs(_rb.velocity.x) > Mathf.Abs(_rb.velocity.y)) //if we are moving more horizontal than vertical
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", false);
            if (_rb.velocity.x > 0)
            {
                anim.SetBool("RightMove", true);
                anim.SetBool("LeftMove", false);
            }
            else
            {
                anim.SetBool("RightMove", false);
                anim.SetBool("LeftMove", true);
            }
        }
        else
        {
            anim.SetBool("LeftMove", false);
            anim.SetBool("RightMove", false);
            if(_rb.velocity.y > 0)
            {
                anim.SetBool("UpMove", true);
                anim.SetBool("DownMove", false);
            }
            else
            {
                anim.SetBool("UpMove", false);
                anim.SetBool("DownMove", true);
            }
        }
    }
}
