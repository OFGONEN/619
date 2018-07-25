/*
Created By OFGONEN
*/
using UnityEngine;

public class BoardHandler : MonoBehaviour {

	#region Variables
	public static BoardHandler instance = null;
	public Animator anim;


	public Sprite[] sprites_cells;
	public Sprite[] sprites_numbers;


	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}


	#region Methods
	public void ChangeCellSprite(GameObject cell , int number)
	{
		cell.GetComponent<SpriteRenderer>().sprite = sprites_cells[ number ];
	}

	public void ChangeNumberSprite(GameObject cell , int number , bool white)
	{
		if( number == 6 )
			number = 0;
		else if( number == 9 )
			number = 2;


		if( white )
			number += 3;

		cell.transform.GetChild( 0 ).GetComponent<SpriteRenderer>().sprite = sprites_numbers[ number ];
	}

	

	public void UpsideDowned()
	{
		Counter.instance.TooglePause( 1 );
	}

	public void EndUpsideDown( int player ) 
	{
		anim.SetTrigger( "Player" + player + "GoNormal" );
		UIAnimationHandler.instance.CameraToogle( true );
	}

	public void EndTurn( int player ) 
	{
		UIAnimationHandler.instance.EndTurn( player );
	}

	#endregion

}
