using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public float time = 10f;
    public int answer = 0;
    public float isCorrect = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 promptAnswer()
    {
        gameObject.SetActive(true);

        while (answer != -1)
        {

        }
        return new Vector2(isCorrect, time);

    }

    public void Answer()
    {
        answer = 0;
    }
}
