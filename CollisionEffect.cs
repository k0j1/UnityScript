using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    public GameObject mEffectCollision;
    public GameObject mEffectGet;
    public float m_nGetFoodRecoverValue = 20.0f;
    private string strCollitionObjName = "";

    // 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Collision!!"); // ログを表示する
        GameObject particle = spawnParticle(mEffectCollision);
        particle.transform.position = collision.gameObject.transform.position;// + particle.transform.position;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Food")
        {
            Debug.Log(hit.collider.gameObject.tag); // ログを表示する
            GameObject particle = spawnParticle(mEffectGet);
            particle.transform.position = this.transform.position;// + particle.transform.position;

            HPGaugeColorChange HP = GameObject.Find("HP").GetComponent<HPGaugeColorChange>();
            HP.RecoverHP(m_nGetFoodRecoverValue);
            // とった食べ物は消える
            hit.collider.gameObject.SetActive(false);
        }
        else if (hit.collider.gameObject.tag != "UnhitObject")
        {
            Debug.Log(hit.collider.gameObject.name + "|" + strCollitionObjName); // ログを表示する
            if (hit.collider.gameObject.name != strCollitionObjName)
            {
                strCollitionObjName = hit.collider.gameObject.name;
                Debug.Log("Hit ControllerCollision!!"); // ログを表示する
                GameObject particle = spawnParticle(mEffectCollision);
                particle.transform.position = this.transform.position;// + particle.transform.position;
                //particle.transform.position.y += 0.1f;
            }
        }
    }

    private GameObject spawnParticle(GameObject instObj)
    {
        GameObject particles = (GameObject)Instantiate(instObj);
        particles.transform.position = new Vector3(0, particles.transform.position.y, 0);
#if UNITY_3_5
			particles.SetActiveRecursively(true);
#else
        particles.SetActive(true);
        //			for(int i = 0; i < particles.transform.childCount; i++)
        //				particles.transform.GetChild(i).gameObject.SetActive(true);
#endif

        ParticleSystem ps = particles.GetComponent<ParticleSystem>();

#if UNITY_5_5_OR_NEWER
        if (ps != null)
        {
            var main = ps.main;
            if (main.loop)
            {
                ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
                ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
            }
        }
#else
		if(ps != null && ps.loop)
		{
			ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
#endif

        return particles;
    }
}
