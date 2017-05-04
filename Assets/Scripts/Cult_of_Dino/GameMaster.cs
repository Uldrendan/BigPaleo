using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;

    public float timeRemaining = 120;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }

	void Start () {
        
    }

	void Update () {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
            GameOver(false);
	}

    public void GameOver(bool win)
    {
        if (win)
            SceneManager.LoadScene(4);
        else
            SceneManager.LoadScene(3);
    }
    

}
