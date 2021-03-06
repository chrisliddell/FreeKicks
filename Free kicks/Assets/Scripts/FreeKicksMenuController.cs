﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FreeKicksMenuController : MonoBehaviour
{
	public GameObject buttonPlay;
	public GameObject buttonInfo;
	public GameObject buttonHelp;
	public GameObject infoPanel;
	public GameObject helpPanel;
	public GameObject warningPanel;
	public GameObject blurPanel;
	public GameObject newTestMenu;
	public GameObject editTestMenu;
	public GameObject pickTestPanel;
	public GameObject buttonNewTest;
	public GameObject buttonEditTest;
	public GameObject testsList;
	public GameObject shuffleQuestions;
	public Dropdown team1Flag;
	public Dropdown team2Flag;
	public string test;
	public int index;
	bool singleplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void showHelp(){
		blurPanel.SetActive(true);
		helpPanel.SetActive(true);

	}
	
	public void hideHelp(){
		blurPanel.SetActive(false);
		helpPanel.SetActive(false);
		
	}
	
	public void showInfo(){
		blurPanel.SetActive(true);
		infoPanel.SetActive(true);
	}
	
	public void hideInfo(){
		blurPanel.SetActive(false);
		infoPanel.SetActive(false);
	}
	
	public void showPickTest(bool s){
		if(team1Flag.value == team2Flag.value){
			showWarning();
			return;
		}
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
		
	public void hidePickTest(){
		blurPanel.SetActive(false);
		pickTestPanel.SetActive(false);
	}
		
	public void pickTest(){
		pickTestPanel.SetActive(false);
		if(singleplayer) 
			startSinglePlayer();
		else 
			startMultiPlayer();
	}
	
	public void exit(){
		PlayerPrefs.Save();
		SceneManager.LoadScene("MainMenu");	
	}
	
	public void startSinglePlayer(){
		Debug.Log("Starting singleplayer with test: "+test+ " index: "+index);
		PlayerPrefs.SetInt("playingIndex", index);
		PlayerPrefs.SetString("team1Country", team1Flag.options[team1Flag.value].text);
		PlayerPrefs.SetString("team2Country", team2Flag.options[team2Flag.value].text);
		PlayerPrefs.SetInt("shuffleTest", shuffleQuestions.GetComponent<Toggle>().isOn?1:0);
		SceneManager.LoadScene("Singleplayer");		
	}
	
	public void startMultiPlayer(){
		Debug.Log("Starting multiplayer with test: "+test+ " index: "+index);
		PlayerPrefs.SetInt("playingIndex", index);
		PlayerPrefs.SetInt("shuffleTest", shuffleQuestions.GetComponent<Toggle>().isOn?1:0);
		PlayerPrefs.SetString("team1Country", team1Flag.options[team1Flag.value].text);
		PlayerPrefs.SetString("team2Country", team2Flag.options[team2Flag.value].text);
		SceneManager.LoadScene("Multiplayer");
	}
	
	public void newTest(){
		Debug.Log("New word list");
		newTestMenu.GetComponent<NewTestController>().show();
	}
	
	public void editTest(){
		Debug.Log("Edit word list");
		editTestMenu.GetComponent<EditTestController>().show();
	}
	
	public void changeFlag(int team){
		if(team == 1)
			team1Flag.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = team1Flag.options[team1Flag.value].image;
		else
			team2Flag.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = team2Flag.options[team2Flag.value].image;
	}
	
	public void showWarning(){
		warningPanel.SetActive(true);
		blurPanel.SetActive(true);
	}
	
	public void hideWarning(){
		warningPanel.SetActive(false);
		blurPanel.SetActive(false);
	}
}
