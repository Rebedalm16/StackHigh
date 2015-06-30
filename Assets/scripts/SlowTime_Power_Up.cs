using UnityEngine;
using System.Collections;

public class SlowTime_Power_Up : MonoBehaviour {

	public GameObject slowTime_Power_Up;
	
	void OnTriggerEnter2D (Collider2D other)
	{
		Time.timeScale = 0.5f;

		Destroy (slowTime_Power_Up);
	}
}
