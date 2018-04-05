using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCtrl : MonoBehaviour {
    public float 기본_이동속도 = 1;
    public float 기본_리스폰시간 = 1;
    public int 리스폰_감소주기 = 5;
    public float 리스폰_감소시간 = 1;
    public float 리스폰_최소시간 = 1;
    public int 카운트 = 0;
    public CatRespond CatRespond;

    private ArrayList arrCatObjs;
    private GameObject successCatObj;
    // Use this for initialization
    void Awake()
    {
        init();
        Constant.catCtrl = this;
        arrCatObjs = new ArrayList();
    }

    void Start () {
		
	}
	
    void init()
    {
        //기본_이동속도 = PlayerPrefs.GetFloat("CatBaseSpeed", 1f);
        //기본_리스폰시간 = PlayerPrefs.GetFloat("CatBaseRespondTime", 1f);
        //리스폰_감소주기 = PlayerPrefs.GetInt("CatRespondReduceCount", 5);
        //리스폰_감소시간 = PlayerPrefs.GetFloat("CatRespondReduceTime", 0.1f);
        //리스폰_최소시간 = PlayerPrefs.GetFloat("CatRespondMinTime", 1);
    }

	// Update is called once per frame
	void Update () {
        if (Constant.comboCtrl.isFever())
        {

        }
	}

    public void addCatObj(GameObject catObj)
    {
        arrCatObjs.Add(catObj);
    }
    
    public ArrayList getCatObjs()
    {
        return arrCatObjs;
    }

    //문제 성공한 고양이
    public void setSuccessCat(GameObject obj)
    {
        successCatObj = obj;
    }

    public GameObject getSuccessCat()
    {
        return successCatObj;
    }
}
