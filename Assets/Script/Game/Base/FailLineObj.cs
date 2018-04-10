using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailLineObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //고양이인지?
        if (collision.tag.Equals("Cat"))
        {
            //박스를 쓴 고양이 인지?
            CatObj catObj = collision.GetComponent<CatObj>();
            bool bBoxCat = catObj.BoxCat;
            if (!bBoxCat)
            {
                Constant.gameCtrl.gameOver();
            }
            
        }

        
    }
}
