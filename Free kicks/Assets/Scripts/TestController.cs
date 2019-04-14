using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
	public GameObject[] buttons;
	public GameObject qLabel;
    public float time = 10f;
    string correctAnswer;
	string question;
	string[] options;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetData(string q, string[] o, string a){
		question = q;
		options = o;
		correctAnswer = a;
		for(int i = 0; i < 4; i++)
			buttons[i].GetComponentInChildren<Text>().text = options[i];
		qLabel.GetComponentInChildren<Text>().text = q;
	}
	
    public void Answer(string option)
    {
        GameObject controller = GameObject.Find("Controller");
        GameController gc = controller.GetComponent<GameController>();
        gc.checkAnswer(correctAnswer==option);
    }
	
	public void AnswerSingleplayer(string option)
    {
        GameObject controller = GameObject.Find("Controller");
        SingleplayerController gc = controller.GetComponent<SingleplayerController>();
        gc.checkAnswer(correctAnswer==option);
    }
}
