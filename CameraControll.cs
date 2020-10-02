using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraControll : MonoBehaviour
{
 
    private GameObject mainCamera;      //メインカメラ格納用
    private GameObject subCamera;       //サブカメラ格納用 
    
    private GameObject playerObject;            //回転の中心となるプレイヤー格納用
    public float rotateSpeed = 5.0f;            //回転の速さ
    public int nCameraMode = 1; 
 
    //呼び出し時に実行される関数
    void Start()
    {
        //メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("MainCamera");
        subCamera = GameObject.Find("SubCamera");
        playerObject = GameObject.Find("cat");
    }
 
 
    //単位時間ごとに実行される関数
    void Update()
    {
		//スペースキーが押されている間、サブカメラをアクティブにする
        if(Input.GetKey(KeyCode.Space)){
            //サブカメラをアクティブに設定
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
            nCameraMode = 2;
        }
        else{
            //メインカメラをアクティブに設定
            subCamera.SetActive(false);
            mainCamera.SetActive(true);
            nCameraMode = 1;
        }        
        //左シフトが押されている時
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //ユニティちゃんを中心に-5f度回転
            transform.RotateAround(playerObject.transform.position, Vector3.up, -5f);
        }
        //右シフトが押されている時
        else if(Input.GetKey(KeyCode.RightShift))
        {
            //ユニティちゃんを中心に5f度回転
            transform.RotateAround(playerObject.transform.position, Vector3.up, 5f);
        }
        //rotateCameraの呼び出し
        rotateCamera();
    }
 
    //カメラを回転させる関数
    private void rotateCamera()
    {
        //Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed,Input.GetAxis("Mouse Y") * rotateSpeed, 0);
 
        //transform.RotateAround()をしようしてメインカメラを回転させる
        if(nCameraMode == 1)
        {
            mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
            mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
        }
        else if(nCameraMode == 2)
        {
            subCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
            subCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
        }
    }
}