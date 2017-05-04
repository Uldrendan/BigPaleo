using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    Image startButton;
    Image introText;
    GameObject tutorialTextHolder;
	
	void Start () {
        startButton = transform.GetChild(1).GetComponent<Image>();
        introText = transform.GetChild(0).GetComponent<Image>();
        tutorialTextHolder = transform.GetChild(2).gameObject;
	}

    float counter;
	void Update () {
        counter += Time.deltaTime;
        if(counter >= 2)
        {
            startButton.color = new Color(255, 255, 255, startButton.color.a + Time.deltaTime);
            introText.color = new Color(255, 255, 255, introText.color.a + Time.deltaTime);
            for (int i = 0; i < tutorialTextHolder.transform.childCount; i++)
            {
                tutorialTextHolder.transform.GetChild(i).GetComponent<Text>().color =
                    new Color(255, 255, 255, introText.color.a + Time.deltaTime);
            }
        }
	}
}
