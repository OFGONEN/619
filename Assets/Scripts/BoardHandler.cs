/*
Created By OFGONEN
*/
using UnityEngine;

public class BoardHandler : MonoBehaviour {

	#region Variables
	public static BoardHandler instance = null;
	public Animator anim;


	public Sprite[] sprites_cells;
	public Sprite[] sprites_numbers_white;
	public Sprite[] sprites_numbers_black;


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
		if( white )
		{
			cell.transform.GetChild( 0 ).GetComponent<SpriteRenderer>().sprite = sprites_numbers_white[ number ];
		}
		else
		{
			cell.transform.GetChild( 0 ).GetComponent<SpriteRenderer>().sprite = sprites_numbers_black[ number ];
		}
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
