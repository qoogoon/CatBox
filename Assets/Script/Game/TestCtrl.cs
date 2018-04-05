using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCtrl : MonoBehaviour {
    public Text CatBaseSpeedTxt;
    public Slider CatBaseSpeedSlider;

    public Text CatBaseRespondTimeTxt;
    public Slider CatBaseRespondTimeSlider;

    public Text CatRespondReduceCountText;
    public Slider CatRespondReduceCountSlider;

    public Text CatRespondReduceTimeTxt;
    public Slider CatRespondReduceTimeSlider;

    public Text CatRespondMinTimeTxt;
    public Slider CatRespondMinTimeSlider;

    public float fCatBaseSpeed;
    public float fCatBaseRespondTime;
    public int nCatRespondReduceCount;
    public float fCatRespondReduceTime;
    public float fCatRespondMinTime;
    // Use this for initialization
    void Start () {
        init();

    }
	
	// Update is called once per frame
	void Update () {
        
        fCatBaseSpeed = CatBaseSpeedSlider.value / 10;
        fCatBaseRespondTime = CatBaseRespondTimeSlider.value / 10;
        nCatRespondReduceCount = (int)CatRespondReduceCountSlider.value;
        fCatRespondReduceTime = CatRespondReduceTimeSlider.value / 100;
        fCatRespondMinTime = CatRespondMinTimeSlider.value / 10;

        CatBaseSpeedTxt.text = (fCatBaseSpeed).ToString();
        CatBaseRespondTimeTxt.text = (fCatBaseRespondTime).ToString();
        CatRespondReduceCountText.text = nCatRespondReduceCount.ToString();
        CatRespondReduceTimeTxt.text = (fCatRespondReduceTime).ToString();
        CatRespondMinTimeTxt.text = (fCatRespondMinTime).ToString();
        
    }

    void init()
    {
        //테스트 설정 정보 가져오기(이 정보는 테스트가 끝나면 고정으로 할 것이기 때문에 지운다)
        //fCatBaseSpeed = PlayerPrefs.GetFloat("CatBaseSpeed", 1f);
        fCatBaseSpeed = Constant.catCtrl.기본_이동속도;
        fCatBaseRespondTime = Constant.catCtrl.기본_리스폰시간;
        nCatRespondReduceCount = Constant.catCtrl.리스폰_감소주기;
        fCatRespondReduceTime = Constant.catCtrl.리스폰_감소시간;
        fCatRespondMinTime = Constant.catCtrl.리스폰_최소시간;

        CatBaseSpeedTxt.text = fCatBaseSpeed.ToString();
        CatBaseSpeedSlider.value = fCatBaseSpeed * 10;

        CatBaseRespondTimeTxt.text = fCatBaseRespondTime.ToString();
        CatBaseRespondTimeSlider.value = fCatBaseRespondTime * 10;

        CatRespondReduceCountText.text = nCatRespondReduceCount.ToString();
        CatRespondReduceCountSlider.value = nCatRespondReduceCount;

        CatRespondReduceTimeTxt.text = fCatRespondReduceTime.ToString();
        CatRespondReduceTimeSlider.value = fCatRespondReduceTime *100;

        CatRespondMinTimeTxt.text = fCatRespondMinTime.ToString();
        CatRespondMinTimeSlider.value = fCatRespondMinTime *10;
    }

    public void setTestSetting()
    {
        PlayerPrefs.SetFloat("CatBaseSpeed", fCatBaseSpeed );
        PlayerPrefs.SetFloat("CatBaseRespondTime", fCatBaseRespondTime);
        PlayerPrefs.SetInt("CatRespondReduceCount", nCatRespondReduceCount);
        PlayerPrefs.SetFloat("CatRespondReduceTime", fCatRespondReduceTime);
        PlayerPrefs.SetFloat("CatRespondMinTime", fCatRespondMinTime);

        Constant.gameCtrl.restart();
    }
}
