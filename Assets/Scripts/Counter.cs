/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	#region Variables
	public static Counter instance = null;
	public Text counterText;

	public float time_normal_round;
	public float time_upsideDown_round;
	public float time_normal_toAdd;
	public float time_upsideDown_toAdd;
	public float time_upsideDown_comboAdd;

	private float counter;
	private bool canCounter;

	private bool played_sound_effect;
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
			if( (int)counter == 3)
			{
				if( !played_sound_effect )
				{
					SoundEffectPlayer.instance.LastSecondsSound();
					Debug.Log( "3" );
					played_sound_effect = true;
				}
			}
			else if((int)counter == 2)
			{
				if( played_sound_effect )
				{
					SoundEffectPlayer.instance.LastSecondsSound();
					Debug.Log( "2" );
					played_sound_effect = false;
				}
			}
			else if((int)counter == 1)
			{
				if( !played_sound_effect )
				{
					SoundEffectPlayer.instance.LastSecondsSound();
					Debug.Log( "1" );
					played_sound_effect = true;
				}
			}


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
			counter += time_upsideDown_toAdd;
		else
			counter += time_normal_toAdd;
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
		played_sound_effect = false;
	}

	public void GoUpsideDown()
	{
		counter = time_upsideDown_round;
		played_sound_effect = false;
	}
	#endregion

}
