/*
Created By OFGONEN
*/
using UnityEngine;

public class BackButton : MonoBehaviour {

	public delegate void BackButtonFunction();

	public enum FunctionName
	{
		Pause, LoadMenu, QuitApplication,
	}

	#region Variables
	public static BackButton instance;

	BackButtonFunction f;
	
	public FunctionName function;
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
		 f = funtions[ ( int )function ];
	}

	private void Update()
	{
		if( Input.GetKey( KeyCode.Escape ) )
		{
			f();
		}
	}

	#region Methods
	static BackButtonFunction[] funtions =
	{
		Pause , LoadMenu  , QuitApplication
	};

	static void LoadMenu()
	{
		Loader.instance.LoadMenu();
	}

	static void Pause()
	{
		Counter.instance.TooglePause( 0 );
		PanelHandler.instance.MenuPanel( true );
	}

	static void QuitApplication()
	{
		Application.Quit();
	}
	#endregion

}
