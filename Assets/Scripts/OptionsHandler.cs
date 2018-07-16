﻿/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {

	#region Variables
	public static OptionsHandler instace = null;

	[Header("Buttons")]
	public ImageChanger soundOn;
	public ImageChanger soundOff;
	public ImageChanger musicOn;
	public ImageChanger musicOff;
	public ImageChanger sizeSmall;
	public ImageChanger sizeMedium;
	public ImageChanger sizeBig;

	[Header("TextFields")]
	public Text player1;
	public Text player2;
	private Text bufferText;


	private int sound;
	private int music;
	private int tableSize;
	private string player1Name;
	private string player2Name;
	private string bufferPrefName;

	private TouchScreenKeyboard keyboard;
	#endregion


	private void Awake()
	{
		if( instace == null )
			instace = this;
		else if( instace != this )
			Destroy( gameObject );
	}

	private void Start()
	{
		GetPlayerPref();
		HandleButtons();
	}

	private void Update()
	{
		if(keyboard != null && keyboard.active )
		{
			bufferText.text = keyboard.text.ToUpper();
			PlayerPrefs.SetString( bufferPrefName, keyboard.text );
		}
	}

	#region Methods
	private void GetPlayerPref()
	{
		sound = PlayerPrefs.GetInt( "Sound" );
		music = PlayerPrefs.GetInt( "Music" );
		tableSize = PlayerPrefs.GetInt( "Table Size" );
		player1Name = PlayerPrefs.GetString( "Player1 Name" );
		player2Name = PlayerPrefs.GetString( "Player2 Name" );
	}



	#region HandleButtons
	private void HandleButtons()
	{
		HandleSound();
		HandleMusic();
		HandleTableSize();
		HandlePlayerNames();
	}

	private void HandleSound()
	{
		if( sound == 0 )
		{
			soundOff.ChangeToNegative();
		}
		else
		{
			soundOn.ChangeToNegative();
		}
	}

	private void HandleMusic()
	{
		if( music == 0 )
		{
			musicOff.ChangeToNegative();
		}
		else
		{
			musicOn.ChangeToNegative();
		}
	}

	private void HandleTableSize()
	{
		if( tableSize == 0 )
		{
			sizeSmall.ChangeToNegative();
		}
		else if( tableSize == 1 )
		{
			sizeMedium.ChangeToNegative();
		}
		else
		{
			sizeBig.ChangeToNegative();
		}
	}

	private void HandlePlayerNames()
	{
		if( player1Name == "default" || player1Name == "" )
		{
			player1.text = "PLAYER1";
		}
		else
		{
			player1.text = player1Name;
		}

		if( player2Name == "default" || player1Name == "" )
		{
			player2.text = "PLAYER2";
		}
		else
		{
			player2.text = player2Name;
		}
	}
	#endregion

	// Suan sadece GUI degistirme var , logic eklemek gerek , SoundManager'dan sonra ekleriz 
	#region ButtonActions 
	public void OpenKeyboardPlayer1()
	{
		keyboard = TouchScreenKeyboard.Open( "Player1", TouchScreenKeyboardType.Default );
		bufferText = player1;
		bufferPrefName = "Player1 Name";
	}

	public void OpenKeyboardPlayer2()
	{
		keyboard = TouchScreenKeyboard.Open( "Player2", TouchScreenKeyboardType.Default );
		bufferText = player2;
		bufferPrefName = "Player2 Name";
	}

	public void ChangeSoundOn()
	{
		soundOn.ChangeToNegative();
		soundOff.ChangeToNormal();
		PlayerPrefs.SetInt( "Sound", 1 );
	}

	public void ChangeSoundOff()
	{
		soundOn.ChangeToNormal();
		soundOff.ChangeToNegative();
		PlayerPrefs.SetInt( "Sound", 0 );
	}

	public void ChangeMusicOn()
	{
		musicOn.ChangeToNegative();
		musicOff.ChangeToNormal();
		PlayerPrefs.SetInt( "Music", 1 );
	}

	public void ChangeMusicOff()
	{
		musicOn.ChangeToNormal();
		musicOff.ChangeToNegative();
		PlayerPrefs.SetInt( "Music", 0 );
	}

	public void ChangeSizeSmall()
	{
		sizeSmall.ChangeToNegative();
		sizeMedium.ChangeToNormal();
		sizeBig.ChangeToNormal();
		PlayerPrefs.SetInt( "Table Size", 0 );
	}

	public void ChangeSizeMedium()
	{
		sizeSmall.ChangeToNormal();
		sizeMedium.ChangeToNegative();
		sizeBig.ChangeToNormal();
		PlayerPrefs.SetInt( "Table Size", 1 );
	}

	public void ChangeSizeBig()
	{
		sizeSmall.ChangeToNormal();
		sizeMedium.ChangeToNormal();
		sizeBig.ChangeToNegative();
		PlayerPrefs.SetInt( "Table Size", 2 );
	}
	#endregion 

	

	#endregion

}