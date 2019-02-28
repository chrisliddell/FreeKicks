using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SingleplayerController : MonoBehaviour
{
    GameObject ball;
    GameObject test;
    GameObject goal;
    public GameObject striker;
    public GameObject goalkeeper;
    public GameObject barrier;
    public GameObject arrowCurved;
    public GameObject arrowStraight;
    Quaternion arrowRot;
    public Slider powerSlider;
    System.Random rand;
    private IEnumerator coroutine;
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
    public float power = 300f;
    public float maxPower = 1000f;
    public float timePressed = 0f;
    public int currentAnswer;

    // Start is called before the first frame update
    void Start()
    {
        arrowRot = arrowStraight.transform.rotation;
        powerSlider.gameObject.SetActive(false);
        GameObject camera = GameObject.Find("Main Camera");
        goal = GameObject.Find("goal2");
        ball = GameObject.Find("Ball");
        cameraPos = camera.transform.position;
        cameraRot = camera.transform.rotation;
        cameraFOV = Camera.main.fieldOfView;
        rand = new System.Random();
        test = GameObject.Find("Test");
        test.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    { 
        float r = rand.Next(0, 2);
        Debug.Log("Starting game");
        striker.GetComponent<StrikerController>().moveStriker();

        barrier.GetComponent<BarrierController>().setSize(4);
        barrier.GetComponent<BarrierController>().moveBarrier();

        ball.GetComponent<BallController>().resetPos();
        striker.GetComponent<StrikerController>().getBall(ball);
    }

    public void reset()
    {

    }

    public void shootMode(GameObject player)
    {
        Debug.Log("Player received ball.");
        GameObject camera = GameObject.Find("Main Camera");
        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 30f;
        camera.transform.position = ball.transform.position + new Vector3(-6, 2, 0);
        camera.transform.LookAt(goal.transform);
        //striker.gameObject.SetActive(false);
       // showQuestion();
    }



}
