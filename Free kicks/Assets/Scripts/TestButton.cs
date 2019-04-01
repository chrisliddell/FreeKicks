using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestButton : MonoBehaviour {

	private string Name;
	public int index;
	public bool Type;
	public Text ButtonText;
	public TestScrollView ScrollView;

	public void SetName(string name)
	{
		Name = name;
		ButtonText.text = name;
	}
	
	public void SetIndex(int i)
	{
		index = i;
	}
	
	public void Button_Click()
	{
		ScrollView.ButtonClicked(index, Name, Type);

	}
}
