using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTestController : MonoBehaviour
{
	public GameObject mainMenu;
	public string test, quest;
	public int index;
	public GameObject[] options;
	public GameObject[] toggles;
	public GameObject testName;
	public GameObject question;
	public GameObject buttonAdd;
	public GameObject buttonEdit;
	public GameObject buttonFinish;
	public GameObject buttonDelete;
	public GameObject buttonRemove;
	public GameObject testsPanels;
	public GameObject questionsPanels;
	public GameObject blurPanel;
	public GameObject changeNamePanel;
	public GameObject cancelChangesPanel;
	public GameObject finishPanel;
	public GameObject newName;
	public GameObject buttonCancelChangeName;
	public GameObject buttonChangeName;
	public int editingTest;
	public int editingQuestion;
	public string answer;
	GameObject input;
	string[] tests;
	string[] prevTests;
	int prevIndex;
	bool madeChanges;
	
    // Start is called before the first frame update
    void Start()
    {
        test = "";
		quest = "";
		answer = "A";
		editingTest = 0;
		editingQuestion = 0;
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
		
		if(!madeChanges)
			buttonFinish.GetComponent<Button>().interactable = false;
		else
			buttonFinish.GetComponent<Button>().interactable = true;
		
		if(editingQuestion > 0){
			buttonEdit.GetComponent<Button>().interactable = true;
			buttonRemove.GetComponent<Button>().interactable = true;
		} else{ 
			buttonEdit.GetComponent<Button>().interactable = false;
			buttonRemove.GetComponent<Button>().interactable = false;
		}
		
		if(editingTest > 0)
			buttonDelete.GetComponent<Button>().interactable = true;
		else
			buttonDelete.GetComponent<Button>().interactable = false;
	}
	
	public void updateTests(){
		List<string> list = new List<string>();
		test = "";
		testsPanels.GetComponent<TestScrollView>().clear();
		questionsPanels.GetComponent<TestScrollView>().clear();
		index = PlayerPrefs.GetInt("index", 0);
		Debug.Log("editing tests, current index: "+index);
		tests = new string[index];
		for(int i = 1; i < index; i++){
			if(PlayerPrefs.GetString(i+"", "").Split(':')[0] != "")
				list.Add(PlayerPrefs.GetString(i+"", "").Split(':')[0].Substring(1)+"\n");
		}
		testsPanels.GetComponent<TestScrollView>().addContent(list);
		editingQuestion = 0;
		editingTest = 0;
	}
	
	public bool dataValid(){
		if(testName.GetComponent<Text>().text != ""){
			if(question.GetComponent<Text>().text != ""){
				if(options[0].GetComponent<Text>().text != ""){
					if(options[1].GetComponent<Text>().text != ""){
						if(options[2].GetComponent<Text>().text != ""){
							if(options[3].GetComponent<Text>().text != ""){
								if(answer != "")
									return true;
							}
						}
					}
				}
			}
		}
		return false;
	}
	
	public void show(){
		gameObject.SetActive(true);
		mainMenu.SetActive(false);
		madeChanges = false;
		prevIndex = PlayerPrefs.GetInt("index", 0);
		prevTests = new string[prevIndex];
		for(int i = 1; i < prevIndex; i++){
			prevTests[i] = PlayerPrefs.GetString(i+"", "");
			Debug.Log("index: "+i+"\t"+prevTests[i]);
		}
		updateTests();
	}
	
	
	public void hide(){
		editingQuestion = 0;
		editingTest = 0;
		clearAnswer();
		clearTextFields();
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
	}
	
	public void Cancel(){
		if(madeChanges){
			cancelChangesPanel.SetActive(true);
			blurPanel.SetActive(true);
		} else {
			test = "";
			hide();
		}
	}
	
	public void yesCancel(){
		test = "";
		PlayerPrefs.SetInt("index", prevIndex);
		for(int i = 1; i < prevIndex; i++){
			PlayerPrefs.SetString(i+"", prevTests[i]);
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
		testName.transform.parent.gameObject.GetComponent<InputField>().text = "";
		question.transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[0].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[1].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[2].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[3].transform.parent.gameObject.GetComponent<InputField>().text = "";
	}
	
	public void clearQuestion(){
		question.transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[0].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[1].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[2].transform.parent.gameObject.GetComponent<InputField>().text = "";
		options[3].transform.parent.gameObject.GetComponent<InputField>().text = "";
	}
	
	
	public void saveQuestion(){
		clearQuestion();
		Debug.Log("New Test: "+editingTest+"\n" + test);
		PlayerPrefs.SetString(editingTest+"", test);
		test = "";
		editingQuestion = 0;
		editingTest = 0;
	}
	
	public void Finish(){
		if(madeChanges){
			finishPanel.SetActive(true);
			blurPanel.SetActive(true);
		} else {
			test = "";
			hide();
		}
		
	}
	
	public void yesFinish(){
		test = "";
		clearTextFields();
		blurPanel.SetActive(false);
		finishPanel.SetActive(false);
		hide();
	}
	
	public void noFinish(){
		blurPanel.SetActive(false);
		finishPanel.SetActive(false);
	}
	
	
	public void saveTest(){
		test = "{"+ testName.GetComponent<Text>().text +":"+test+"\n}";
		clearTextFields();
		Debug.Log(test);
		PlayerPrefs.SetString(editingTest+"", test);
		test = "";
		editingQuestion = 0;
		editingTest = 0;
	}
	
	public void fillQuestion(int id, int qNum, string q){
		if(id == 0) return;
		editingTest = id;
		editingQuestion = qNum;
		question.transform.parent.gameObject.GetComponent<InputField>().text = q;
		changeAnswer(PlayerPrefs.GetString(editingTest+"", "").Replace("\n", "").Replace("\t", "").Split('{')[qNum+1].Split(']')[1].Split(':')[1].Substring(0,1));
		string[] opts =  PlayerPrefs.GetString(editingTest+"", "").Replace("\n", "").Replace("\t", "").Split('{')[qNum+1].Split('[')[1].Split(']')[0].Split(',');
		for(int i = 0; i < opts.Length; i++)
			options[i].transform.parent.gameObject.GetComponent<InputField>().text = opts[i];
	}
	
	public void fillTest(int id, string t){
		if(id == 0) return;
		test =  PlayerPrefs.GetString(id+"", "");
		editingTest = id;
		clearQuestion();
		testName.transform.parent.gameObject.GetComponent<InputField>().text = t;
		List<string> list = new List<string>();
		string[] questions = PlayerPrefs.GetString(id+"", "").Replace("\n", "").Replace("\t", "").Split('{');
		for(int i = 2; i < questions.Length; i++)
			list.Add(questions[i].Split(':')[0]);
		questionsPanels.GetComponent<TestScrollView>().clear();
		questionsPanels.GetComponent<TestScrollView>().addContent(list, id);
		editingQuestion = 0;
	}

	
	public void clearAnswer(){	
		answer = "";
		toggles[0].GetComponent<Toggle>().isOn = false;
		toggles[1].GetComponent<Toggle>().isOn = false;
		toggles[2].GetComponent<Toggle>().isOn = false;
		toggles[3].GetComponent<Toggle>().isOn = false;
	}

	public void changeAnswer(string a){
		answer = a;
		if(a == "A"){
			toggles[0].GetComponent<Toggle>().isOn = true;
			toggles[1].GetComponent<Toggle>().isOn = false;
			toggles[2].GetComponent<Toggle>().isOn = false;
			toggles[3].GetComponent<Toggle>().isOn = false;
		} else if(a == "B"){
			toggles[0].GetComponent<Toggle>().isOn = false;
			toggles[1].GetComponent<Toggle>().isOn = true;
			toggles[2].GetComponent<Toggle>().isOn = false;
			toggles[3].GetComponent<Toggle>().isOn = false;
		} else if(a == "C"){
			toggles[0].GetComponent<Toggle>().isOn = false;
			toggles[1].GetComponent<Toggle>().isOn = false;
			toggles[2].GetComponent<Toggle>().isOn = true;
			toggles[3].GetComponent<Toggle>().isOn = false;
		} else {
			toggles[0].GetComponent<Toggle>().isOn = false;
			toggles[1].GetComponent<Toggle>().isOn = false;
			toggles[2].GetComponent<Toggle>().isOn = false;
			toggles[3].GetComponent<Toggle>().isOn = true;
		}
	}
	
	public void editQuestion(){
		if(editingQuestion == 0) return;
		string tName = testName.transform.parent.gameObject.GetComponent<InputField>().text;
		Debug.Log("editing question #"+editingQuestion);
		quest = question.GetComponent<Text>().text + ": [" + options[0].GetComponent<Text>().text + ", " + options[1].GetComponent<Text>().text + ", " + options[2].GetComponent<Text>().text + ", " + options[3].GetComponent<Text>().text+"]";
		string[] quests = test.Split('{');
		quests[editingQuestion+1] = "\n\t\t"+quest+"\n\t\tans:\t"+answer+"\n\t}\n\t";
		test = "{" + tName+":\n\t";
		for(int i = 2; i < quests.Length; i++){
			test += "{"+quests[i];
		}
		madeChanges = true;
		saveQuestion();
		updateTests();
	}
	
	public void removeQuestion(){
		if(editingQuestion == 0) return;
		Debug.Log("Removing question #"+editingQuestion);
		string tName = testName.transform.parent.gameObject.GetComponent<InputField>().text;
		string[] questions = test.Split('{');
		test = "{" + tName+":\n\t";
		for(int i = 2; i < questions.Length; i++){
			if((i-1) != editingQuestion)
				test += "{"+questions[i];
		}
		madeChanges = true;
		saveQuestion();
		updateTests();
	}
	
	public void deleteTest(){
		if(editingTest == 0) return;
		Debug.Log("Deleting test #"+editingTest);
		List<string> list = new List<string>();
		index = PlayerPrefs.GetInt("index", 0);
		for(int i = 1; i < index; i++){
			if(i != editingTest)
				list.Add(PlayerPrefs.GetString(i+"", ""));
		}
		PlayerPrefs.DeleteAll();
		index = 1;
		foreach(string s in list){
			PlayerPrefs.SetString(index+"", s);
			index++;
		}
		madeChanges = true;
		PlayerPrefs.SetInt("index", index);
		updateTests();
	}
	
	public void addQuestion(){
		editingQuestion = questionsPanels.transform.GetChild(0).childCount;
		Debug.Log("Adding question #"+editingQuestion);
		questionsPanels.GetComponent<TestScrollView>().addContent(question.GetComponent<Text>().text, editingTest, editingQuestion);
		quest = question.GetComponent<Text>().text + ": [" + options[0].GetComponent<Text>().text + ", " + options[1].GetComponent<Text>().text + ", " + options[2].GetComponent<Text>().text + ", " + options[3].GetComponent<Text>().text+"]";
		test = test.Substring(0, test.Length-1)+ "\t{\n\t\t" + quest + "\n\t\tans:\t"+answer+"\n\t}\n}";
		saveQuestion();
		madeChanges = true;
	}
	
	public void changeName(){
		string n =  testName.GetComponent<Text>().text;
		if(n == "") return;
		Debug.Log("Renaming test #"+editingTest);
		blurPanel.SetActive(true);
		changeNamePanel.SetActive(true);
		newName.transform.parent.gameObject.GetComponent<InputField>().text = n;
	}
	
	public void cancelChangeName(){
		blurPanel.SetActive(false);
		changeNamePanel.SetActive(false);
	}
	
	public void saveName(){
		testName.transform.parent.gameObject.GetComponent<InputField>().text = newName.GetComponent<Text>().text;
		int i = test.Split(':')[0].Length;
		test = "{" + newName.GetComponent<Text>().text + test.Substring(i);
		PlayerPrefs.SetString(editingTest+"", test);
		madeChanges = true;
		cancelChangeName();
		updateTests();
	}
}
