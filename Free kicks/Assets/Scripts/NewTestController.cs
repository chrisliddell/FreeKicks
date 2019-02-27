using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTestController : MonoBehaviour
{
	public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void show(){
		mainMenu.SetActive(false);
	}
	
	
	public void hide(){
		gameObject.SetActive(false);
		mainMenu.SetActive(true);
	}
	
	public void Cancel(){
		hide();
	}
	
}
