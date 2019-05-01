using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class WordListPicker : MonoBehaviour
{
	public GameObject pickWordLabel;
	public GameObject wordsPanel;
	public GameObject circleBrush;

	// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

	}
	
	public void updateContent(string p, List<string> words){
		pickWordLabel.GetComponent<Text>().text = p+" pick a word to draw!";
		wordsPanel.GetComponent<TestScrollView>().addContent(words);
	}
	/*
	public void showPickTest(bool s){
		singleplayer = s;
		blurPanel.SetActive(true);
		pickTestPanel.SetActive(true);
		List<string> list = new List<string>();
		test = "";
		testsList.GetComponent<TestScrollView>().clear();
		index = PlayerPrefs.GetInt("index", 0);
		for(int i = 1; i < index; i++){
			if(PlayerPrefs.GetString(i+"", "").Split(':')[0] != "")
				list.Add(PlayerPrefs.GetString(i+"", "").Split(':')[0].Substring(1)+"\n");
		}
		testsList.GetComponent<TestScrollView>().addContent(list);
	}
	*/
}
