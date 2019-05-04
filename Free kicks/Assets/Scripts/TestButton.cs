using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestButton : MonoBehaviour {

	public string Name;
	public int index;
	public int qNum;
	public bool Type;
	public Text ButtonText;
	public TestScrollView ScrollView;
	public Text pickTestText;

	public void SetName(string name)
	{
		Name = name;
		ButtonText.text = name;
	}
	
	public void SetIndex(int i)
	{
		index = i;
	}
	
	public void SetQNum(int i)
	{
		qNum = i;
	}
	
	
	public void Button_Click()
	{
		if(Name != "") 
			ScrollView.ButtonClicked(index, Name, Type, qNum);
	}
	
	public void Test_Pick()
	{
		ScrollView.TestPicked(index, Name);
		pickTestText.text = "Play: "+Name;
	}

	public void Word_Pick(){
		ScrollView.WordPicked(Name);
	}
	
	public void PT_Button_Click()
	{
		if(Name != "") 
			ScrollView.PT_ButtonClicked(index, Name, Type, qNum);
	}
	
}
