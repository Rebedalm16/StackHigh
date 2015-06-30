using UnityEngine;
using System.Collections;

public class Platform_Power_Up : MonoBehaviour {

	public GameObject platform, platform_Power_Up;
	public AudioClip popsound;


	void OnTriggerEnter2D (Collider2D other)
	{
		Destroy (platform_Power_Up);
		Instantiate (platform, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint (popsound, transform.position);
	}
}
