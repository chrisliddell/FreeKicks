using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public GameObject buttonInfo;
	public GameObject buttonHelp;
	public GameObject infoPanel;
	public GameObject helpPanel;
	public GameObject blurPanel;
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

}
