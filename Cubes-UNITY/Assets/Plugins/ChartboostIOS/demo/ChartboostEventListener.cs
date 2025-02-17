using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ChartboostEventListener : MonoBehaviour
{
#if UNITY_IPHONE

	void OnEnable()
	{
		// Listen to all events for illustration purposes
		ChartboostManager.didCacheInterstitialEvent += didCacheInterstitialEvent;
		ChartboostManager.didFailToCacheInterstitialEvent += didFailToLoadInterstitialEvent;
		ChartboostManager.didFinishInterstitialEvent += didFinishInterstitialEvent;

		ChartboostManager.didCacheMoreAppsEvent += didCacheMoreAppsEvent;
		ChartboostManager.didFailToCacheMoreAppsEvent += didFailToLoadMoreAppsEvent;
		ChartboostManager.didFinishMoreAppsEvent += didFinishMoreAppsEvent;

		ChartboostManager.didCacheRewardedVideoEvent += didCacheRewardedVideoEvent;
		ChartboostManager.didFailToLoadRewardedVideoEvent += didFailToLoadRewardedVideoEvent;
		ChartboostManager.didFinishRewardedVideoEvent += didFinishRewardedVideoEvent;
		ChartboostManager.didCompleteRewardedVideoEvent += didCompleteRewardedVideoEvent;
	}


	void OnDisable()
	{
		// Remove all event handlers
		ChartboostManager.didCacheInterstitialEvent -= didCacheInterstitialEvent;
		ChartboostManager.didFailToCacheInterstitialEvent -= didFailToLoadInterstitialEvent;
		ChartboostManager.didFinishInterstitialEvent -= didFinishInterstitialEvent;

		ChartboostManager.didCacheMoreAppsEvent -= didCacheMoreAppsEvent;
		ChartboostManager.didFailToCacheMoreAppsEvent -= didFailToLoadMoreAppsEvent;
		ChartboostManager.didFinishMoreAppsEvent -= didFinishMoreAppsEvent;

		ChartboostManager.didCacheRewardedVideoEvent -= didCacheRewardedVideoEvent;
		ChartboostManager.didFailToLoadRewardedVideoEvent -= didFailToLoadRewardedVideoEvent;
		ChartboostManager.didFinishRewardedVideoEvent -= didFinishRewardedVideoEvent;
		ChartboostManager.didCompleteRewardedVideoEvent -= didCompleteRewardedVideoEvent;
	}



	void didCacheInterstitialEvent( string location )
	{
		Debug.Log( "didCacheInterstitialEvent: " + location );
	}


	void didFailToLoadInterstitialEvent( string location )
	{
		Debug.Log( "didFailToLoadInterstitialEvent: " + location );
	}


	void didFinishInterstitialEvent( string location, string reason )
	{
		Debug.Log( "didFinishInterstitialEvent. Location: " + location + ", reason: " + reason );
	}



	void didCacheMoreAppsEvent(  string location )
	{
		Debug.Log( "didCacheMoreAppsEvent: " + location );
	}


	void didFailToLoadMoreAppsEvent(  string location )
	{
		Debug.Log( "didFailToLoadMoreAppsEvent: " + location );
	}


	void didFinishMoreAppsEvent(  string location, string reason )
	{
		Debug.Log( "didFinishMoreAppsEvent. Location: " + location + ", reason: " + reason );
	}



	void didCacheRewardedVideoEvent( string location )
	{
		Debug.Log( "didCacheRewardedVideoEvent: " + location );
	}


	void didFailToLoadRewardedVideoEvent( string location )
	{
		Debug.Log( "didFailToLoadRewardedVideoEvent: " + location );
	}


	void didFinishRewardedVideoEvent( string location, string reason )
	{
		Debug.Log( "didFinishRewardedVideoEvent. Location: " + location + ", reason: " + reason );
	}


	void didCompleteRewardedVideoEvent( int reward )
	{
		Debug.Log( "didCompleteRewardedVideoEvent. Reward: " + reward );
	}

	#endif
}


