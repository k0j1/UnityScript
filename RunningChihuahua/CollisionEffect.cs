using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionEffect : MonoBehaviour
{
    //public GameObject mEffect;
    //public GameObject mEffectGet;
    //public float m_nGetFoodRecoverValue = 20.0f;
    private string strCollitionObjName = "Block";

    public SkinnedMeshRenderer mPlayerMeshRenderer = null;
    public GameObject mBrokenButtonObj = null;
    public GameObject mMissButtonObj = null;
    public Button mBrokenButton = null;
    public Button mMissButton = null;
    public GameSystem mGameSystem = null;
    public AudioClip mBombAudio = null;
    public AudioClip mDamageAudio = null;

    void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }

    private GameObject mBlockObj = null;
    private GameObject mEffectObj = null;
    private ParticleSystem mEffectSys = null;
    private AudioSource mAudio = null;
    public void PlayEffectBomb()
    {
        if (mEffectObj && mEffectSys)
        {
            mEffectObj.SetActive(true);
            mEffectSys.Play();
            if(mBombAudio) mAudio.PlayOneShot(mBombAudio);
            mBlockObj.SetActive(false);
            ResetObject();
        }
    }
    void ResetObject()
    {
        if (mBrokenButton && mBrokenButtonObj) mBrokenButtonObj.SetActive(false);
        mBlockObj = null;
        mEffectObj = null;
        mEffectSys = null;
    }

    void FixedUpdate()
    {
        //ダメージを受けた時の処理
        if (mGameSystem.GetDamage())
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            Color _color = mPlayerMeshRenderer.material.color;
            _color.a = level;
            mPlayerMeshRenderer.material.color = _color;
            //Debug.Log("Damage!! alpha=" + level);
        }
    }
    IEnumerator WaitForIt()
    {
        if(mGameSystem.GetDamage())
        {
            // 2秒間処理を止める
            yield return new WaitForSeconds(2f);
            // 2秒後ダメージフラグをfalseにして点滅を戻す
            mGameSystem.SetDamage(false);
            Color _color = mPlayerMeshRenderer.material.color;
            _color.a = 1f;
            mPlayerMeshRenderer.material.color = _color;
            Debug.Log("Damage End");
        }
    }

    // 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("Hit Collision!!"); // ログを表示する
        //GameObject particle = spawnParticle(mEffect);
        //particle.transform.position = collision.gameObject.transform.position;// + particle.transform.position;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.LogWarning("Hit ColliderHit!!"); // ログを表示する
        if (hit.collider.gameObject.tag == strCollitionObjName)
        {
            Debug.Log("Hit ColliderHit - block"); // ログを表示する
            //Debug.Log(hit.collider.gameObject.tag); // ログを表示する
            //GameObject particle = spawnParticle(mEffect);
            //particle.transform.position = this.transform.position;// + particle.transform.position;
        }
    }

    //オブジェクトが衝突したとき
    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("OnTriggerEnter!! " + other.name); // ログを表示する
        if(other.tag == strCollitionObjName)
        {
            if(mBrokenButton && mBrokenButtonObj)
            {
                int nRandX = Random.Range(100, 900);
                int nRandY = Random.Range(-700, -240);
                Vector3 nRandPos = new Vector3(nRandX, nRandY, 0);
                mBrokenButtonObj.SetActive(true);
                mBrokenButtonObj.GetComponent<RectTransform>().anchoredPosition = nRandPos;
                mBlockObj = other.gameObject;
                mEffectObj = mBlockObj.transform.parent.transform.GetChild(0).gameObject;
                mEffectSys = mEffectObj.GetComponent<ParticleSystem>();
            }

        }
    }

    //オブジェクトが離れた時
    void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("OnTriggerExit!! " + other.name); // ログを表示する
        if (other.tag == strCollitionObjName)
        {
            if (mBrokenButton && mBrokenButtonObj)
            {
                if(mBlockObj.activeSelf)
                {
                    PlayEffectBomb();
                    mGameSystem.SetDamage(true);
                    if (mDamageAudio) mAudio.PlayOneShot(mDamageAudio);
                    StartCoroutine("WaitForIt");
                }
                ResetObject();
            }

        }
    }
    //オブジェクトが触れている間
    //void OnTriggerStay(Collider other)
    //{
    //}

}