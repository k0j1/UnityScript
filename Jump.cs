using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private GameObject playerObject;            //回転の中心となるプレイヤー格納用
    private GameObject DoSkillSound;            //スキル実行時の効果音再生用
    
    public void OnClick()
    {
        Debug.Log("Jump.OnClick Called.");
        //
        //playerObject.GetComponent<MoveCharactor> ().JumpValue(5f);
        //playerObject.Jump();
        
        //playerObject.GetComponent<MoveCharactor> ().jumpPower = 5f;
     
        //playerObject.GetComponent<DoSkillSound> ().StartSound();
        
    }
}
