/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

	#region Variables
	public Text text;
	public bool options;
	string name_player1;
	string name_player2;
	#endregion
	
	
	void Start () 
	{
		name_player1 = PlayerPrefs.GetString( "Player1 Name" );
		name_player2 = PlayerPrefs.GetString( "Player2 Name" );
		if( !options )
			text.text = name_player1;
	}
	
	
	
	#region Methods
	public void ChangeToNormal()
	{
		if( options )
		{
			text.text = "END TURN!";
		}
		else
		{
			text.text = name_player1;
		}
	}

	public void ChangeToNegative()
	{
		if( options )
		{
			text.text = "UPSIDEDOWN!";
		}
		else
		{
			text.text = name_player2;
		}
	}
	#endregion
	
	}
