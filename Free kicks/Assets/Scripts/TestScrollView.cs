using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScrollView : MonoBehaviour {

	public GameObject Button_Template;
	public GameObject Controller;
	int index;
	private List<string> NameList = new List<string>();

	void Start () {

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
	
	public void addContent(List<string> list, int testId){
		index = testId;
		int i = 1;
		foreach(string str in list)
		{
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			TestButton TB = go.GetComponent<TestButton>();
			TB.SetName(str);
			TB.SetIndex(index);
			TB.SetQNum(i);
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
		children.RemoveAt(0);
		children.ForEach(child => Destroy(child));
	}
	
	public void ButtonClicked(int id, string name, bool type, int q){ //type 0 = test, 1 = question
		Debug.Log("Name: "+name + " button clicked. "+type + " index: "+id + " q#:"+q);
		if(type)
			Controller.GetComponent<EditTestController>().fillQuestion(id, q, name);
		else
			Controller.GetComponent<EditTestController>().fillTest(id, name);
	}
	
	public void TestPicked(int id, string name){ 
		Debug.Log("Name: "+name + " button clicked. index: "+id );
		Controller.GetComponent<FreeKicksMenuController>().test = name;
		Controller.GetComponent<FreeKicksMenuController>().index = id;
	}
}
