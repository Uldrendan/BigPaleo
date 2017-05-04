using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FossilCounter : MonoBehaviour {

    PlayerMovement playerValues;
    Text fossilValue;
    public GameObject player;

	void Start () {
        playerValues = player.GetComponent<PlayerMovement>();
        fossilValue = GetComponent<Text>();
	}

	void Update () {
        fossilValue.text = playerValues.numFossils.ToString();
	}
}
