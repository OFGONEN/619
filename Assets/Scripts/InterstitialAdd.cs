/*
Created By OFGONEN
*/
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAdd : MonoBehaviour {

	#region Variables
	public static InterstitialAdd instance = null;

	private InterstitialAd interstitial;

	public int time_to_check_ad;
	public int time_to_check_internet;

	public bool have_interstitial;
	private bool  can_show_ad;
	private float time_for_add;
	private float time_for_internet;
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
		//RequestInterstitial();
		time_for_add = 0;
		time_for_internet = 0;
	}

	//private void Update()
	//{

	//	if(!can_show_ad &&  Time.time - time_for_add > time_to_check_ad )
	//	{
	//		Debug.Log( "canshowad = true" );
	//		can_show_ad = true;
	//	}

	//	if( Time.time - time_for_internet > time_to_check_internet )
	//	{
	//		time_for_internet = Time.time;

	//		if( !have_interstitial && Application.internetReachability != NetworkReachability.NotReachable )
	//		{
	//			RequestInterstitial();
	//		}
	//	}

		
	//}

	#region Methods
	private void RequestInterstitial()
	{

		#if UNITY_ANDROID
           string adUnitId = "ca-app-pub-6236652016171676/9107981258";
		#elif UNITY_IOS
		   string adUnitId = "ca-app-pub-6236652016171676/5720088942";
		#else
		   string adUnitId = "unexpected_platform";
		#endif

	

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd( adUnitId );

			AdRequest request = new AdRequest.Builder().Build();
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



	 public void ShowAdd() // Burda Timer ' ile kontrol etmem gerek 
     {
		if( can_show_ad &&  have_interstitial && interstitial.IsLoaded() )
		{
			interstitial.Show();
			time_for_add = Time.time;
			can_show_ad = false;
		}
	 }
	
	 public void ShowAddInsta()
	 {
		if( have_interstitial && interstitial.IsLoaded() )
		{
			interstitial.Show();
			time_for_add = Time.time;
			can_show_ad = false;
		}
	 }

	public void DestroyAd()
	{
		interstitial.Destroy();
		have_interstitial = false;
	}

	public void HandleOnAdLoaded( object sender, EventArgs args )
	{
		have_interstitial = true;
		
	}

	public void HandleOnAdFailedToLoad( object sender, AdFailedToLoadEventArgs args )
	{
		DestroyAd();
	}

	public void HandleOnAdOpened( object sender, EventArgs args )
	{
		MonoBehaviour.print( "HandleAdOpened event received" );
	}

	public void HandleOnAdClosed( object sender, EventArgs args )
	{
		DestroyAd();
	}

	public void HandleOnAdLeavingApplication( object sender, EventArgs args )
	{
		MonoBehaviour.print( "HandleAdLeavingApplication event received" );
	}




	#endregion

}
