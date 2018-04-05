using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionObj : MonoBehaviour {
    public Image wrongImage;
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showWrongImage(bool bShow)
    {
        float fA = 0;
        if (bShow)
        {
            fA = 1f;
        }
        
        wrongImage.color = new Color(wrongImage.color.r, wrongImage.color.g, wrongImage.color.b, fA);
    }
}
