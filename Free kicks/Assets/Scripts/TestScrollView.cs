using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScrollView : MonoBehaviour {

	public GameObject Button_Template;
	public GameObject Controller;
	int index;
	private List<string> NameList = new List<string>();

	// Use this for initialization
	void Start () {

/*
		NameList.Add("Alan");
		NameList.Add("Amy");
		NameList.Add("Brian");
		NameList.Add("Carrie");
		NameList.Add("David");
		NameList.Add("Joe");
		NameList.Add("Jason");
		NameList.Add("Michelle");
		NameList.Add("Stephanie");
		NameList.Add("Zoe");

		foreach(string str in NameList)
		{
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			go.transform.SetParent(Button_Template.transform.parent);

		}*/
	}

	public void addContent(List<string> list){
		index = 1;
		foreach(string str in list)
		{
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			TB.SetIndex(index);
			go.transform.SetParent(Button_Template.transform.parent);
			index++;
		}
	}
	
	public void addContent(string s){
		GameObject go = Instantiate(Button_Template) as GameObject;
		go.SetActive(true);
		TestButton TB = go.GetComponent<TestButton>();
		TB.SetName(s);
		TB.SetIndex(index);
		go.transform.SetParent(Button_Template.transform.parent);
		index++;
	}
	
	
	public void clear(){
		var children = new List<GameObject>();
		foreach (Transform child in gameObject.transform.GetChild(0)) children.Add(child.gameObject);
		children.RemoveAt(0);
		children.ForEach(child => Destroy(child));
	}
	
	public void ButtonClicked(int index, string name, bool type){ //type 0 = test, 1 = question
		Debug.Log("Name: "+name + " button clicked. "+type + " index: "+index);
		if(type)
			Controller.GetComponent<EditTestController>().fillQuestion(index, name);
		else
			Controller.GetComponent<EditTestController>().fillTest(index, name);
	}
}
