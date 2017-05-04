using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Animator anim;

    Rigidbody2D _rb;

    GameObject currentTile;

    public float speed;

    public float numFossils = 4;

    public GameObject[] fossils;

    public float digPercentage;

    void Start ()
    {
        anim = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        //RaycastHit hit;

        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        if(_rb.velocity != Vector2.zero)
        {
            StopAllCoroutines();
            digPercentage = 0;
        }

        HandleAnimations();

        if (Input.GetKeyDown("j"))
        {
            if (currentTile != null && currentTile.GetComponent<DiggableTile>() != null)
            {
                if (currentTile.GetComponent<DiggableTile>().currState == DiggableTile.State.Empty)
                {
                    StartCoroutine(StartDigging());
                }
                else if (currentTile.GetComponent<DiggableTile>().currState == DiggableTile.State.Open)
                {
                    StartCoroutine(StartPlacing());
                }
                else if (currentTile.GetComponent<DiggableTile>().currState == DiggableTile.State.Filled)
                {
                    StartCoroutine(StartCovering());
                }
            }
        }

        if (numFossils == 0)
            GameMaster.instance.GameOver(true);
    }

    void HandleAnimations()
    {
        anim.SetFloat("VerticalSpeed", _rb.velocity.y);
        anim.SetFloat("HorizontalSpeed", _rb.velocity.x);

        if (_rb.velocity.x != 0 && !anim.GetBool("MoveVertical"))
            anim.SetBool("MoveHorizontal", true);
        else if (_rb.velocity.x == 0)
            anim.SetBool("MoveHorizontal", false);

        if (_rb.velocity.y != 0 && !anim.GetBool("MoveHorizontal"))
            anim.SetBool("MoveVertical", true);
        else if (_rb.velocity.y == 0)
            anim.SetBool("MoveVertical", false);

        if (_rb.velocity == Vector2.zero)
        {
            anim.SetTrigger("Stop");
            anim.SetBool("MoveVertical", false);
            anim.SetBool("MoveHorizontal", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Diggable")
        {
            currentTile = collision.gameObject;
            currentTile.GetComponent<DiggableTile>().Highlight();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diggable")
            currentTile.GetComponent<DiggableTile>().Deselect();
    }

    IEnumerator StartDigging()
    {
        GetComponent<AudioSource>().Play();
        while (digPercentage <= 1)
        {
            digPercentage += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentTile.GetComponent<DiggableTile>().Dig();
        digPercentage = 0;
    }

    IEnumerator StartPlacing()
    {
        while (digPercentage <= 1)
        {
            digPercentage += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (currentTile.layer == 8)
            currentTile.GetComponent<DiggableTile>().Bury(fossils[0]);
        else if (currentTile.layer == 9)
            currentTile.GetComponent<DiggableTile>().Bury(fossils[1]);
        else if (currentTile.layer == 10)
            currentTile.GetComponent<DiggableTile>().Bury(fossils[2]);
        else if (currentTile.layer == 11)
            currentTile.GetComponent<DiggableTile>().Bury(fossils[4]);
        digPercentage = 0;
    }

    IEnumerator StartCovering()
    {
        while (digPercentage <= 1)
        {
            digPercentage += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentTile.GetComponent<DiggableTile>().Cover();
        numFossils -= 1;
        digPercentage = 0;
    }
}