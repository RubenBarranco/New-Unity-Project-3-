using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScenario : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
