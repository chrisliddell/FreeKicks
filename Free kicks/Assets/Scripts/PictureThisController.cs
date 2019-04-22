using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PictureThisController : MonoBehaviour
{
	public GameObject helpPanel;
	public GameObject blurPanel;
	public GameObject newWordListMenu;
	public GameObject editWordListMenu;
	public GameObject wordListPicker;
	public GameObject buttonNewWordList;
	public GameObject buttonEditWordList;
	public GameObject WordLists;
	public string wordList;
	public int index;
	string player1, player2;
	string currentColor = "Black";
	
    // Start is called before the first frame update
    void Start()
    {
       	index = PlayerPrefs.GetInt("PT_playingIndex", 0);
		player1 = PlayerPrefs.GetString("PT_player1", "Player 1");
		player2 = PlayerPrefs.GetString("PT_player2", "Player 2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void updateWordLists(){
		Dropdown d = wordListPicker.GetComponent<Dropdown>();
		d.ClearOptions();
		List<string> options = new List<string>();
		string s = "";
		for(int i = 1, index = PlayerPrefs.GetInt("PT_index", 0); i < index; i++){
			if((s = PlayerPrefs.GetString("PT_"+i,"")) != "")
				options.Add(s.Split(':')[0].Substring(1));
		}
		d.AddOptions(options);
	}
	
	public void showHelp(){
		blurPanel.SetActive(true);
		helpPanel.SetActive(true);

	}
	
	public void pickedColor(string c){
		currentColor = c;
	}
	
	public void exitGame(){
		SceneManager.LoadScene("PictureThisMenu");	
	}	
	
	public void exit(){
		PlayerPrefs.Save();
		Application.Quit();
	}
	
	public void start(){
		Debug.Log("Starting game with word list: "+wordList);
		SceneManager.LoadScene("PictureThis");	
	}
	
	public void newWordList(){
		Debug.Log("New word list");
		newWordListMenu.GetComponent<NewWordList>().show();
	}
	
	public void editWordList(){
		Debug.Log("Edit word list");
		editWordListMenu.GetComponent<EditWordList>().show();
	}
}
