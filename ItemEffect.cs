using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    //    public GameObject mEffectCollision;
    //    private string strCollitionObjName = "";
    private CharacterController characterController = null;
    protected float maxFallSpeed = 20f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        // 着地中以外の動作
        if (characterController && !characterController.isGrounded)
        {
            // 重力追加
            float nGravity = Physics.gravity.y * Time.deltaTime;

            // ジャンプ＋重力で更新
            Vector3 pos = Vector3.zero;
            pos.y = nGravity;
            pos.y = Mathf.Max(pos.y, -maxFallSpeed);
            pos.y = -maxFallSpeed;

            characterController.Move(pos * Time.deltaTime);
        }
    }


    // 当たり判定
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Hit CollisionEnter!!"); // ログを表示する
    //    GameObject particle = spawnParticle(mEffectCollision);
    //    particle.transform.position = collision.gameObject.transform.position;// + particle.transform.position;
    //}
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Debug.Log("Hit CollisionHit!!"); // ログを表示する
    //    //Debug.Log(hit.collider.gameObject.tag); // ログを表示する
    //    if (hit.collider.gameObject.tag == "Player")
    //    {
    //        Debug.Log(hit.collider.gameObject.name + "|" + strCollitionObjName); // ログを表示する
    //        if (hit.collider.gameObject.name != strCollitionObjName)
    //        {
    //            strCollitionObjName = hit.collider.gameObject.name;
    //            Debug.Log("Hit ControllerCollision!!"); // ログを表示する
    //            GameObject particle = spawnParticle(mEffectCollision);
    //            particle.transform.position = this.transform.position;// + particle.transform.position;
    //            //particle.transform.position.y += 0.1f;

    //            GameObject Item = GetComponent<GameObject>();
    //            Item.SetActive(false);
    //        }
    //    }
    //}

    //private GameObject spawnParticle(GameObject instObj)
    //{
    //    GameObject particles = (GameObject)Instantiate(instObj);
    //    particles.transform.position = new Vector3(0, particles.transform.position.y, 0);
    //    particles.SetActive(true);

    //    ParticleSystem ps = particles.GetComponent<ParticleSystem>();
    //    if (ps != null)
    //    {
    //        var main = ps.main;
    //        if (main.loop)
    //        {
    //            ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
    //            ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
    //        }
    //    }

    //    return particles;
    //}
}
