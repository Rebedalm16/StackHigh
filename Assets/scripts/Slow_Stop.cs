using UnityEngine;
using System.Collections;

public class Slow_Stop : MonoBehaviour {

	public GameObject slow_Stop_Trigger;

	void OnTriggerEnter2D (Collider2D other)
	{
		Time.timeScale = 1;
	}
}
