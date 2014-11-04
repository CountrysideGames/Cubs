using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {


	//ESTA CLASSE COLOCA NOVOS CUBOS NA CENA//


	public Transform cubePrefab; //PREFAB DO CUBO PADRÃO
	public Color[] cubeColor = new Color[4]; //CORES QUE O CUBO PODE TER
	public float[] cubeX = new float[] {2.7f, -1.35f, 0, 1.35f}; //PÓSIÇÕES EM QUE OS CUBOS PODEM SER COLOCADOS

	public static List <GameObject> BlackList = new List<GameObject> ();

	private int cubeCount = 0; //CONTAGEM DOS CUBOS CRIADOS
	private float delay = 2;

	private int randomColor = 0;
	private int lastColor = 0; //grava a ultima cor sorteada


	void Start ()
	{
		BlackList.Clear ();
		InvokeRepeating ("CreateCube", delay, delay);
	}

	//COLOCA UM NOVO CUBO NA CENA
	void CreateCube ()
	{
		if (Game.time != 0)
		{
			//ADICIONA 1 NA CONTAGEM DE CUBOS
			cubeCount++;

			//salva a ultima cor sorteada
			lastColor = randomColor;

			//SORTEIA UMA COR
			randomColor = Random.Range (0, cubeColor.Length);

			//SE A COR SORTEADA FOR IGUAL A ULTIMA (lastColor), SORTEIA DE NOVO
			while (randomColor == lastColor)
				randomColor = Random.Range (0, cubeColor.Length);

			//SE FOR CUBO PRETO E JA HOUVER MAIS QUE 2, SORTEIA DE NOVO, PARA DIMINUIR A CHANCE
			if (randomColor == 4 && BlackList.Count > 2)
				Random.Range (0, cubeColor.Length);

			//INSTANCIA O CUBO
			var cube = Instantiate(cubePrefab, new Vector2 (cubeX [Random.Range (0, cubeX.Length)], 6.75f), Quaternion.identity) as Transform;

			//DEFINE O CUBO COMO FILHO DESTE GAMEOBJECT
			cube.parent = transform;

			//DEFINE O NUMERO DA COR DO CUBO
			cube.GetComponent<Cube>().color = randomColor;

			//COLORE O CUBO
			cube.GetComponent<SpriteRenderer>().color = cubeColor [randomColor];

			//DEFINE O NUMERO DO CUBO
			cube.GetComponent<Cube> ().number = cubeCount;

			//ADICIONA O NUMERO DO CUBO AO FIM DO NOME DELE
			cube.name = "Cube" + cubeCount;

			//SE FOR CUBO PRETO (4), ADICIONA ELE A BLACKLIST
			if (randomColor == 4)
				BlackList.Add (cube.gameObject);

		}
	}

	//REMOVE CUBOS DA BLACK LIST
	public static void RefreshBlackList ()
	{
		if (BlackList.Count > 0) //SE HOUVER CUBOS NA BLACKLIST
		{
			print ("Refreshing black list");
			BlackList[0].SetActive (false);
			BlackList.Remove (BlackList[0]); //REMOVE O PRIMEIRO
		}
	}
}
