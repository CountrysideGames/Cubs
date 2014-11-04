using UnityEngine;
using System.Collections;

public class ButtonRestart : MonoBehaviour {

	//BOTAO DE RESTART

	public Game gameManager;



	void OnClick ()
	{
		transform.parent.animation.Play ("pause-hide");
		gameManager.StartGame ();
	}

}
