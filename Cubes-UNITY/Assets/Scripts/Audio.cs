using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class Audio : MonoBehaviour {


	public static Audio manager;
	public AudioClip[] audioClips = new AudioClip[3];

	void Awake ()
	{
		manager = this.GetComponent<Audio>();
	}

	//TOCA O SOM REFERENTE À VARIÁVEL "audioClipName"
	public void Play (string audioClipName)
	{
		for (int i = 0; i < audioClips.Length; i++)
			if (audioClips[i].name == audioClipName)
				audio.clip = audioClips[i];

		audio.Play ();
	}
}
