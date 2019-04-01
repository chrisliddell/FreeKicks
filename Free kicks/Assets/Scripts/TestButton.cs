using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestButton : MonoBehaviour {

	private string Name;
	public Text ButtonText;
	public TestScrollView ScrollView;

	public void SetName(string name)
	{
		Name = name;
		ButtonText.text = name;
	}
	public void Button_Click()
	{
		ScrollView.ButtonClicked(Name);

	}
	
	public void test(){
		Debug.Log("hola");
	}
}
