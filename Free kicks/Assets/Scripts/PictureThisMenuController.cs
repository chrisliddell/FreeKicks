using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PictureThisMenuController : MonoBehaviour
{
	public GameObject buttonPlay;
	public GameObject buttonHelp;
	public GameObject helpPanel;
	public GameObject blurPanel;
	public GameObject newWordListMenu;
	public GameObject editWordListMenu;
	public GameObject wordListPicker;
	public GameObject buttonNewWordList;
	public GameObject buttonEditWordList;
	public GameObject WordLists;
	public GameObject shuffleQuestions;
	public string wordList;
	public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void showHelp(){
		blurPanel.SetActive(true);
		helpPanel.SetActive(true);

	}
	
	public void hideHelp(){
		blurPanel.SetActive(false);
		helpPanel.SetActive(false);
		
	}	
	
	public void exit(){
		Application.Quit();
	}
	
	public void start(){
		Debug.Log("Starting game with wod list: "+wordList);
		SceneManager.LoadScene("PictureThis");	
	}
	
	public void newWordList(){
		Debug.Log("New word list");
		newWordListMenu.GetComponent<NewWordList>().show();
	}
	
	public void editWordList(){
		Debug.Log("Edit word list");
		editWordListMenu.GetComponent<EditTestController>().show();
	}
}
