﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestScrollView : MonoBehaviour {

	public GameObject Button_Template;
	public GameObject Controller;
	int index;
	private List<string> NameList = new List<string>();

	void Start () {

	}

	public void addContent(List<string> list){
		index = 1;
		int i = 0;
		foreach(string str in list)
		{
			GameObject go;
			if(i == 0)
				go = Button_Template;
			else 
				go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			TB.SetIndex(index);
			go.transform.SetParent(Button_Template.transform.parent);
			go.transform.localScale = new Vector3(1,1,1);
			index++;
			i++;
		}
	}
		
	public void addWords(List<string> list){
		for(int i = 0; i < list.Count; i++)
		{
			GameObject go;
			if(i == 0)
				go = Button_Template;
			else 
				go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(list[i]);
			TB.SetIndex(index);
			go.transform.SetParent(Button_Template.transform.parent);
			go.transform.localScale = new Vector3(1,1,1);
			go.transform.localPosition = new Vector3(0,0,0);
		}
	}
	
	public void addContent(List<string> list, int testId){
		index = testId;
		int i = 0;
		foreach(string str in list)
		{
			GameObject go;
			if(i == 0)
				go = Button_Template;
			else 
				go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			TB.SetIndex(index);
			TB.SetQNum(i+1);
			go.transform.SetParent(Button_Template.transform.parent);
			i++;
		}
		index++;
	}
	
	public void addContent(string s, int testId, int q){
		GameObject go = Instantiate(Button_Template) as GameObject;
		go.SetActive(true);
		TestButton TB = go.GetComponent<TestButton>();
		TB.SetName(s);
		TB.SetIndex(testId);
		TB.SetQNum(q);
		go.transform.SetParent(Button_Template.transform.parent);
		index = testId+1;
	}
	
	
	public void clear(){
		var children = new List<GameObject>();
		foreach (Transform child in gameObject.transform.GetChild(0)) children.Add(child.gameObject);
		children[0].GetComponent<TestButton>().SetName("");
		children.RemoveAt(0);
		children.ForEach(child => Destroy(child));
	}
	
	public void ButtonClicked(int id, string name, bool type, int q){ //type 0 = test, 1 = question
		if(type)
			Controller.GetComponent<EditTestController>().fillQuestion(id, q, name);
		else
			Controller.GetComponent<EditTestController>().fillTest(id, name);
	}
	
	public void TestPicked(int id, string name){ 
		Controller.GetComponent<FreeKicksMenuController>().test = name;
		Controller.GetComponent<FreeKicksMenuController>().index = id;
	}
	
	public void PT_ButtonClicked(int id, string name, bool type, int w){ //type 0 = wordlist, 1 = word
		if(type)
			Controller.GetComponent<EditWordList>().fillWord(id, w, name);
		else
			Controller.GetComponent<EditWordList>().fillWordList(id, name);
	}
	
	public void WordPicked(string name){ 
		Controller.GetComponent<WordListPicker>().pickWord(name);
	}
	
}
