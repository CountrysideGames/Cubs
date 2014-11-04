using UnityEngine;
using System.Collections;

public class ButtonPause : MonoBehaviour {


	//BOTAO DE PAUSE


	void OnClick ()
	{
		if (Game.time != 0)
			Pause ();
		else
			Resume ();
	}


	void Pause ()
	{
		animation.Play ("pause-show");

		Game.time = 0;
		Game.cubeBase.GetComponent<Base> ().canMove = false; //IMPEDE A BASE DE MOVER
	}

	void Resume ()
	{
		animation.Play ("pause-hide");

		Game.time = 1;
		Game.cubeBase.GetComponent<Base> ().canMove = true; //PERMITE QUE A BASE POSSA SER MOVIDA
	}
}
