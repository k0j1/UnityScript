using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Timer : MonoBehaviour {
 
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	// 生き残った合計時間（秒）を記録
	public int totalSeconds = 0;
	//　前のUpdateの時の秒数
	private float oldSeconds;
	//　タイマー表示用テキスト
	private Text timerText;

	bool m_bGameFinish = false;
 
	void Start ()
    {
		timerText = GetComponentInChildren<Text> ();
		ResetTimer();
	}
 
	void Update ()
    {
		// ゲーム終了していたらカウントを止める
		if (m_bGameFinish) return;

		seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}

		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
			oldSeconds = seconds;
			totalSeconds++;
		}
	}
    
    public void ResetTimer()
    {
        minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		totalSeconds = 0;
		timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
    }
    
    public void StopTimer()
    {
        Time.timeScale = 0f;
    }
    
    public void StartTimer()
    {
        Time.timeScale = 1f;        
    }

	public void FinishTimer()
    {
		m_bGameFinish = true;
		Debug.Log("生存時間：" + totalSeconds);
		// 時間を記録
		SettingManager.SaveGamePrePlayTime(totalSeconds);
		SettingManager.SaveGameTotalPlayTime(totalSeconds);
		SettingManager.SaveGameBestTime(totalSeconds);
    }
}