/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	#region Variables
	public static Counter instance = null;
	public Text counterText;

	public int time_normal_round;
	public int time_upsideDown_round;
	public float time_normal_toAdd;
	public float time_upsideDown_toAdd;
	public float time_upsideDown_comboAdd;

	private float counter;
	private bool canCounter;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}

	void Start () 
	{
		counter = time_normal_round;
		counterText.text = "" + ( int )counter;
		canCounter = true;
	}
	
	void Update () 
	{
		if(canCounter)
		{
			counter -= Time.deltaTime;
			counterText.text = "" + ( int )counter;
			if( counter <= 0.05f )
			{
				canCounter = false;
				GameLogic.instance.SetEndTurn( true );
				GameLogic.instance.EndTurn();
			}
		}
	}

	#region Methods
	public void Scored( bool inUpsideDown )
	{
		if( inUpsideDown )
			counter = time_upsideDown_toAdd;
		else
			counter = time_normal_toAdd;
	}

	public void TooglePause( int pause )
	{
		if( pause == 1 )
			canCounter = true;
		else
			canCounter = false;
	}

	public void EndOfRound()
	{
		counter = time_normal_round;
	}

	public void GoUpsideDown()
	{
		counter = time_upsideDown_round;
	}
	#endregion

}
