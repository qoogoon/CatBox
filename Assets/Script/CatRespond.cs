using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatRespond : MonoBehaviour {
    private bool bRespond = true;
    public GameObject CatObj;

	// Use this for initialization
	void Start () {
        StartCoroutine(respond());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void respondStart()
    {
        bRespond = true;
    }

    public void respondStop()
    {
        bRespond = false;
    }

    public IEnumerator respond()
    {
        //고양이 리스폰
        GameObject objCat = null;

        if (!Constant.comboCtrl.isFever() || !Constant.comboCtrl.isEndFeverCat())   //피버상태가 아니거나 피버 고양이가 덜 나온 상테일 경우 '리스폰'
        {
            objCat = Instantiate(CatObj);
            objCat.transform.SetParent(transform);
            objCat.transform.localPosition = new Vector3(0, 0, 0);
            objCat.transform.localScale = new Vector3(1, 1, 1);
            Constant.catCtrl.addCatObj(objCat);
            Constant.catCtrl.카운트++;
        }        

        //다음 리스폰 시간 계산
        float fNextCatRespondTime = 0f;
        if (!Constant.comboCtrl.isFever())  //피버가 아니면
        {
            fNextCatRespondTime = Constant.catCtrl.기본_리스폰시간;
            fNextCatRespondTime -= (Constant.catCtrl.카운트 / Constant.catCtrl.리스폰_감소주기) * Constant.catCtrl.리스폰_감소시간;
            if (fNextCatRespondTime < Constant.catCtrl.리스폰_최소시간)
            {
                fNextCatRespondTime = Constant.catCtrl.리스폰_최소시간;
            }
        }
        else if(!Constant.comboCtrl.isEndFeverCat())
        {
            fNextCatRespondTime = Constant.comboCtrl.피버_고양이_리스폰_시간; //피버이면
            Constant.comboCtrl.addFeverCatCount();

            if(objCat != null)  //테스트를 위해 피버 상태 고양이 빨갛게 표기
                objCat.GetComponent<Image>().color = Color.red;
        }

        if (fNextCatRespondTime <= 0)   //무한루프 방지
        {
            fNextCatRespondTime = 0.1f;
        }
        
        //대기
        float fTimeCount = 0;
        while(fNextCatRespondTime > fTimeCount)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (bRespond)
            {
                fTimeCount += 0.1f;
                if (Constant.comboCtrl.isFever() && Constant.comboCtrl.isFirstCat())
                {
                    Constant.comboCtrl.setFirstCat(false);
                    break;
                }   
            }
        }

        //다음 고양이 리스폰(재귀)
        StartCoroutine(respond());
        
    }
}
