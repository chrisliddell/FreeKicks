using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
 public class Chronometer : MonoBehaviour {
    public Text timerLabel;
	public bool counting;
    public float time;
 
	void Start(){
		time = 0;
		counting = false;
	}
	
    void Update() {
		if(counting){
			time += Time.deltaTime;
	 
			float minutes = (float)((int)time / 60); 
			float seconds = time % 60;
			timerLabel.text = string.Format ("{0:00} : {1:00} ", minutes, seconds );
		}
    }
	
	public void reset(){
		time = 0;
		counting = false;
	}
	
	public void start(){
		counting = true;
	}
	
	public void stop(){
		counting = false;
	}
	public float getMinutes(){
		return time/60;
	}
 }