using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCtrl : MonoBehaviour {
    public int 피버_발생_콤보_갯수 = 10;
    public int 피버_생성_고양이 = 5;
    //public float 피버_발생_시간;
    public float 피버_고양이_속도 = 3f;
    public float 피버_고양이_리스폰_시간 = 0.5f;
    public float 피버_종료_후_딜레이시간 = 0.5f;
    public float 콤보_유지_시간 = 3.5f;

    public Text ComboText;
    public Text FeverText;
    public Slider ComboSlider;


    private int m_nComboCount = 0;
    private int m_nFeverGauge = 0;
    private int m_nFeverCatCount = 0;
    private bool m_bFiverState = false;
    private bool m_bFirstCat = false;

    // Use this for initialization
    private void Awake()
    {
        Constant.comboCtrl = this;
        ComboSlider.maxValue = 피버_발생_콤보_갯수;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        setComboGaugeUI(m_nFeverGauge);
        feverListener();
    }

    //콤보 더하기
    public void addComboCount()
    {
        m_nComboCount++;
    }

    //콤보 갯수 설정
    public void setComboCount(int nCount)
    {
        m_nComboCount = nCount;
    }

    //콤보 타이머
    public IEnumerator comboTimer()
    {
        ComboText.enabled = true;
        ComboText.text = m_nComboCount + " Combo";
        yield return new WaitForSeconds(콤보_유지_시간);
        m_nComboCount = 0;
        ComboText.enabled = false;
    }

    //콤보 게이지 UI 설정
    private void setComboGaugeUI(int nGauge)
    {
        ComboSlider.value = nGauge;
    }

    /*---------------------피버---------------------*/
    //피버 게이지 더하기
    public void addFeverGauge()
    {
        if (!m_bFiverState)
        {
            m_nFeverGauge++;
        }
    }

    //피버 게이지 갯수 설정
    public void setFeverGauge(int nGauge)
    {
        m_nFeverGauge = nGauge;
    }

    //피버상태 확인
    public bool isFever()
    {
        feverListener();
        return m_bFiverState;
    }

    //피버 리스너
    private void feverListener()
    {
        if(m_nFeverGauge >= 피버_발생_콤보_갯수 && !m_bFiverState)
        {
            setFeverGauge(0);
            StartCoroutine(fever());

        }
    }

    //피버 발생
    IEnumerator fever()
    {
        Debug.Log("Fever!");
        m_bFiverState = true;
        m_bFirstCat = true;
        FeverText.enabled = true;
        ArrayList arrCurrentCat = Constant.catCtrl.getCatObjs();       //생성되어있는 고양이들
        if(arrCurrentCat != null)
        {
            foreach (GameObject obj in arrCurrentCat)
            {
                obj.GetComponent<CatObj>().SPEED = 피버_고양이_속도;
                obj.GetComponent<Image>().color = Color.red;    //테스트를 위해 피버 상태 고양이 빨갛게 표기
                addFeverCatCount();
            }
        }
        
        yield return new WaitForSeconds(1f);
        FeverText.enabled = false;
    }

    //피버 끝
    public IEnumerator endFever()
    {
        
        yield return new WaitForSeconds(피버_종료_후_딜레이시간);
        m_bFiverState = false;
        setFeverCatCount(0);
        setFeverGauge(0);
    }

    //피버 고양이가 다 나왔는지?
    public bool isEndFeverCat()
    {
        if(m_nFeverCatCount < 피버_생성_고양이)
        {
            return false;
        }
        
        //endFever();
        return true;
    }

    //피버 고양이 카운트
    public void addFeverCatCount()
    {
        m_nFeverCatCount++;
        
    }

    public void setFeverCatCount(int nCount)
    {
        m_nFeverCatCount = nCount;
    }

    public int getFeverCatCount()
    {
        return m_nFeverCatCount;
    }

    //첫 고양이인지?
    public bool isFirstCat()
    {
        return m_bFirstCat;
    }

    public void setFirstCat(bool bFirstCat)
    {
        m_bFirstCat = bFirstCat;
    }
}
