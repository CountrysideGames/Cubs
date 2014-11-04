using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeFeeler : MonoBehaviour {


	//ESTA CLASSE CONTROLA O FUNCIONAMENTO DOS TRIGGERS(FEELERS) DO CUBO//


	public int position; //POSIÇÃO DESTE FEELER (CIMA/BAIXO, ESQUERDA/DIREITA)
	private Transform cubeParent; //PAI (CUBO) DESTE FEELER
	private Cube cubeController; //CONTROLADOR DO CUBO PAI DESTE FEELER
	
	private int colLayer; //LAYER DO OBJETO COLIDIDO (COL)
	private Transform colTransform; //TRANSFORM DO OBJETO COLIDIDO (COL)
	private Cube colCubeController; //CONTROLADOR DO CUBO PAI DO OBJETO COLIDIDO (COL)


	void OnEnable ()
	{
		cubeParent = transform.parent;
		cubeController = cubeParent.GetComponent<Cube>();
	}
	
	void OnTriggerEnter (Collider col)
	{
		colLayer = col.gameObject.layer;
		colTransform = col.transform;

		if (colLayer == 9) //SE COLIDIR COM A BASE
		{
			if (position == 1) //COLLIDER DE BAIXO
			{
				if (cubeController.moveState == 1)
				{
					cubeParent.parent = colTransform;
					cubeController.moveState = 0;
					
					//HABILITA OS FEELERS HORIZONTAIS
					cubeController.feeler [2].SetActive (true);
					cubeController.feeler [3].SetActive (true);

					Audio.manager.Play ("hit");
				}
			}
		}

		if (colLayer == 10) //SE COLIDIR COM A ENDLINE
		{
			cubeParent.gameObject.SetActive (false); //SOME

			if (Game.score > 50)
				Game.score -= 50;
		}

		if (colLayer == 8) //SE COLIDIR COM OUTRO CUBE
		{
			colCubeController = col.GetComponent<Cube> ();
			
			if (position == 4) //COLISAO NO NUCLEO/CENTRO DO CUBO
			{
				if (cubeController.color != 4) //SE O CUBO NAO FOR PRETO
				{
					if (cubeController.number > colCubeController.number) //SE ESTE CUBO FOR MAIS NOVO QUE O COLIDIDO
					{
						cubeParent.gameObject.SetActive (false); //ele fica oculto

						if (Game.score > 50) //TIRA 50 PONTOS
							Game.score -= 50;
					}
				}
				else //SE FOR PRETO
				{
					if (cubeController.number > colCubeController.number) //SE ESTE CUBO FOR MAIS NOVO QUE O COLIDIDO
						col.gameObject.SetActive (false); //ele fica oculto
				}
			}//ESTA EXCECAO SERVIU PARA IMPEDIR QUE CUBOS SE "INFILTREM" PELO CORREDOR LATERAL

			if (cubeController.moveState == 1) //SE ESTIVER CAINDO
			{
				if (position == 1) //COLISAO ABAIXO
				{
					colCubeController.VerticalConnections.Add (cubeParent.gameObject); //ADICIONA O CUBO A LISTA DE CONEXOES VERTICAIS DO CUBO COLIDIDO (COL)
				}

				if (position == 0 || position == 1) //COLISAO VERTICAL
				{
					if (colCubeController.color == cubeController.color && cubeController.color != 4) //SE FOR DA MESMA COR E NAO FORME PRETAS (4)
					{
						if (!colCubeController.VerticalTwins.Contains (cubeParent.gameObject)) //CONFERE SE O CUBO JA NAO ESTA NA LISTA DO CUBO COLIDIDO (COL)
							colCubeController.VerticalTwins.Add (cubeParent.gameObject); //ADICIONA NA LISTA DE GEMEOS VERTICAIS DO CUBO COLIDIDO (COL)

						if (!cubeController.VerticalTwins.Contains (col.gameObject)) //CONFERE SE O CUBO COLIDIDO (COL) JA NAO ESTA NA LISTA DESTE CUBO
							cubeController.VerticalTwins.Add (col.gameObject); //ADICIONA NA LISTA DE GEMEOS VERTICAIS DO CUBO
					}

					cubeParent.parent = Game.cubeBase; //TRANSFORMA O CUBO COLIDIDO (col) EM FILHO DA BASE (cubeBase)
					
					cubeController.moveState = 0; //IMPEDE QUE O CUBO SE MOVA
					colCubeController.moveState = 0; //IMPEDE QUE O CUBO COLIDIDO (COL) SE MOVA
					
					Audio.manager.Play ("hit");

					//HABILITA OS FEELERS HORIZONTAIS
					cubeController.feeler [2].SetActive (true);
					cubeController.feeler [3].SetActive (true);
				}
			}

			if (position == 2 || position == 3) //SE COLIDIR COM A LATERAL
			{
				if (cubeController.color == colCubeController.color && cubeController.color != 4) //E AS CORES FOREM IGUAIS E NAO FOREM PRETAS (4)
				{
					if (!colCubeController.HorizontalTwins.Contains (cubeParent.gameObject)) //CONFERE SE O CUBO JA NAO ESTA NA LISTA DO CUBO COLIDIDO (COL)
						colCubeController.HorizontalTwins.Add (cubeParent.gameObject); //ADICIONA NA LISTA DE GEMEOS HORIZONTAIS DO CUBO COLIDIDO (COL)

					if (!cubeController.HorizontalTwins.Contains (col.gameObject)) //CONFERE SE O CUBO COLIDIDO (COL) JA NAO ESTA NA LISTA DESTE CUBO
						cubeController.HorizontalTwins.Add (col.gameObject); //ADICIONA NA LISTA DE GEMEOS HORIZONTAIS DO CUBO COLIDIDO (COL)
				}
			}
		}

		
		if (colLayer == 11) //SE COLIDIR COM O GAME OVER
		{
			if (position == 2)
			{
				if (cubeController.moveState == 0)
					Game.GameOver ();
			}
		}
	}


	void OnTriggerExit (Collider col)
	{
		colLayer = col.gameObject.layer;
		colTransform = col.transform;

		if (colLayer == 8)
		{
			if (position == 2 || position == 3) //SE des-COLIDIR COM A LATERAL
			{
				if (cubeController.color == colCubeController.color) //E AS CORES FOREM IGUAIS
				{
					colCubeController.HorizontalTwins.Remove (cubeParent.gameObject); //REMOVE DA LISTA DE GEMEOS HORIZONTAIS DO CUBO COLIDIDO (COL)
					
					cubeController.HorizontalTwins.Remove (col.gameObject); //REMOVE DA LISTA DE GEMEOS HORIZONTAIS DO CUBO COLIDIDO (COL)
				}
			}
			
			if (position == 1) //des-COLISAO ABAIXO
			{
				cubeController.moveState = 1;
				
				//HABILITA OS FEELERS HORIZONTAIS
				cubeController.feeler [2].SetActive (false);
				cubeController.feeler [3].SetActive (false);
			}

			if (position == 0) //des-COLISAO ACIMA
			{
				cubeController.VerticalConnections.Remove (col.gameObject); //REMOVE O CUBO COLIDIDO (COL) DA LISTA DE COLISOES VERTICAIS DO CUBO
			}
		}
	}

}
