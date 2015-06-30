using UnityEngine;
using System.Collections;

public class Winner_Power_Up : MonoBehaviour {

	public GameObject winner_Power_Up;

	void OnTriggerEnter2D (Collider2D other)
	{
		Destroy (winner_Power_Up);
		Application.LoadLevel (2);
	}
}
