using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {


	//ESTA CLASSE CONTROLA A BASE SOBRE A QUAL OS CUBOS SE EMPILHAM//


	public bool canMove = false; //define se o usuário pode mover a base
	
	public List<GameObject> rowZero = new List<GameObject>(); //3 LISTAS DE OBJETOS, UMA PARA CADA COLUNA VERTICAL
	public List<GameObject> rowOne = new List<GameObject>(); //3 LISTAS DE OBJETOS, UMA PARA CADA COLUNA VERTICAL
	public List<GameObject> rowTwo = new List<GameObject>(); //3 LISTAS DE OBJETOS, UMA PARA CADA COLUNA VERTICAL


	void Start ()
	{
		canMove = true;
	}

	void Update ()
	{
	
		if (Input.GetKeyDown (KeyCode.A))
			MoveLeft ();
		else if (Input.GetKeyDown (KeyCode.D))
			MoveRight ();

#if !UNITY_EDITOR
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector2 touchPosition = Input.GetTouch(0).position;

			if (touchPosition.x < Screen.width/2 && touchPosition.y < (Screen.height - Screen.height/5))
				MoveLeft ();
			else if (touchPosition.x > Screen.width/2 && touchPosition.y < (Screen.height - Screen.height/5))
				MoveRight ();
		}
#endif
	}

	void MoveLeft ()
	{
		if (transform.position.x > -6.08f && canMove)
			transform.position = new Vector2 (rigidbody.position.x - 1.35f, rigidbody.position.y);
			//rigidbody.MovePosition (new Vector2 (rigidbody.position.x - 1.35f, rigidbody.position.y));
	}

	void MoveRight ()
	{
		if (transform.position.x < 4.72f && canMove)
			transform.position = new Vector2 (rigidbody.position.x + 1.35f, rigidbody.position.y);
			//rigidbody2D.MovePosition (new Vector2 (rigidbody2D.position.x + 1.35f, rigidbody2D.position.y));
	}

}
