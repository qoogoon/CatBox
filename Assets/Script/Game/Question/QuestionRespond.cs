using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionRespond : MonoBehaviour {
    public GameObject[] arrQuestionPrep;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void renderQuestion(ArrayList arrLogicQuestion)
    {
        //시작 위치 잡기
        int nQustionNum = arrLogicQuestion.Count;
        float fFirstPositionX = 0f;
        ArrayList arrQuestionPosition = new ArrayList();
        float fQuestionObjWidth = arrQuestionPrep[0].GetComponent<RectTransform>().rect.width;
        if(nQustionNum % 2 != 0)    //홀짝
        {
            //arrQuestionPosition[nQustionNum / 2 + 1] = Vector3.zero;
            for (int i = 0; i < nQustionNum / 2; i++)
            {
                fFirstPositionX -= fQuestionObjWidth;
            }
        }
        else
        {
            fFirstPositionX += fQuestionObjWidth / 2;
            for (int i = 0; i < nQustionNum / 2; i++)
            {
                fFirstPositionX -= fQuestionObjWidth;
            }
        }

        //위치 할당
        for(int i = 0; i< arrLogicQuestion.Count; i++)
        {
            arrQuestionPosition.Add(new Vector3(fFirstPositionX + fQuestionObjWidth * i,0,0));

        }

        //화면에 표기
        ArrayList arrRenderQuestion = new ArrayList();
        for (int i = 0; i< arrLogicQuestion.Count; i++)
        {
            GameObject obj = Instantiate(arrQuestionPrep[(int)arrLogicQuestion[i]], transform, false);
            obj.transform.localPosition = (Vector3)arrQuestionPosition[i];
            arrRenderQuestion.Add(obj);
        }
        Constant.questionCtrl.setRenderQuestion(arrRenderQuestion); //화면의 문제 오브젝트들 보내기
    }
}
