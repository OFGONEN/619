﻿/*
Created By OFGONEN
*/
using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;
using System;

public class InterstitialAdd : MonoBehaviour {

	#region Variables
	public static InterstitialAdd instance = null;

	private InterstitialAd interstitial;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}



	private void Start()
	{
			RequestInterstitial();	
	}

	#region Methods
	private void RequestInterstitial()
	{
		
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-3940256099942544/1033173712";

		#endif

			// Initialize an InterstitialAd.
			interstitial = new InterstitialAd( adUnitId );

			AdRequest request = new AdRequest.Builder().AddTestDevice( SystemInfo.deviceUniqueIdentifier ).Build();
			// Load the interstitial with the request.
			interstitial.LoadAd( request );

			// Called when an ad request has successfully loaded.
			interstitial.OnAdLoaded += HandleOnAdLoaded;
			// Called when an ad request failed to load.
			interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
			// Called when an ad is shown.
			interstitial.OnAdOpening += HandleOnAdOpened;
			// Called when the ad is closed.
			interstitial.OnAdClosed += HandleOnAdClosed;
			// Called when the ad click caused the user to leave the application.
			interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

	}



	 public void ShowAdd()
     {
		if( interstitial.IsLoaded() )
		{
			interstitial.Show();
		}
	 }  

	

	public void HandleOnAdLoaded( object sender, EventArgs args )
	{
		MonoBehaviour.print( "HandleAdLoaded event received" );
	}

	public void HandleOnAdFailedToLoad( object sender, AdFailedToLoadEventArgs args )
	{
		MonoBehaviour.print( "HandleFailedToReceiveAd event received with message: "
							+ args.Message );
	}

	public void HandleOnAdOpened( object sender, EventArgs args )
	{
		MonoBehaviour.print( "HandleAdOpened event received" );
	}

	public void HandleOnAdClosed( object sender, EventArgs args )
	{
		interstitial.Destroy();
	}

	public void HandleOnAdLeavingApplication( object sender, EventArgs args )
	{
		MonoBehaviour.print( "HandleAdLeavingApplication event received" );
	}




	#endregion

}
