using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour {
    public GameObject RestartUIObj;
    public GameObject QuitUIObj;
    public GameObject TestUIObj;

    private bool bShowRestart = false;
    private bool bShowQuit = false;
    // Use this for initialization
    private void Awake()
    {
        Constant.UICtrl = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //게임 오버 시에 리스타트 화면 보여주기
    public void showRestartUI()
    {
        RestartUIObj.transform.localPosition = Vector3.zero;
        bShowRestart = true;
    }

    public void showQuitUI(bool bShow)
    {
        if (bShow)
        {
            //RestartUIObj.transform.localPosition = new Vector3(1000, 0, 0); 
            QuitUIObj.transform.localPosition = Vector3.zero;
            Constant.gameCtrl.gameStop();
        }
        else
        {
            QuitUIObj.transform.localPosition = new Vector3(1000,0,0);
            Constant.gameCtrl.gameResume();
        }
        
    }

   //테스트 창 열기/닫기
   public void showTestUI(bool bShow)
    {
        if (bShow)
        {
            TestUIObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            TestUIObj.transform.localPosition = new Vector3(1164f, -397f, 0f);
        }
        
    }

}
