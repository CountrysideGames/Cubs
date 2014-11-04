using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube : MonoBehaviour {


	//ESTA CLASSE DEFINE A MOVIMENTAÇÃO, AS CONEXÕES, A COR E O ESTADO DO CUBO//
	//TAMBEM REMOVE OS CUBOS QUANDO SE FORMA UMA LINHA, E ADICIONA PONTOS AO SCORE//

	public GameObject[] feeler = new GameObject[4];

	private static float speed = .4f;

	public int number = 0; //NUMERO DO CUBO (O MESMO QUE APARECE NO FIM DO NOME DELE)

	public int color = 0; //COLOR 0 = VERDE / COLOR 1 = AMARELO / COLOR 2 = VERMELHO / COLOR 3 = AZUL

	public int moveState = 0; //cubemoveState 0 = MOVENDO / moveState 1 = ENCAIXADO
	
	public List<GameObject> VerticalConnections = new List<GameObject>(); //LISTA DE CUBOS GÊMEOS QUE ESTÁO CONECTADOS HORIZONTALMENTE A ESTE

	public List<GameObject> VerticalTwins = new List<GameObject>(); //LISTA DE CUBOS GÊMEOS QUE ESTÁO CONECTADOS VERTICALMENTE A ESTE
	
	public List<GameObject> HorizontalTwins = new List<GameObject>(); //LISTA DE CUBOS GÊMEOS QUE ESTÁO CONECTADOS HORIZONTALMENTE A ESTE


	void Start ()
	{
		speed = .4f;
		moveState = 1;

		InvokeRepeating ("MoveDown", speed, speed);
	}

	//FAZ O CUBO DESCER A TELA
	void MoveDown ()
	{
		if (Game.time != 0) //SE O JOGO NAO ESTIVER PAUSADO
			rigidbody.MovePosition (new Vector2 (rigidbody.position.x, rigidbody.position.y - 1.35f * moveState));

		//CONFERE QUANTOS GEMEOS ESTE CUBO TEM
		if (VerticalTwins.Count > 1)
		{
			Remove ("vertical");
			print ("Vertical line!");
		}
		if (HorizontalTwins.Count > 1)
		{
			Remove ("horizontal");
			print ("Horizontal line!");
		}
	}

	//REMOVE ESTE CUBO E SOLTA TODOS OS QUE ESTÃO CONECTADOS A ELE HORIZONTALMENTE
	public void Remove (string axis)
	{
		if (axis == "vertical")
		{
			for (int i = 0; i < VerticalTwins.Count; i++)
			{
				VerticalTwins[i].SetActive (false); //OCULTA TODOS GEMEOS VERTICAIS
				Game.score += 25; //ADICIONA PONTOS AO SCORE
			}
			VerticalTwins.Clear (); //LIMPA A LISTA DE GEMEOS VERTICAIS
			VerticalConnections.Clear (); //LIMPA A LISTA DE CONEXOES VERTICAIS
		}
		if (axis == "horizontal")
		{
			for (int i = 0; i < HorizontalTwins.Count; i++)
			{
				HorizontalTwins[i].SetActive (false); //OCULTA TODOS OS GEMEOS HORIZONTAIS
				Game.score += 50; //ADICIONA PONTOS AO SCORE
			}
			HorizontalTwins.Clear (); //LIMPA A LISTA DE GEMEOS HORIZONTAIS
		}
		gameObject.SetActive (false); //OCULTA ESTE CUBO


		MainCamera.animations.Play (MainCamera.animationName[color]); //RODA A ANIMAÇAO DE MUDANÇA DE COR DO BACKGROUND

		Audio.manager.Play ("line"); //TOCA O SOM DE "LINHA"

		//REMOVE UM CUBO PRETO (4)
		LevelGenerator.RefreshBlackList ();

		//ACELERA O JOGO
		if (Game.score > 199)
			speed = .375f;
		if (Game.score > 399)
			speed = .35f;
		if (Game.score > 599)
			speed = .325f;
		if (Game.score > 799)
			speed = .3f;
		if (Game.score > 999)
			speed = .275f;
		if (Game.score > 1299)
			speed = .25f;
		if (Game.score > 1599)
			speed = .225f;
		if (Game.score > 1799)
			speed = .2f;
		if (Game.score > 1999)
			speed = .175f;
		if (Game.score > 2299)
			speed = .15f;
	}
	
	void OnDisable ()
	{
		for (int i = 0; i < VerticalConnections.Count; i++)
		{
			VerticalConnections[i].GetComponent<Cube>().moveState = 1; //FAZ DESCER AS CONEXOES VERTICAIS
		}
	
	}

}
