using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxObj : MonoBehaviour {
    //public GameObject mouseHomeObj;
    private GameObject targetCatObj = null;
    private Vector3 vTargetPoint;
    private float fAngle;

    bool bTest = false;

	// Use this for initialization
	void Start () {
        targetCatObj = (GameObject)Constant.catCtrl.getSuccessCat();    //성공한 고양이
        bTest = true;
        float fCatSpeed = targetCatObj.GetComponent<CatObj>().SPEED;    //고양이 속도
        float fHalfPoint = Math.Abs(transform.position.x - targetCatObj.transform.position.x) / 2 + transform.position.x; //고양이와 투척지점의 중간 지점
        float fPreMoveDistenceX = fHalfPoint - (fCatSpeed / 150f);    //고양이 속도와 투사체 속도에 따른 예상 목표지점 X값
        vTargetPoint = new Vector3(fPreMoveDistenceX, transform.position.y, transform.position.y);

    }

    // Update is called once per frame
    void Update () {
        transform.RotateAround(vTargetPoint, new Vector3(0, 0, -1), 200f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Equals("cat_boxLine"))
        {
            Destroy(gameObject);

            targetCatObj.GetComponent<Collider2D>().enabled = false;   //선두 고양이 물리 비사용
            targetCatObj.GetComponent<Animator>().SetTrigger("box");

        }
        else
        {

        }
    }

}
