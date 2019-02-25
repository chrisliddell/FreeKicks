using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public float lowerLimit;
    public float upperLimit;
    public float speed;
    public int team;
    public string text;
    bool raising, lowering;
    // Start is called before the first frame update
    void Start()
    {
        raising = false;
        lowering = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (raising)
        {
            transform.position = transform.position + (new Vector3(0,1,0)* Time.deltaTime*speed);
            Debug.Log(transform.position.y);
        }
        else if (lowering)
        {
            transform.position = transform.position - (new Vector3(0, 1, 0) * Time.deltaTime*speed);
            Debug.Log(transform.position.y);
        }
    }

    public void show()
    {
        text = text.Substring(0, 5) + team + text.Substring(6);
        gameObject.GetComponent<Text>().text = text;
        StopAllCoroutines();
        StartCoroutine(Raise());
    }
    
    public void show(int t)
    {
        text = text.Substring(0, 5) + t + text.Substring(6);
        gameObject.GetComponent<Text>().text = text;
        StopAllCoroutines();
        StartCoroutine(Raise());
    }

    IEnumerator Raise()
    {
        raising = true;
        Debug.Log("raising");
        yield return new WaitUntil(() => Centered());
        raising = false;
        StartCoroutine(wait());
    }
    
    bool Centered()
    {
        Debug.Log("Pos y: "+transform.position.y);
        return (transform.position.y > upperLimit);
    }

    public void hide()
    {
        StopAllCoroutines();
        StartCoroutine(Lower());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        hide();
    }

    IEnumerator Lower()
    {
        lowering = true;
        Debug.Log("lowering");
        yield return new WaitUntil(() => Lowered());
        lowering = false;
        GameObject.Find("Controller").GetComponent<GameController>().changeTurns();
    }

    bool Lowered()
    {
        return (transform.position.y < lowerLimit);
    }

}
