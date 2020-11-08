using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	// 生き残った合計時間（秒）を記録
	public int totalSeconds = 0;
	//　前のUpdateの時の秒数
	private float oldSeconds;

	public GameObject mEffect;
	public GameObject mPosition;

	void Start()
	{
		ResetTimer();
	}

	void Update()
	{
		seconds += Time.deltaTime;
		if (seconds >= 60f)
		{
			minute++;
			seconds = seconds - 60;
		}

		//　値が変わった時だけテキストUIを更新
		if ((int)seconds != (int)oldSeconds)
		{
			oldSeconds = seconds;
			totalSeconds++;

			GameObject particle = spawnParticle(mEffect);
			Vector3 pos = new Vector3(Random.Range(-4.0f, 0.0f), Random.Range(3.0f, 5.0f), Random.Range(-2.0f, 5.0f));
			particle.transform.position = pos;
		}
	}

	public void ResetTimer()
	{
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		totalSeconds = 0;
	}

	public void StopTimer()
	{
		Time.timeScale = 0f;
	}

	public void StartTimer()
	{
		Time.timeScale = 1f;
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