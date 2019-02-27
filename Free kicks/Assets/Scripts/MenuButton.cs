using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	public GameObject newTest;
	public GameObject editTest;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void startSinglePlayer(){
		Debug.Log("Starting singleplayer");
		SceneManager.LoadScene("Singleplayer");
	}
	
	public void startMultiPlayer(){
		Debug.Log("Starting multiplayer");
		SceneManager.LoadScene("Multiplayer");
	}
	
	public void newWordList(){
		Debug.Log("New word list");
		newTest.SetActive(true);
		newTest.GetComponent<NewTestController>().show();
	}
	
	public void editWordList(){
		Debug.Log("Edit word list");
		editTest.SetActive(true);
		editTest.GetComponent<EditTestController>().show();
	}
}
