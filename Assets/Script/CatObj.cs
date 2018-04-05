using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatObj : MonoBehaviour {
    public float SPEED;
    public bool MOVE = true;
	// Use this for initialization
	void Start () {
        Constant.questionCtrl.createQuestion();
        gameObject.name = "cat_" + Constant.catCtrl.getCatObjs().Count;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Constant.comboCtrl.isFever())
        {
            move(Constant.catCtrl.기본_이동속도 * 0.01f);     //기본 이동속도
            SPEED = Constant.catCtrl.기본_이동속도;
        }
        else
        {
            move(Constant.comboCtrl.피버_고양이_속도 * 0.01f);     //피버 이동속도
            SPEED = Constant.comboCtrl.피버_고양이_속도;
        }
        
	}

    //고양이 전진
    public void move(float fSpeed)
    {      
        if (MOVE)
        {
            transform.Translate(Vector3.left * fSpeed *Time.deltaTime);
        }
    }
}
