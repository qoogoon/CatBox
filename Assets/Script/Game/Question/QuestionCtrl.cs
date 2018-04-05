using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCtrl : MonoBehaviour {
    public int[] 문제수에_따른_고양이_갯수;
    public int 색깔_네개_고양이_갯수;
    public int 색깔_여섯개_고양이_갯수;
    public QuestionRespond questionRespond;

    public GameObject BoxObjPrefeb;
    public GameObject MouseHomeObj;

    //private
    ArrayList arrLogicQuestion;
    int nBtnTouchCounter = 0;       //버튼 터치 카운터
    ArrayList arrRenderQuestion;    //화면에 떠있는 문제 오브젝트
    bool bQuestion = false;         //문제 출제 중 여부
    Coroutine m_comboTimer;         //콤보 타이머
    private bool bBtnUsing = false;

    // Use this for initialization
    private void Awake()
    {
        Constant.questionCtrl = this;    
    }

    void Start () {
        arrLogicQuestion = new ArrayList();
        

    }
	
	// Update is called once per frame
	void Update () {

    }

    //문제 생성
    public void createQuestion()
    {
        if (!bQuestion)
        {
            //생성 문제 갯수 파악
            int nCurrentCatNo = Constant.catCtrl.카운트;   //현재 고양이 번호
            int nCatNumCount = 0;
            int nQuestionCount = 0;
            int[] arrCatNumBySize = 문제수에_따른_고양이_갯수;
            foreach (int nCatNum in arrCatNumBySize)
            {
                nQuestionCount++;
                nCatNumCount += nCatNum;
                if (nCurrentCatNo <= nCatNumCount)
                {

                    break;
                }
            }

            if (nQuestionCount == 0)    //nQuestionCount 0일때 처리
                return;

            //이전 문제 지우기
            arrLogicQuestion.Clear();

            //피버 상태 확인
            
            if (Constant.comboCtrl.isFever())
            {
                Debug.Log(!Constant.comboCtrl.isFever() + ":" + !Constant.comboCtrl.isEndFeverCat() + "/" + Constant.comboCtrl.getFeverCatCount());
                //피버박스로 문제 배열
                for (int i = 0; i < nQuestionCount; i++)
                {
                    arrLogicQuestion.Add(questionRespond.arrQuestionPrep.Length - 1);   //마지막 배열은 피버 박스
                }
                
            }
            else
            {
                //고양이 타입 갯수 결정
                
                int nQuestionTypeCount = 2;
                /*
                if (색깔_여섯개_고양이_갯수 < nCatNumCount)
                {
                    nQuestionTypeCount = 6;
                }
                else 
               
                if (색깔_네개_고양이_갯수 < nCatNumCount)
                {
                    nQuestionTypeCount = 4;
                }
                 *///일단 제거

                //랜덤으로 문제 배열
                int nLastQuestionNo = -1;
                int nRandomNo = 0;
                int nOverlapCount = 0;
                int nOverlapMax = 2;
                for (int i = 0; i < nQuestionCount; i++)
                {
                    if(nOverlapMax > nOverlapCount)
                    {
                        
                        nRandomNo = Random.Range(0, nQuestionTypeCount);  //랜덤으로 문제 선택
                        
                        if (nRandomNo == nLastQuestionNo)
                        {
                            nOverlapCount++;
                        }
                        else
                        {
                            nOverlapCount = 0;
                            nLastQuestionNo = nRandomNo;
                        }
                        Debug.Log("overlap count:"+ nOverlapCount);
                        Debug.Log("select:"+nRandomNo);
                    }
                    else
                    {
                        Debug.Log("overlap:" + nRandomNo);
                        while(nRandomNo == nLastQuestionNo)
                        {
                            nRandomNo = Random.Range(0, nQuestionTypeCount);  //랜덤으로 문제 선택
                        }
                        Debug.Log("final:" + nRandomNo);
                        nOverlapCount = 0;
                        nLastQuestionNo = nRandomNo;
                    }

                    arrLogicQuestion.Add(nRandomNo);
                }
            }

            //화면 표기 호출
            questionRespond.renderQuestion(arrLogicQuestion);
            bQuestion = true;
        }
    }
    //branch test
    //문제 지우기
    void clearQuestion()
    {
        foreach(GameObject obj in arrRenderQuestion)
        {
            Destroy(obj);
        }
        nBtnTouchCounter = 0;
        arrRenderQuestion.Clear();
        
    }

    //문제 버튼
    public void QuestionBtnListner(int nBtnNo)  //0 : 가위, 2 : 마커, 3 : 커터, 1 : 테이프
    {
        if (bQuestion && !Constant.gameCtrl.getGameOverState() && !bBtnUsing)
        {
            bBtnUsing = true;   //동시에 버튼 하나만 사용하게 하기위함
            if ((int)arrLogicQuestion[nBtnTouchCounter]%4 == nBtnNo || Constant.comboCtrl.isFever())
            {
                //정답 처리
                GameObject obj = (GameObject)arrRenderQuestion[nBtnTouchCounter];
                obj.SetActive(false);
                nBtnTouchCounter++;
                
                if (arrLogicQuestion.Count <= nBtnTouchCounter)   //총정답
                {
                    //박스 날리기
                    GameObject BoxObj = Instantiate(BoxObjPrefeb, MouseHomeObj.transform);
                    //GameObject objForwardCat = (GameObject)Constant.catCtrl.getCatObjs()[0];
                    
                    clearQuestion();
                    Constant.catCtrl.setSuccessCat((GameObject)Constant.catCtrl.getCatObjs()[0]);   //선두 고양이를 성공 고양이로 설정
                    Constant.catCtrl.getCatObjs().RemoveAt(0);  //고양이 리스트에서 선두 고양이 삭제

                    //콤보 처리
                    Constant.comboCtrl.addComboCount();  //콤보 카운트
                    Constant.comboCtrl.addFeverGauge();  //피버 카운트

                    
                    if (m_comboTimer != null)
                        StopCoroutine(m_comboTimer);    //이전 콤보 타이머 정지
                    m_comboTimer = StartCoroutine(Constant.comboCtrl.comboTimer());    //새로운 콤보 타이머 시작
                    Constant.comboCtrl.isFever();
                    
                    //생성된 고양이 유무에 따른 문제 생성 처리
                    bQuestion = false;
                    if (Constant.catCtrl.getCatObjs().Count > 0)    
                    {
                        createQuestion();
                    }

                    //할당된 모든 피버 고양이가 등장 된 상태에서 남아 있는 고양이의 유무로 피버 종료를 판단
                    if(Constant.comboCtrl.isFever() && Constant.comboCtrl.isEndFeverCat() && Constant.catCtrl.getCatObjs().Count == 0)
                    {
                        Debug.Log("피버끗");
                        StartCoroutine(Constant.comboCtrl.endFever());
                    }
                }
            }
            else if(bQuestion)
            {
                //오답 처리
                GameObject obj = (GameObject)arrRenderQuestion[nBtnTouchCounter];
                obj.GetComponent<Image>().color = Color.black;
                nBtnTouchCounter++;
                StartCoroutine(wrongQuestion(obj.GetComponent<QuestionObj>()));
                
                #if UNITY_ANDROID
                Handheld.Vibrate(); //진동
#endif
                Constant.comboCtrl.setComboCount(0); //콤보 초기화
                Constant.comboCtrl.setFeverGauge(0); //피버 초기화
            }
            bBtnUsing = false;
        }



    }

    //화면에 떠있는 문제 오브젝트
    public void setRenderQuestion(ArrayList arrRenderImage)
    {
        arrRenderQuestion = arrRenderImage;
        
    }

    //오답 처리
    IEnumerator wrongQuestion(QuestionObj wrongObj)
    {
        wrongObj.showWrongImage(true);
        yield return new WaitForSeconds(0.3f);
        wrongObj.showWrongImage(false);
        clearQuestion();
        bQuestion = false;
        createQuestion();
    }
}
