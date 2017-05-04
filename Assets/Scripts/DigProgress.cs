using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigProgress : MonoBehaviour {

    PlayerMovement player;
	void Start () {
        player = transform.parent.parent.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().localScale = new Vector2(player.digPercentage, 1);
	}
}
