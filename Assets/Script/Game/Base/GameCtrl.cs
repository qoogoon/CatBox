using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {
    private bool m_bExcuteState = true;
    private bool m_bGameOverState = false;
    private int windowWidth = 360; // 고정할 너비
    private int windowHeight = 640; // 고정할 높이
    // Use this for initialization
    private void Awake()
    {
        Constant.gameCtrl = this;
        Screen.SetResolution(windowWidth, windowHeight, false);
    }

    void Start () 
    {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_bGameOverState)
        {
            Constant.UICtrl.showQuitUI(m_bExcuteState);
        }else if (Input.GetKeyDown(KeyCode.Escape) && m_bGameOverState)
        {
            Constant.UICtrl.showQuitUI(m_bGameOverState);
        }

    }
    
    public void restart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void gameQuit()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        gameStop();
        m_bGameOverState = true;
        //재시작 UI
        Constant.UICtrl.showRestartUI();
    }

    public void gameStop()
    {
        m_bExcuteState = false;
        catExecute(m_bExcuteState);
    }

    public void gameResume()
    {
        m_bExcuteState = true;
        if(!m_bGameOverState)
            catExecute(m_bExcuteState);
    }
    
    private void catExecute(bool bExecute)
    {
        //고양이 멈추기
        foreach (GameObject obj in Constant.catCtrl.getCatObjs())
        {
            obj.GetComponent<CatObj>().MOVE = bExecute;
        }

        //고양이 리스폰 중지
        if (bExecute && !m_bGameOverState)
        {
            Constant.catCtrl.CatRespond.respondStart();
        }
        else
        {
            Constant.catCtrl.CatRespond.respondStop();
        }
        
    }

    //게임 오버 상태
    public bool getGameOverState()
    {
        return m_bGameOverState;
    }


}
