/*
Created By OFGONEN
*/
using UnityEngine;

public class PanelHandler : MonoBehaviour {

	#region Variables
	public static PanelHandler instance = null;

	public GameObject panel_menu;
	public GameObject panel_pause;
	public GameObject panel_endgame;
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
		panel_menu.SetActive( open );
	}

	public void PausePanel(bool open)
	{
		panel_pause.SetActive( open );
	}

	public void EndGamePanel(bool open)
	{
		panel_endgame.SetActive( open );
	}
	#endregion
	
	}
