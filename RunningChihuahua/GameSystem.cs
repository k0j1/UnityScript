using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public Camera MainCamera = null;
    public Camera SubCamera = null;
    public Camera Camera_3 = null;
    private bool bChangeCamera = false;
    public int nCameraMode = 1;

    // Game
    public GameObject mCharactor01 = null;
    public GameObject mCharactor02 = null;
    private CharacterController characterController01 = null;
    private CharacterController characterController02 = null;
    public GameObject RotateCenterObj;
    public float walkSpeed = 50f;
    public float gravityValue = -9.81f;
    private bool m_bIsDamage = false;
    public bool GetDamage() { return m_bIsDamage; }
    public void SetDamage(bool bFlag) { m_bIsDamage = bFlag; moveSpeedRunner = 0f; }

    private int nDistance = 0;
    public float Distance = 0.0f;
    public Text DistanceText = null;

    public GameObject mMap = null;
    public GameObject mMap01 = null;
    public GameObject mMap02 = null;
    public GameObject mMap03 = null;
    public GameObject mMap04 = null;
    public GameObject mMap05 = null;
    public GameObject mMap06 = null;
    public GameObject mMap07 = null;
    public GameObject mMap08 = null;
    public GameObject mMap09 = null;
    public GameObject mMap10 = null;
    public GameObject mRunner = null;
    public GameObject mChaser = null;
    public Animator mRunnerAnim = null;
    public Animator mChaserAnim = null;
    public CharacterController mRunnerCharCon = null;
    public float moveSpeedRunner = 0f;
    float addSpeedRunner = -0.25f;
    float addStopSpeedRunner = -0.05f;
    public bool m_bMoveStop = false;
    public Button mRunBtn = null;
    int nMode = 0;

    public GameObject mTutorial1 = null;
    public GameObject mTutorial2 = null;

    // 結果
    public Text ResultText = null;

    // Start is called before the first frame update
    void Start()
    {
        //nCameraMode = 1;
        bChangeCamera = true;
        ChangeCamera();
        DispTutorial(SettingManager.LoadTutorialMode());
        DispResult();

        //mCharactor01 = null;
        //mCharactor02 = null;
        //characterController01 = null;
        //characterController02 = null;
        //if (mRunBtn) mRunBtn.animator.SetTrigger("Highlighted");
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////////////////////////////////////////////
        // キャラクターの移動
        if (mCharactor01)
            characterController01 = mCharactor01.GetComponent<CharacterController>();
        if (mCharactor02)
            characterController02 = mCharactor02.GetComponent<CharacterController>();

        if (characterController01) MoveCharactor(mCharactor01, characterController01);
        if (characterController02) MoveCharactor(mCharactor02, characterController02);

        //////////////////////////////////////////////////////////////////////////////
        /// ゲーム中動作
        if(mMap && mRunner && mChaser && nMode == 0)
        {
            // マップの移動
            MoveMap(mMap, mRunner.transform, mChaser.transform, 0f);
            if (mMap01) MoveMap(mMap01, mRunner.transform, mChaser.transform);
            if (mMap02) MoveMap(mMap02, mRunner.transform, mChaser.transform);
            if (mMap03) MoveMap(mMap03, mRunner.transform, mChaser.transform);
            if (mMap04) MoveMap(mMap04, mRunner.transform, mChaser.transform);
            if (mMap05) MoveMap(mMap05, mRunner.transform, mChaser.transform);
            if (mMap06) MoveMap(mMap06, mRunner.transform, mChaser.transform);
            if (mMap07) MoveMap(mMap07, mRunner.transform, mChaser.transform);
            if (mMap08) MoveMap(mMap08, mRunner.transform, mChaser.transform);
            if (mMap09) MoveMap(mMap09, mRunner.transform, mChaser.transform);
            if (mMap10) MoveMap(mMap10, mRunner.transform, mChaser.transform);

            // 距離の表示
            if(DistanceText)
            {
                Distance += AddDistance();
                int nCeilDistance = Mathf.CeilToInt(Distance);
                if(nDistance < nCeilDistance)
                {
                    nDistance = nCeilDistance;
                    DistanceText.text = "" + nDistance + " m";
                }
            }

            // キャラクターの移動
            float step = moveSpeedRunner * Time.deltaTime;
            float nNotMoveY = mRunner.transform.position.y;
            mRunner.transform.position = Vector3.MoveTowards(mRunner.transform.position, mChaser.transform.position, step);
            bool bStop = false;
            Vector3 RunPos = mRunner.transform.position;
            RunPos.y = nNotMoveY;
            if (mRunner.transform.position.x < -2.0f)
            {
                RunPos.x = -2.0f;
                bStop = true;
            }
            if (mRunner.transform.position.z > -3.0f)
            {
                RunPos.z = -3.0f;
                bStop = true;
            }
            if (bStop)
            {
                mRunner.transform.position = RunPos;
                m_bMoveStop = true;
            }
            else
            {
                m_bMoveStop = false;
            }
            // 距離が縮まれば結果画面へ
            float distance = Vector3.Distance(mRunner.transform.position, mChaser.transform.position);
            //Debug.Log("Dog - Gorilla : " + distance);
            if (distance < 1.0f)
            {
                //LoadScene("Result");
                nMode = 99;
                bChangeCamera = true;
                nCameraMode = 1;
                StartCoroutine("StartEnding");
            }
            else
            {
                // 重力の調整
                //if (mRunnerCharCon && !mRunnerCharCon.isGrounded)
                //{
                //    Vector3 RunGrav = Vector3.zero;
                //    RunGrav.x = 0;
                //    RunGrav.z = 0;
                //    RunGrav.y += Physics.gravity.y * Time.deltaTime;
                //    mRunnerCharCon.Move(RunGrav * Time.deltaTime);
                //}
            }
            moveSpeedRunner += Time.deltaTime;
        }

        Vector3 pos = CommonFunc.GetTouchPosition();
        if(pos != Vector3.zero)
        {
            //Debug.Log(pos);
            //bChangeCamera = true;
        }
        else
        {
            if(bChangeCamera)
            {
                ChangeCamera();
            }
        }
    }

    public void ChangeCameraMode()
    {
        bChangeCamera = true;
    }

    public void AddMoveSpeedRunner()
    {
        if(!m_bIsDamage)
        {
            float addSpeed = addSpeedRunner;
            if (m_bMoveStop)
            {
                addSpeed = addStopSpeedRunner;
                moveSpeedRunner = 0;
            }
            moveSpeedRunner += addSpeedRunner;
        }
    }

    public float AddDistance()
    {
        return ((walkSpeed / 5) * Time.deltaTime);
    }

    /// <summary>
    /// カメラのリセット
    /// </summary>
    void ResetCamera()
    {
        if(MainCamera) MainCamera.depth = -1;
        if(SubCamera) SubCamera.depth = -1;
        if(Camera_3) Camera_3.depth = -1;
    }

    /// <summary>
    /// カメラ切り替え
    /// </summary>
    void ChangeCamera()
    {
        if (bChangeCamera)
        {
            //スペースキーが押されている間、サブカメラをアクティブにする
            switch(nCameraMode)
            {
                case 1:
                    //サブカメラをアクティブに設定
                    ResetCamera();
                    if(SubCamera) SubCamera.depth = 1;
                    nCameraMode = 2;
                    break;
                case 2:
                    //メインカメラをアクティブに設定
                    ResetCamera();
                    if(MainCamera) MainCamera.depth = 1;
                    nCameraMode = 3;
                    break;
                case 3:
                    //カメラ3をアクティブに設定
                    ResetCamera();
                    if(Camera_3) Camera_3.depth = 1;
                    //BackCamera.SetActive(true);
                    nCameraMode = 1;
                    break;
            }
            bChangeCamera = false;
        }
    }

    public void DispTutorial(int nMode)
    {
        // 例外処理
        if (!mTutorial1 && !mTutorial2) return;

        Debug.LogWarning("DispTutorial:Mode-" + nMode);
        if (mTutorial1) mTutorial1.SetActive(false);
        if (mTutorial2) mTutorial2.SetActive(false);
        switch (nMode)
        {
            case 1:
                if (mTutorial1) mTutorial1.SetActive(true);
                CommonFunc.StopTimer();
                break;
            case 2:
                if (mTutorial2) mTutorial2.SetActive(true);
                CommonFunc.StopTimer();
                break;
            default:
                CommonFunc.StartTimer();
                SettingManager.SaveTutorialMode(0);
                break;
        }
    }
    public void DispResult()
    {
        if(ResultText)
        {
            int nDistance = SettingManager.LoadDistance();
            int nTotalDistance = SettingManager.LoadTotalDistance();
            int nBestDistance = SettingManager.LoadBestDistance();
            ResultText.text = string.Format("　きょり：{0,5}m\n　ベスト：{1,5}m\nごうけい：{2,5}m", nDistance, nBestDistance, nTotalDistance);
        }
    }

    /// <summary>
    /// キャラクターの移動
    /// </summary>
    /// <param name="Charactor">移動したいキャラクターのGameObject</param>
    /// <param name="characterController">移動したいキャラクターのCharacterController</param>
    public void MoveCharactor(GameObject Charactor, CharacterController characterController)
    {
        Vector3 moveDirection = Vector3.zero;  //移動方向

        //移動方向に向けてキャラを回転させる
        //mCharactor.transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        if (!characterController.isGrounded)
        {
            //y軸方向への移動に重力を加える
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
            //CharacterControllerを移動させる
            characterController.Move(moveDirection * Time.deltaTime);
        }

        if (characterController.isGrounded)
        {
            Charactor.transform.RotateAround(RotateCenterObj.transform.position, Vector3.up, walkSpeed * Time.deltaTime);
        }
        Debug.Log(moveDirection);
    }
    /// <summary>
    /// 地面の移動
    /// </summary>
    /// <param name="Obj"></param>
    /// <param name="moveTrans"></param>
    /// <param name="hideTrans"></param>
    /// <param name="moveObj"></param>
    /// <returns></returns>
    public bool MoveMap(GameObject Obj, Transform moveTrans, Transform hideTrans = null, float moveObj = 90f)
    {
        // 例外処理
        if (!Obj) return false;

        if(Obj.activeSelf)
            Obj.transform.position -= moveTrans.forward * AddDistance();

        // オブジェクトの移動
        if(hideTrans && ((hideTrans.position.x + 30f) < Obj.transform.position.x || (hideTrans.position.z - 30f) > Obj.transform.position.z))
        {
            //Obj.transform.position.x -= 60f;
            if(0f < moveObj)
            {
                Obj.transform.Translate(-moveObj, 0, moveObj);
            }
            else
            {
                Obj.SetActive(false);
            }
            // 障害物のアクティブ化、位置調整
            for (int nIndex=0; nIndex<Obj.transform.childCount; nIndex++)
            {
                GameObject ChildObj = Obj.transform.GetChild(nIndex).gameObject;
                if(ChildObj.tag == "Block")
                {
                    ChildObj.SetActive(true);
                    float nRandPos = Random.Range(-14.5f,14.5f);
                    // ブロックの位置調整
                    Vector3 ChildPos = Obj.transform.position;
                    ChildPos.x -= nRandPos;
                    ChildPos.z += nRandPos;
                    ChildObj.transform.position = ChildPos;
                    // エフェクトの位置調整
                    GameObject EffectObj = ChildObj.transform.parent.transform.GetChild(0).gameObject;
                    Vector3 EffectPos = Obj.transform.position;
                    EffectPos.y = 1.0f;
                    EffectPos.x -= nRandPos;
                    EffectPos.z += nRandPos;
                    EffectObj.transform.position = EffectPos;
                }
            }
        }

        //Debug.Log(Obj.transform.position);

        return true;
    }

    // 別シーン呼び出し
    public void LoadScene(string strScene)
    {
        SceneManager.LoadScene(strScene);
    }

    public void LoadTitleToGame()
    {
        SettingManager.SaveTutorialMode(1);
        LoadScene("Game");
    }

    ////////////////////////////////////////////////////////////////////////////
    // 各スレッド
    IEnumerator StartEnding()
    {
        if (nMode == 99)
        {
            // チワワ、ゴリラ止まる
            if (mRunnerAnim) mRunnerAnim.SetBool("isRunning", false);
            if (mChaserAnim) mChaserAnim.SetBool("isRunning", false);
            yield return new WaitForSeconds(1); //1秒待つ

            // チワワ座る、ゴリラ攻撃
            if (mRunnerAnim) mRunnerAnim.SetBool("isSitting", true);
            if (mChaserAnim) mChaserAnim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(1f); //1秒待つ

            // ゴリラ吠える
            if (mChaserAnim) mChaserAnim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(0.5f); //1秒待つ
            if (mChaserAnim) mChaserAnim.SetBool("isChestHit", true);
            yield return new WaitForSeconds(2.5f); //2.5秒待つ

            // 走行距離の保存
            SettingManager.SaveDistance(nDistance);

            // 結果画面へ
            LoadScene("Result");
        }
    }
}
