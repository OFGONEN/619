/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {

	#region Variables
	public static OptionsHandler instace = null;

	[Header("Avaliable Buttons")]
	public bool soundControl;
	public bool musicControl;
	public bool tableSizeControl;
	public bool playerNamesControl;


	[Header("Buttons")]
	public ImageChanger soundOn;
	public ImageChanger soundOff;
	public ImageChanger musicOn;
	public ImageChanger musicOff;
	public ImageChanger sizeMin;
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
			PlayerPrefs.SetString( bufferPrefName, keyboard.text.ToUpper() );
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
		if(soundControl)
		{
			HandleSound();
		}

		if( musicControl )
		{
			HandleMusic();
		}

		if( tableSizeControl )
		{
			HandleTableSize();

		}

		if( playerNamesControl )
		{
			HandlePlayerNames();

		}
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
			sizeMin.ChangeToNegative();
		}
		else if( tableSize == 1 )
		{
			sizeSmall.ChangeToNegative();
		}
		else if( tableSize == 2 )
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
			player1.text = "Player1";
		}
		else
		{
			player1.text = player1Name;
		}

		if( player2Name == "default" || player1Name == "" )
		{
			player2.text = "Player2";
		}
		else
		{
			player2.text = player2Name;
		}
	}
	#endregion

	#region ButtonActions 
	public void OpenKeyboardPlayer1()
	{
		player1Name = PlayerPrefs.GetString( "Player1 Name" );
		keyboard = TouchScreenKeyboard.Open( player1Name, TouchScreenKeyboardType.Default );
		bufferText = player1;
		bufferPrefName = "Player1 Name";
	}

	public void OpenKeyboardPlayer2()
	{
		player2Name = PlayerPrefs.GetString( "Player2 Name" );
		keyboard = TouchScreenKeyboard.Open( player2Name, TouchScreenKeyboardType.Default );
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
		SoundManager.instance.StartMusic();
	}

	public void ChangeMusicOff()
	{
		musicOn.ChangeToNormal();
		musicOff.ChangeToNegative();
		PlayerPrefs.SetInt( "Music", 0 );
		SoundManager.instance.StopMusic();
	}

	public void ChangeSizeMin()
	{
		sizeMin.ChangeToNegative();
		sizeSmall.ChangeToNormal();
		sizeMedium.ChangeToNormal();
		sizeBig.ChangeToNormal();
		PlayerPrefs.SetInt( "Table Size", 0 );
	}

	public void ChangeSizeSmall()
	{
		sizeMin.ChangeToNormal();
		sizeSmall.ChangeToNegative();
		sizeMedium.ChangeToNormal();
		sizeBig.ChangeToNormal();
		PlayerPrefs.SetInt( "Table Size", 1 );
	}

	public void ChangeSizeMedium()
	{
		sizeMin.ChangeToNormal();
		sizeSmall.ChangeToNormal();
		sizeMedium.ChangeToNegative();
		sizeBig.ChangeToNormal();
		PlayerPrefs.SetInt( "Table Size", 2 );
	}

	public void ChangeSizeBig()
	{
		sizeMin.ChangeToNormal();
		sizeSmall.ChangeToNormal();
		sizeMedium.ChangeToNormal();
		sizeBig.ChangeToNegative();
		PlayerPrefs.SetInt( "Table Size", 3 );
	}
	#endregion 

	

	#endregion

}
