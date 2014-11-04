using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class ButtonStart : MonoBehaviour {

	public GameObject HUD;
	public Game game;


	public Audio audioManager;



	void OnClick ()
	{
		if (Advertisement.isReady ())
		{
			print ("Showing Unity Ads");
			audioManager.Play ("line");
			Advertisement.Show (); //MOSTRA ADS DO UNITY ADS
		}
		else if (Chartboost.hasCachedInterstitial ())
		{
			print ("Showing Chartboost Ads");
			audioManager.Play ("hit");
			Chartboost.showInterstitial (); //MOSTRA ADS DO CHARTBOOST
		}
		else
		{	
			audioManager.Play ("countryside");
			AdBuddizBinding.ShowAd();
		}

		game.StartGame ();

		HUD.SetActive (true);
		transform.parent.gameObject.SetActive (false);
	}

}
