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
	public int edittingTest;
	public int edittingQuestion;
	public string answer;
	GameObject input;
	string[] tests;
	
    // Start is called before the first frame update
    void Start()
    {
        test = "";
		quest = "";
		answer = "A";
		edittingTest = 0;
		edittingQuestion = 0;
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
		Debug.Log("Editting tests, current index: "+index);
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
		edittingQuestion = 0;
		edittingTest = 0;
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
		clearTextFields();
		Debug.Log("New Test:\n" + test);
		PlayerPrefs.SetString(edittingTest+"", test);
		test = "";
		edittingQuestion = 0;
		edittingTest = 0;
	}
	
	public void saveTest(){
		test = "{"+ testName.GetComponent<Text>().text +":"+test+"\n}";
		clearTextFields();
		Debug.Log(test);
		PlayerPrefs.SetString(edittingTest+"", test);
		test = "";
		edittingQuestion = 0;
		edittingTest = 0;
	}
	
	public void fillQuestion(int id, string q){
		if(id == 0) return;
		edittingQuestion = id;
		question.transform.parent.gameObject.GetComponent<InputField>().text = q;
		Debug.Log(":) \n" +PlayerPrefs.GetString(edittingTest+"", "").Replace("\n", "").Replace("\t", ""));
		changeAnswer(PlayerPrefs.GetString(edittingTest+"", "").Replace("\n", "").Replace("\t", "").Split('{')[id+1].Split('[')[1].Split(']')[1].Split(':')[1].Substring(0,1));
		string[] opts =  PlayerPrefs.GetString(edittingTest+"", "").Replace("\n", "").Replace("\t", "").Split('{')[id+1].Split('[')[1].Split(']')[0].Split(',');
		for(int i = 0; i < opts.Length; i++)
			options[i].transform.parent.gameObject.GetComponent<InputField>().text = opts[i];
	}
	
	public void fillTest(int id, string t){
		if(id == 0) return;
		test =  PlayerPrefs.GetString(id+"", "");
		edittingTest = id;
		clearQuestion();
		testName.transform.parent.gameObject.GetComponent<InputField>().text = t;
		List<string> list = new List<string>();
		string[] questions = PlayerPrefs.GetString(id+"", "").Replace("\n", "").Replace("\t", "").Split('{');
		for(int i = 2; i < questions.Length; i++)
			list.Add(questions[i].Split(':')[0]);
		questionsPanels.GetComponent<TestScrollView>().clear();
		questionsPanels.GetComponent<TestScrollView>().addContent(list);
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
		
		Debug.Log("Editting question #"+edittingQuestion);
	}
	
	public void Delete(){
		Debug.Log("Deleting question #"+edittingQuestion);
		test = "";
		PlayerPrefs.SetString(index+"", test);
		questionsPanels.GetComponent<Text>().text = test;
		clearTextFields();
		hide();
	}
	
	public void addQuestion(){
		edittingQuestion = questionsPanels.transform.GetChild(0).childCount;
		Debug.Log("Adding question #"+edittingQuestion);
		questionsPanels.GetComponent<TestScrollView>().addContent(question.GetComponent<Text>().text);
		quest = question.GetComponent<Text>().text + ": [" + options[0].GetComponent<Text>().text + ", " + options[1].GetComponent<Text>().text + ", " + options[2].GetComponent<Text>().text + ", " + options[3].GetComponent<Text>().text+"]";
		test = test.Substring(0, test.Length-1)+ "\t{\n\t\t" + quest + "\n\t\tans:\t"+answer+"\n\t}\n}";
		saveQuestion();
	}
}
