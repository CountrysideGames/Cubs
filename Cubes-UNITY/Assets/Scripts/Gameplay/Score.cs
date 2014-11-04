using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {


	private UILabel label;


	void Awake ()
	{
		label = GetComponent<UILabel> ();
	}


	void Update ()
	{
		label.text = Game.score.ToString ();
	}
}
