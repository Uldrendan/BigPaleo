using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggableTile : MonoBehaviour {

    public enum State { Empty, Open, Filled, Covered }

    [HideInInspector]
    public State currState;

    List<SpriteRenderer> _sr;
    LineRenderer _lr;

    [HideInInspector]
    public GameObject occupiedFossil;

    public Sprite dugup;
    public Sprite covered;

    public float sizeX;
    public float sizeY;

    private void Start()
    {
        //_sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _sr = new List<SpriteRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _sr.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        _lr = GetComponent<LineRenderer>();

        currState = State.Empty;

    }

    public void Dig()
    {

        currState = State.Open;
        foreach (SpriteRenderer s in _sr)
            s.sprite = dugup;
    }

    public void Bury(GameObject fossilToBury)
    {
        //Fossil fossilValues = fossilToBury.GetComponent<Fossil>();
        GetComponent<AudioSource>().Play();
        occupiedFossil = Instantiate(fossilToBury, transform.position, transform.rotation);
        occupiedFossil.transform.SetParent(transform);
        currState = State.Filled;
    }

   /* public List<GameObject> FindNeighboursX(int neededNeighbours, List<GameObject> neighboursX)
    {
        neighboursX.Add(gameObject);
        if (neededNeighbours - 1 == 0)
            return neighboursX;
        else

    }*/

    public void Cover()
    {
        currState = State.Covered;
        Destroy(occupiedFossil);
        foreach (SpriteRenderer s in _sr)
            s.sprite = covered;

    }

    public void Highlight()
    {
        _lr.positionCount = 5;
        _lr.SetPosition(0, new Vector3(-sizeX / 2, -sizeY / 2, 0));
        _lr.SetPosition(1, new Vector3(sizeX / 2, -sizeY / 2, 0));
        _lr.SetPosition(2, new Vector3(sizeX / 2, sizeY / 2, 0));
        _lr.SetPosition(3, new Vector3(-sizeX / 2, sizeY / 2, 0));
        _lr.SetPosition(4, new Vector3(-sizeX / 2, -sizeY / 2, 0));
    }

    public void Deselect()
    {
        _lr.positionCount = 0;
    }
}
