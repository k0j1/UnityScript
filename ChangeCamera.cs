using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField]
    Camera BackCamera = null;
    [SerializeField]
    Camera AboveCamera = null;
    [SerializeField]
    Camera FrontCamera = null;
    public GameObject FrontCameraObj = null;

    private bool bChangeCamera = false;
    private int nCameraMode = 1;

    // Start is called before the first frame update
    void Start()
    {
        nCameraMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // カメラ切り替え
        if(bChangeCamera)
        {
            //スペースキーが押されている間、サブカメラをアクティブにする
            if(nCameraMode == 1){
                //サブカメラをアクティブに設定
                //BackCamera.SetActive(false);
                AboveCamera.depth = 1;
                //BackCamera.enabled = true;
                nCameraMode = 2;
            }
            else
            {
                //メインカメラをアクティブに設定
                AboveCamera.depth = -1;
                //BackCamera.SetActive(true);
                nCameraMode = 1;
            }
            bChangeCamera = false;
        }        
    }
    
    public void Change()
    {
        bChangeCamera = true;　
    }

    public void ChangeEndingCamera()
    {
        nCameraMode = 3;
        //FrontCameraObj.SetActive(true);
        BackCamera.depth = -1;
        AboveCamera.depth = -1;
        FrontCamera.depth = 1;
    }
}
