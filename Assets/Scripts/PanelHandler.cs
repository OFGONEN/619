/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour {

	#region Variables
	public static PanelHandler instance = null;

	public GameObject panel_menu;
	public GameObject panel_pause;
	public GameObject panel_endgame;
	public Text       text_winner_name; 
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}


	#region Methods
	public void MenuPanel(bool open )
	{
		ToogleMouse( !open );
		ToogleRotate( panel_menu );
		panel_menu.SetActive( open );
	}

	public void PausePanel(bool open)
	{
		ToogleMouse( !open );
		ToogleRotate( panel_pause );
		panel_pause.SetActive( open );
	}

	public void EndGamePanel(bool open)
	{
		ToogleMouse( !open );
		ToogleRotate(panel_endgame);
		WinnerName();
		panel_endgame.SetActive( open );
	}

	void ToogleMouse(bool open)
	{
		Mouse.instance.gameObject.SetActive( open );
	}

	void ToogleRotate( GameObject panel)
	{
		if( GameLogic.instance.GetCurrentPlayer() == 1 )
			panel.transform.localRotation = Quaternion.Euler(0,0,0);
		else
			panel.transform.localRotation = Quaternion.Euler( 0, 0, 180 );
	}

	public void WinnerName()
	{
		int player = GameLogic.instance.GetWinner();

		if( player == 1 )
			text_winner_name.text = PlayerPrefs.GetString( "Player1 Name" );
		else if( player == 2 )
			text_winner_name.text = PlayerPrefs.GetString( "Player2 Name" );
		else
			text_winner_name.text = "NONE";

	}

	public void ToogleBackButton(bool open)
	{
		BackButton.instance.enabled = open;
	}
	#endregion
	
	}
