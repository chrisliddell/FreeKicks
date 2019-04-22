using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWordList : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject wordListName;
	public GameObject word;
	public GameObject buttonAdd;
	public GameObject buttonFinish;
	public GameObject buttonCancel;
	public GameObject wordsPanel;
	int index;
	string currentWord;
	string wordList;
	GameObject input;
    // Start is called before the first frame update
    void Start()
    {
		index = PlayerPrefs.GetInt("PT_index", 0);
		if(index == 0) index++;
		Debug.Log("Adding test, current index: "+index);
	}
	
    // Update is called once per frame
    void Update()
    {
        if(dataValid())
			buttonAdd.GetComponent<Button>().interactable = true;
		else 
			buttonAdd.GetComponent<Button>().interactable = false;
		
		if(wordList == "")
			buttonFinish.GetComponent<Button>().interactable = false;
		else
			buttonFinish.GetComponent<Button>().interactable = true;
	}
	
	public bool dataValid(){
		if(wordListName.GetComponent<Text>().text != ""){
			if(word.GetComponent<Text>().text != ""){
				return true;
			}
		}
		return false;
	}
	
	public void show(){
		gameObject.SetActive(true);
		mainMenu.SetActive(false);
	}
	
	
	public void hide(){
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
		mainMenu.GetComponent<PictureThisMenuController>().updateWordLists();
	}
	
	public void Cancel(){
		currentWord = "";
		wordsPanel.GetComponent<Text>().text = wordList;
		clearTextFields();
		hide();
	}
	
	public void addWord(){
		currentWord = word.GetComponent<Text>().text;
		wordsPanel.GetComponent<Text>().text += currentWord + "\n";
		wordList += "\n\t{\n\t\t" + currentWord + "\n\t}";
		clearWord();
	}
	
	public void clearTextFields(){
		input = GameObject.Find("WordListName");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("Word");
		input.GetComponent<InputField>().text = "";
	}
	
	public void clearWord(){
		input = GameObject.Find("Word");
		input.GetComponent<InputField>().text = "";
	}
	
	
	public void save(){
		wordList = "{"+ wordListName.GetComponent<Text>().text +":"+wordList+"\n}";
		clearTextFields();
		Debug.Log("Saving new test:\n"+wordList);
		PlayerPrefs.SetString("PT_"+index, wordList);
		index++;
		Debug.Log("Setting new index: "+index);
		PlayerPrefs.SetInt("PT_index", index);
		wordList = "";
		Cancel();
	}
}
