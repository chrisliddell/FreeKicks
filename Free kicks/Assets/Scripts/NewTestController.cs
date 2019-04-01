﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTestController : MonoBehaviour
{
	public GameObject mainMenu;
	public string test, quest;
	public int index;
	public GameObject[] options;
	public GameObject testName;
	public GameObject question;
	public GameObject buttonAdd;
	public GameObject buttonFinish;
	public GameObject questionsPanels;
	GameObject input;
    // Start is called before the first frame update
    void Start()
    {
        test = "";
		quest = "";
		index = PlayerPrefs.GetInt("index", 0)+1;
		Debug.Log("Adding test, current index: "+index);
	}
	
	public void reset(){
		PlayerPrefs.DeleteAll();
		Debug.Log("Cleared all player preferences");
		PlayerPrefs.SetInt("index", 0);
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
	}
	
	
	public void hide(){
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
	}
	
	public void Cancel(){
		test = "";
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
		index++;
		Debug.Log("Setting new index: "+index);
		PlayerPrefs.SetInt("index", index);
		test = "";
		Cancel();
	}
}
