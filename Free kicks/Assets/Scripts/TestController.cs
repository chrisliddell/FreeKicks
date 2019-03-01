using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public float time = 10f;
    public int answer = -1;
    public int correctAnswer = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void promptAnswer(int ans)
    {
        correctAnswer = ans;
        time = 0;
        answer = -1;
    }

    public void Answer(int option)
    {
        answer = option;
        GameObject controller = GameObject.Find("Controller");
        GameController gc = controller.GetComponent<GameController>();
        gc.checkAnswer(answer);
    }
	
	public void AnswerSinglePlayer(int option)
    {
        answer = option;
        GameObject controller = GameObject.Find("Controller");
        SingleplayerController gc = controller.GetComponent<SingleplayerController>();
        gc.checkAnswer(answer);
    }
}
