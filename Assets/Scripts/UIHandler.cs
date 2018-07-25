/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	#region Variables
	public static UIHandler instance = null;


	public ImageChanger bar_option;
	public Button		button_option;
	public TextChanger  text_option;
	public TextChanger  text_player_name;

	public Text text_score_player1;
	public Text text_score_player2;

	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}


	#region Methods
	public void UpdateScore(int player , int score)
	{
		if(player == 1 )
		{
			text_score_player1.text = "" + score;
		}
		else
		{
			text_score_player2.text = "" + score;
		}
	}

	void ToggleOptionBar(bool open )
	{
		if( open )
		{
			bar_option.ChangeToNegative();
			button_option.gameObject.SetActive( true );
			text_option.gameObject.SetActive( true );
			text_player_name.gameObject.SetActive( false );
		}
		else
		{
			bar_option.ChangeToNormal();
			button_option.gameObject.SetActive( false );
			text_option.gameObject.SetActive( false );
			text_player_name.gameObject.SetActive( true );
		}
	}

	public void ChangePlayerName(bool player)
	{
		if( player )
			text_player_name.ChangeToNormal();
		else
			text_player_name.ChangeToNegative();
	}

	void ChangeOptionText(bool endturn)
	{
		if( endturn )
			text_option.ChangeToNormal();
		else
			text_option.ChangeToNegative();
	}

	public void ChangeToOption(bool endturn)
	{
		ChangeOptionText( endturn );
		ToggleOptionBar( true );
	}

	public void ChangeToPlayerName(bool player)
	{
		ChangePlayerName( player );
		ToggleOptionBar( false );
	}
	#endregion

}
