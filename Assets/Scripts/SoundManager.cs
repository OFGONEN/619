/*
Created By OFGONEN
*/
using UnityEngine;

public class SoundManager : MonoBehaviour {

	#region Variables
	public static SoundManager instance = null;
	public AudioSource source;
	public AudioClip[] effects;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		DontDestroyOnLoad( gameObject );
	}

	void Start () 
	{
		int number = PlayerPrefs.GetInt( "Music" );
		if( number == 1 )
			source.Play();
	}

	public void StartMusic()
	{
		source.Play();
	}

	public void StopMusic()
	{
		source.Stop();
	}

	public void PlayEffect(int effect)
	{
		if( 1 == PlayerPrefs.GetInt( "Sound" ) )
		{
			source.PlayOneShot( effects[ effect ], 1 );
		}
	}
	
	#region Methods
	#endregion
	
	}
