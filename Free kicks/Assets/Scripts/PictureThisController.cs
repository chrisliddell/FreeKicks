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
	public GameObject colorPicker;
	public GameObject colorPickerPos;
	public GameObject slider;
	public GameObject sizeLabel;
	public string wordList;
	public int index;
	public int size = 0;
	string player1, player2;
	Color currentColor = Color.black;
	
    // Start is called before the first frame update
    void Start()
    {
       	index = PlayerPrefs.GetInt("PT_playingIndex", 0);
		player1 = PlayerPrefs.GetString("PT_player1", "Player 1");
		player2 = PlayerPrefs.GetString("PT_player2", "Player 2");
		colorPicker.GetComponent<ColorPicker>().startPos = new Vector2(colorPickerPos.transform.position.x, colorPickerPos.transform.position.y);
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
		
	public void pickColor(Color color){
		currentColor = color;
		Debug.Log("Selected new color:  "+currentColor.ToString());
	}
	
	public void pickedColor(string color){
		switch(color){
			case "Blue":
				currentColor = Color.blue;
				break;
			case "Red":
				currentColor = Color.red;
				break;
			case "Green":
				currentColor = Color.green;
				break;
			case "Yellow":
				currentColor = Color.yellow;
				break;
			case "Orange":
				currentColor.r = 255;
				currentColor.g = 145;
				currentColor.b = 0;
				currentColor.a = 255;
				break;
			case "Purple":
				currentColor.r = 195;
				currentColor.g = 0;
				currentColor.b = 255;
				currentColor.a = 255;
				break;
			case "Brown":
				currentColor.r = 135;
				currentColor.g = 62;
				currentColor.b = 0;
				currentColor.a = 255;
				break;
			case "White":
				currentColor = Color.white;
				break;
			default:
				currentColor = Color.black;
				break;
		}
		Debug.Log("Selected new color:  "+currentColor.ToString());
	}
	
	public void changeSize(){
		size = (int)slider.GetComponent<Slider>().value;
		sizeLabel.GetComponent<Text>().text = "Size: " + size;
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
