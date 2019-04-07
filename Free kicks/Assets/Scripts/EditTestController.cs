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
	public GameObject newName;
	public GameObject buttonCancelChangeName;
	public GameObject buttonChangeName;
	public int editingTest;
	public int editingQuestion;
	public string answer;
	GameObject input;
	string[] tests;
	
    // Start is called before the first frame update
    void Start()
    {
        test = "";
		quest = "";
		answer = "A";
		editingTest = 0;
		editingQuestion = 0;
		blurPanel.SetActive(false);
		changeNamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dataValid())
			buttonAdd.GetComponent<Button>().interactable = true;
		else 
			buttonAdd.GetComponent<Button>().interactable = false;
		
		if(test == "")
			buttonFinish.GetComponent<Button>().interactable = false;
		else
			buttonFinish.GetComponent<Button>().interactable = true;
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
	}
	
	public bool dataValid(){
		if(testName.GetComponent<Text>().text != ""){
			if(question.GetComponent<Text>().text != ""){
				if(options[0].GetComponent<Text>().text != ""){
					if(options[1].GetComponent<Text>().text != ""){
						if(options[2].GetComponent<Text>().text != ""){
							if(options[3].GetComponent<Text>().text != ""){
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
		mainMenu.SetActive(false);
		updateTests();
	}
	
	
	public void hide(){
		editingQuestion = 0;
		editingTest = 0;
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
	}
	
	public void Cancel(){
		test = "";
		clearTextFields();
		hide();
	}
	
	public void clearTextFields(){
		input = GameObject.Find("TestName");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("Question");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionA");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionB");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionC");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionD");
		input.GetComponent<InputField>().text = "";
	}
	
	public void clearQuestion(){
		input = GameObject.Find("Question");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionA");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionB");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionC");
		input.GetComponent<InputField>().text = "";
		input = GameObject.Find("OptionD");
		input.GetComponent<InputField>().text = "";
	}
	
	
	public void saveQuestion(){
		clearQuestion();
		Debug.Log("New Test: "+editingTest+"\n" + test);
		PlayerPrefs.SetString(editingTest+"", test);
		test = "";
		editingQuestion = 0;
		editingTest = 0;
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
		cancelChangeName();
		updateTests();
	}
}
