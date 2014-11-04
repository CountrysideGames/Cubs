using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using Prime31;

public class Game : MonoBehaviour {


	public static float time = 1;
	public static int score = 0;

	public GameObject baseGo; //GameObject da base
	public GameObject scoreGo; //GameObject da base
	public static Transform cubeBase; //base controlavel pelo jogador, q armazena os cubos
	
	public Transform levelGenerator;


	void Awake ()
	{
#if UNITY_ANDROID
		Advertisement.Initialize ("19226");
#endif
#if UNITY_IOS
		Advertisement.Initialize ("19233");
#endif
		Chartboost.init ("54580ae904b0165fa321c7e4", "0eff6be6ffe736418eaab9fb89c52b7bf88bc77e", "54580f120d60254b6f01c190", "59a6cb1cbab8773f277355d20422955f321104a7");
		AdBuddizBinding.SetAndroidPublisherKey ("f7f1e5e2-2881-40a4-bfe7-0bf24450c5f3");
		AdBuddizBinding.SetIOSPublisherKey ("58a528e8-00f3-4c80-ae25-e2d3f1e0a574");
	}

	void Start ()
	{
		cubeBase = baseGo.transform;

		Chartboost.cacheInterstitial ();
	}


	//INICIA O JOGO
	public void StartGame()
	{
		//RESETA O TEMPO
		time = 1;
		
		//RESETA O SCORE
		score = 0;
		
		//LIMPA A BLACKLIST DO LEVEL GENERATOR
		LevelGenerator.RefreshBlackList ();

		//DESTROI TODOS OS FILHOS DO LEVEL GENERATOR
		for (int i = 0; i < levelGenerator.transform.childCount; i++)
			Destroy (levelGenerator.transform.GetChild (i).gameObject);
		
		//DESTROI TODOS OS FILHOS DA BASE
		for (int i = 0; i <  cubeBase.childCount; i++)
			Destroy (cubeBase.GetChild (i).gameObject);

		//MOSTRA A BASE
		baseGo.SetActive (true);

		//POSICIONA A BASE NO CENTRO DA TELA
		baseGo.transform.position = new Vector2 (-0.6769109f, -5.4f);

		//PERMITE CONTROLAR A BASE
		baseGo.GetComponent<Base> ().canMove = true;
	}

	public static void GameOver ()
	{
		cubeBase.GetComponent<Base> ().canMove = false; //IMPEDE A BASE DE MOVER

		time = 0; //DEFINE O RITMO DE JOGO COMO ZERO (0)
		print ("Game Over");
	}


}
