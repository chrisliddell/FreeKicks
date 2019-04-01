using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScrollView : MonoBehaviour {

	public GameObject Button_Template;
	private List<string> NameList = new List<string>();
	private GameObject button;

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
	
	public void init(){
		button = Instantiate(Button_Template) as GameObject;
		clear();
	}
	
	public void addContent(List<string> list){
		foreach(string str in list)
		{
			GameObject go = Instantiate(button) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			go.transform.SetParent(GameObject.Find("ContentTests").transform);
		}
	}
	
	public void addContent(List<string> list, bool test){
		foreach(string str in list)
		{
			GameObject go = Instantiate(button) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			if(test)
				go.transform.SetParent(GameObject.Find("ContentTests").transform);
			else
				go.transform.SetParent(GameObject.Find("ContentQuestions").transform);
		}
	}	
	
	public void clear(){
		var children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}
	
	public void ButtonClicked(string str)
	{
		Debug.Log(str + " button clicked.");

	}
}
