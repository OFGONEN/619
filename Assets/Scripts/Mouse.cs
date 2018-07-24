/*
Created By OFGONEN
*/
using UnityEngine;

public class Mouse : MonoBehaviour {

	#region Variables
	public static Mouse instance = null;

	private RaycastHit2D hitInfo;

	
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}

	
	
	void Update () 
	{
		
	}
	
	#region Methods
	#endregion
	
	}
