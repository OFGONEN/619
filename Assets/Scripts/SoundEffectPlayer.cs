/*
Created By OFGONEN
*/
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {

	#region Variables
	public static SoundEffectPlayer instance = null;
	public bool earnedUpsideDown;
	public bool lostUpsideDown;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}

	#region Methods
	public void MainButtonSound()
	{
		SoundManager.instance.PlayEffect( 0 );
	}

	public void NegativeButtonSound()
	{
		SoundManager.instance.PlayEffect( 1 );
	}

	public void CellTouchSound()
	{
		SoundManager.instance.PlayEffect( 2 );
	}

	public void OptionCellTouchSound()
	{
		SoundManager.instance.PlayEffect( 3 );
	}

	public void ScoredSound()
	{
		SoundManager.instance.PlayEffect( 4 );
	}

	public void EarnUpsideDownSound()
	{
		if( !earnedUpsideDown )
		{
			SoundManager.instance.PlayEffect( 5 );
			earnedUpsideDown = true;
		}
		else
			SoundManager.instance.PlayEffect( 4 );
	}

	public void LastSecondsSound()
	{
		SoundManager.instance.PlayEffect( 6 );
	}

	public void Neutralize()
	{
		earnedUpsideDown = false;
		lostUpsideDown = false;
	}


	#endregion

}
