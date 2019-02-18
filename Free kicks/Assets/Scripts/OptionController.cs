using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    public int option;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        if (option != null)
        {
            GameObject tc = GameObject.Find("Test");
            tc.GetComponent<TestController>().Answer(option);
        }
    }
}
