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
	public GameObject testName;
	public GameObject question;
	public GameObject buttonAdd;
	public GameObject buttonFinish;
	public GameObject buttonDelete;
	public GameObject buttonRemove;
	public GameObject testsPanels;
	public GameObject questionsPanels;
	GameObject input;
	string[] tests;
	
    // Start is called before the first frame update
    void Start()
    {
        test = "";
		quest = "";
		updateTests();
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
			Debug.Log(i+": "+PlayerPrefs.GetString(i+"", ""));
			if(PlayerPrefs.GetString(i+"", "").Split(':')[0] != ""){
				list.Add(PlayerPrefs.GetString(i+"", "").Split(':')[0].Substring(1)+"\n");
				/*//Button button = new Button()
				testsPanels.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(i+"", "").Split(':')[0].Substring(1)+"\n";
				//.onClick.AddListener(() => { MyFunction(); MyOtherFunction(); });
				//testsPanels.GetComponent<Text>().text += PlayerPrefs.GetString(i+"", "").Split(':')[0].Substring(1)+"\n";*/
			}
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
		testsPanels.GetComponent<TestScrollView>().init();
		questionsPanels.GetComponent<TestScrollView>().init();
		updateTests();
	}
	
	
	public void hide(){
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
	}
	
	public void Cancel(){
		test = "";
		clearTextFields();
		hide();
	}
		
	public void Delete(){
		test = "";
		PlayerPrefs.SetString(index+"", test);
		questionsPanels.GetComponent<Text>().text = test;
		clearTextFields();
		hide();
	}
	
	public void addQuestion(){
		quest = question.GetComponent<Text>().text + ": [" + options[0].GetComponent<Text>().text + ", " + options[1].GetComponent<Text>().text + ", " + options[2].GetComponent<Text>().text + ", " + options[3].GetComponent<Text>().text+"]";
		questionsPanels.GetComponent<Text>().text += quest + "\n";
		test += "\n\t{\n\t\t" + quest + "\n\t}";
		clearQuestion();
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
	
	
	public void save(){
		test = "{"+ testName.GetComponent<Text>().text +":"+test+"\n}";
		clearTextFields();
		Debug.Log(test);
		PlayerPrefs.SetString(index+"", test);
		test = "";
	}
}
