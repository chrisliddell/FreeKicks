using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditWordList : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject wordListName;
	public GameObject word;
	public GameObject buttonAdd;
	public GameObject buttonEdit;
	public GameObject buttonFinish;
	public GameObject buttonDelete;
	public GameObject buttonRemove;
	public GameObject wordListsPanel;
	public GameObject wordsPanel;
	public GameObject blurPanel;
	public GameObject changeNamePanel;
	public GameObject cancelChangesPanel;
	public GameObject finishPanel;
	public GameObject newName;
	public GameObject buttonCancelChangeName;
	public GameObject buttonChangeName;
	public int editingWordList;
	public int editingWord;
	int index;
	string currentWord;
	string wordList;
	GameObject input;
	string[] wordLists;
	string[] prevWordLists;
	int prevIndex;
	bool madeChanges;
    // Start is called before the first frame update
    void Start()
    {
		index = 0;
		currentWord = "";
		wordList = "";
		madeChanges = false;
		blurPanel.SetActive(false);
		changeNamePanel.SetActive(false);
		cancelChangesPanel.SetActive(false);
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
		
		if(!madeChanges)
			buttonFinish.GetComponent<Button>().interactable = false;
		else
			buttonFinish.GetComponent<Button>().interactable = true;
		
		if(editingWord > 0){
			buttonEdit.GetComponent<Button>().interactable = true;
			buttonRemove.GetComponent<Button>().interactable = true;
		} else{ 
			buttonEdit.GetComponent<Button>().interactable = false;
			buttonRemove.GetComponent<Button>().interactable = false;
		}
		
		if(editingWordList > 0)
			buttonDelete.GetComponent<Button>().interactable = true;
		else
			buttonDelete.GetComponent<Button>().interactable = false;
	}
	
	public void updateWordLists(){
		List<string> list = new List<string>();
		wordList = "";
		wordListsPanel.GetComponent<TestScrollView>().clear();
		wordsPanel.GetComponent<TestScrollView>().clear();
		index = PlayerPrefs.GetInt("PT_index", 0);
		Debug.Log("editing wordsLissts, current index: "+index);
		wordLists = new string[index];
		for(int i = 1; i < index; i++){
			if(PlayerPrefs.GetString("PT_"+i, "").Split(':')[0] != "")
				list.Add(PlayerPrefs.GetString("PT_"+i, "").Split(':')[0].Substring(1)+"\n");
		}
		wordListsPanel.GetComponent<TestScrollView>().addContent(list);
		editingWord = 0;
		editingWordList = 0;
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
		madeChanges = false;
		prevIndex = PlayerPrefs.GetInt("PT_index", 0);
		prevWordLists = new string[prevIndex];
		for(int i = 1; i < prevIndex; i++){
			prevWordLists[i] = PlayerPrefs.GetString("PT_"+i, "");
			Debug.Log("index: "+i+"\t"+prevWordLists[i]);
		}
		updateWordLists();
	}
	
	
	public void hide(){
		editingWord = 0;
		editingWordList = 0;
		clearTextFields();
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
		mainMenu.GetComponent<PictureThisMenuController>().updateWordLists();
	}
	
	public void Cancel(){
		if(madeChanges){
			cancelChangesPanel.SetActive(true);
			blurPanel.SetActive(true);
		} else {
			wordList = "";
			hide();
		}
	}
		
	public void yesCancel(){
		wordList = "";
		PlayerPrefs.SetInt("PT_index", prevIndex);
		for(int i = 1; i < prevIndex; i++){
			PlayerPrefs.SetString("PT_"+i, prevWordLists[i]);
		}
		blurPanel.SetActive(false);
		cancelChangesPanel.SetActive(false);
		hide();
	}
	
	public void noCancel(){
		blurPanel.SetActive(false);
		cancelChangesPanel.SetActive(false);
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

	public void saveWord(){
		clearWord();
		Debug.Log("New WordList: "+editingWordList+"\n" + wordList);
		PlayerPrefs.SetString("PT_"+editingWordList, wordList);
		wordList = "";
		editingWord = 0;
		editingWordList = 0;
	}
		
	public void Finish(){
		if(madeChanges){
			finishPanel.SetActive(true);
			blurPanel.SetActive(true);
		} else {
			wordList = "";
			hide();
		}
	}
		
	public void yesFinish(){
		wordList = "";
		clearTextFields();
		blurPanel.SetActive(false);
		finishPanel.SetActive(false);
		hide();
	}
	
	public void noFinish(){
		blurPanel.SetActive(false);
		finishPanel.SetActive(false);
	}
		
	public void saveWordList(){
		wordList = "{"+ wordListName.GetComponent<Text>().text +":"+wordList+"\n}";
		clearTextFields();
		Debug.Log(wordList);
		PlayerPrefs.SetString("PT_"+editingWordList, wordList);
		wordList = "";
		editingWord = 0;
		editingWordList = 0;
	}
	
	public void fillWord(int id, int wNum, string w){
		if(id == 0) return;
		editingWordList = id;
		editingWord = wNum;
		word.transform.parent.gameObject.GetComponent<InputField>().text = w;
	}
	
	public void fillWordList(int id, string wL){
		if(id == 0) return;
		wordList =  PlayerPrefs.GetString("PT_"+id, "");
		editingWordList = id;
		clearWord();
		wordListName.transform.parent.gameObject.GetComponent<InputField>().text = wL;
		List<string> list = new List<string>();
		string[] words = PlayerPrefs.GetString("PT_"+id, "").Replace("\n", "").Replace("\t", "").Split('{');
		for(int i = 2; i < words.Length; i++)
		list.Add(words[i].Replace("}", ""));
		wordsPanel.GetComponent<TestScrollView>().clear();
		wordsPanel.GetComponent<TestScrollView>().addContent(list, id);
		editingWord = 0;
	}
	
	public void editWord(){
		if(editingWord == 0) return;
		string wLName = wordListName.transform.parent.gameObject.GetComponent<InputField>().text;
		Debug.Log("editing word #"+editingWord);
		currentWord = word.GetComponent<Text>().text;
		string[] words = wordList.Split('{');
		words[editingWord+1] = "\n\t\t"+currentWord+"\n\t}\n\t";
		wordList = "{" + wLName+":\n\t";
		for(int i = 2; i < words.Length; i++){
			wordList += "{"+words[i];
		}
		madeChanges = true;
		saveWord();
		updateWordLists();
	}
	
	public void removeWord(){
		if(editingWord == 0) return;
		Debug.Log("Removing word #"+editingWord);
		string wLName = wordListName.transform.parent.gameObject.GetComponent<InputField>().text;
		string[] words = wordList.Split('{');
		wordList = "{" + wLName+":\n\t";
		for(int i = 2; i < words.Length; i++){
			if((i-1) != editingWord)
				wordList += "{"+words[i];
		}
		madeChanges = true;
		saveWord();
		updateWordLists();
	}
		
	public void deleteWordList(){
		if(editingWordList == 0) return;
		Debug.Log("Deleting word list #"+editingWordList);
		List<string> list = new List<string>();
		index = PlayerPrefs.GetInt("PT_index", 0);
		PlayerPrefs.DeleteKey("PT_"+editingWordList);
		for(int i = editingWordList; i < index; i++){
			PlayerPrefs.SetString("PT_"+i, PlayerPrefs.GetString("PT_"+(i+1), ""));
		}
		madeChanges = true;
		index--;
		PlayerPrefs.SetInt("PT_index", index);
		updateWordLists();
	}
	
	public void addWord(){
		editingWord = wordsPanel.transform.GetChild(0).childCount;
		Debug.Log("Adding word #"+editingWord);
		wordsPanel.GetComponent<TestScrollView>().addContent(word.GetComponent<Text>().text, editingWordList, editingWord);
		currentWord = word.GetComponent<Text>().text;
		wordList = wordList.Substring(0, wordList.Length-1)+ "\t{\n\t\t" + currentWord +"\n\t}\n}";
		saveWord();
		madeChanges = true;
	}
	
	public void changeName(){
		string n =  wordListName.GetComponent<Text>().text;
		if(n == "") return;
		Debug.Log("Renaming word list #"+editingWordList);
		blurPanel.SetActive(true);
		changeNamePanel.SetActive(true);
		newName.transform.parent.gameObject.GetComponent<InputField>().text = n;
	}
	
	public void cancelChangeName(){
		blurPanel.SetActive(false);
		changeNamePanel.SetActive(false);
	}
		
	public void saveName(){
		wordListName.transform.parent.gameObject.GetComponent<InputField>().text = newName.GetComponent<Text>().text;
		int i = wordList.Split(':')[0].Length;
		wordList = "{" + newName.GetComponent<Text>().text + wordList.Substring(i);
		PlayerPrefs.SetString("PT_"+editingWordList, wordList);
		madeChanges = true;
		cancelChangeName();
		updateWordLists();
	}
}
