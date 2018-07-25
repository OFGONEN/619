/*
Created By OFGONEN
*/
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour {

	#region Variables
	public static UIAnimationHandler instance = null;

	public Animator anim_camera;
	public Animator anim_board;
	public Animator anim_board_score;
	public Animator anim_board_option;
	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}


	#region Methods
	public void EndTurn(int player)
	{
		anim_board.SetTrigger( "Player" + player + "EndTurn" );
		anim_board_option.SetTrigger("player" + player + "Start");
		anim_board_score.SetTrigger("player" + player + "Start");
	}

	public void CameraToogle(bool white)
	{
		if( white )
			anim_camera.SetTrigger( "Go_White" );
		else
			anim_camera.SetTrigger( "Go_Black" );
	}

	public void GoUpsideDown( int player )
	{
		Counter.instance.TooglePause( 0 );
		Counter.instance.GoUpsideDown();
		anim_board.SetTrigger( "Player" + player + "GoUpsideDown" );
		UIAnimationHandler.instance.CameraToogle( false );
	}

	#endregion

}
