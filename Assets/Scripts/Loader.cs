/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	#region Variables
	public static Loader instance = null;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}


	// Burayi yayinlamadan once 1 kere calistir:DD 

	private void Start()
	{
		if( PlayerPrefs.GetInt( "Start" ) == 0 )
		{
			PlayerPrefs.SetInt( "Start", 1 );
			PlayerPrefs.SetInt( "Sound", 1 );
			PlayerPrefs.SetInt( "Music", 1 );
			PlayerPrefs.SetInt( "Table Size", 0 );
			PlayerPrefs.SetString( "Player1 Name", "PLAYER1" );
			PlayerPrefs.SetString( "Player2 Name", "PLAYER2" );
		}
	}

	#region Methods
	public void LoadMenu()
	{
		SceneManager.LoadScene( 0 );
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene( 1 );
	}

	public void LoadHow2Play()
	{
		SceneManager.LoadScene( 2 );
	}

	public void LoadOptions()
	{
		SceneManager.LoadScene( 3 );
	}

	public void LoadWtfWeAre()
	{
		SceneManager.LoadScene( 4 );
	}

	#endregion

}
