using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fraction : MonoBehaviour
{
	private int numerator;
	private int denominator;
	
	void Start()
    {
		numerator = 1;
		denominator = 1;

	
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public int getNumerator(){
		return numerator;
	}
	
	public void setNumerator(int n){
		numerator = n;
	}
	
	public int getDenominator(){
		return denominator;
	}
	
	public void getDenominator(int d){
		denominator = d;
	}
	
	public float getPercentage(){
		return ((float)numerator/(float)denominator)*100;
	}
	
	public void increaseNumerator(int n){
		numerator += n;
	}
	
	public void increaseDenominator(int d){
		denominator += d;
	}
	
	public void decreaseNumerator(int n){
		numerator -= n;
	}
	
	public void decreaseDenominator(int d){
		denominator -= d;
	}
	
	public string getFraction(){
		return numerator+"/"+denominator;
	}
}