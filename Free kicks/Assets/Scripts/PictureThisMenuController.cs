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
	public GameObject Player1;
	public GameObject Player2;
	public string wordList;
	public int index;
	string player1, player2;
    // Start is called before the first frame update
    void Start()
	{
		updatePlayers();
        updateWordLists();
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
		index = PlayerPrefs.GetInt("PT_index", 0);
		options.Add("No word list");
		for(int i = 1; i < index; i++){
			if((s = PlayerPrefs.GetString("PT_"+i,"")) != "")
				options.Add(s.Split(':')[0].Substring(1));
		}
		d.AddOptions(options);
	}
	
	public void updatePlayers(){
		player1 = PlayerPrefs.GetString("PT_player1", "");
		player2 = PlayerPrefs.GetString("PT_player2", "");
		Player1.transform.parent.gameObject.GetComponent<InputField>().text = player1;
		Player2.transform.parent.gameObject.GetComponent<InputField>().text = player2;
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
		PlayerPrefs.Save();
		Application.Quit();
	}
	
	public void start(){
		player1 = Player1.GetComponent<Text>().text == "" ? "Player 1" : Player1.GetComponent<Text>().text;
		player2 = Player2.GetComponent<Text>().text == "" ? "Player 2" : Player2.GetComponent<Text>().text;
		index = wordListPicker.GetComponent<Dropdown>().value;
		Debug.Log("Starting game with word list: "+index);
		PlayerPrefs.SetInt("PT_playingIndex", index);
		PlayerPrefs.SetString("PT_player1", player1);
		PlayerPrefs.SetString("PT_player2", player2);
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
