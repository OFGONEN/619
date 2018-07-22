/*
Created By OFGONEN
*/
using UnityEngine;

public class PanelHandler : MonoBehaviour {

	#region Variables
	public GameObject menuPanel;
	public GameObject pausePanel;
	#endregion
	
	
	#region Methods
	public void MenuPanel(bool open )
	{
		menuPanel.SetActive( open );
	}

	public void PausePanel(bool open)
	{
		pausePanel.SetActive( open );
	}
	#endregion
	
	}
