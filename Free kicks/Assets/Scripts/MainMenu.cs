using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject infoPanel;
	public GameObject blurPanel;
	public GameObject resetPanel;
    // Start is called before the first frame update
    void Start()
    {
		hideReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void showInfo(){
		blurPanel.SetActive(true);
		infoPanel.SetActive(true);
	}
	
	public void hideInfo(){
		blurPanel.SetActive(false);
		infoPanel.SetActive(false);
	}
	
	public void exit(){
		PlayerPrefs.Save();
		Application.Quit();
	}
	
	public void startFreeKicks(){
		Debug.Log("Starting Free Kicks");
		SceneManager.LoadScene("FreeKicksMenu");		
	}
	
	public void startPictureThis(){
		Debug.Log("Starting Picture This");
		SceneManager.LoadScene("PictureThisMenu");		
	}

	public void showReset(){
		blurPanel.SetActive(true);
		resetPanel.SetActive(true);
	}
	
	public void hideReset(){
		blurPanel.SetActive(false);
		resetPanel.SetActive(false);
	}
	
	
	public void reset(){
		PlayerPrefs.DeleteAll();
		Debug.Log("Cleared all player preferences");
		PlayerPrefs.SetInt("index", 0);
		PlayerPrefs.SetInt("PT_index", 0);
	}

	
}
