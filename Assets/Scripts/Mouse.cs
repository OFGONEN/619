/*
Created By OFGONEN
*/
using UnityEngine;

public class Mouse : MonoBehaviour {

	#region Variables
	public static Mouse instance = null;
	public GameObject options;

	private RaycastHit2D hitInfo;
	private GameObject obj_selected;
	private GameObject obj_hit;

	int x , y;

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
		if( Input.GetMouseButtonDown( 0 ) )
		{
			hitInfo = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );
			if( hitInfo )
			{
				obj_hit = hitInfo.collider.gameObject;
				if( obj_selected != null )
				{
					if( obj_hit.tag == "option" )
					{
						obj_selected.transform.localScale = Vector2.one;
						BoardHandler.instance.ChangeCellSprite( obj_selected, 0 );
						options.SetActive( false );

						int number_put = ( int )obj_hit.name[ 0 ] - 48;

						if( GameLogic.instance.InUpsideDown() )
						{
							if(number_put == 1)
								BoardHandler.instance.ChangeNumberSprite( obj_selected, number_put, false );
							else
								BoardHandler.instance.ChangeNumberSprite( obj_selected, 15 - number_put, false );
						}
						else
							BoardHandler.instance.ChangeNumberSprite( obj_selected, number_put, false );

						GameLogic.instance.UpdateArrayNumber( number_put, x, y );

						obj_hit = null;
						obj_selected = null;
						x = -1;
						y = -1;
					}
					else
					{
						Neutralize();
					}
				}
				else
				{
					if( obj_hit.tag == "cell" )
					{
						x = ( int )obj_hit.name[ 0 ] - 48;
					    y = ( int )obj_hit.name[ 2 ] - 48;
						if( GameLogic.instance.IsEmpty( x, y ) )
						{
							obj_selected = obj_hit;
							obj_selected.transform.localScale = new Vector2( 1.1f, 1.1f );
							BoardHandler.instance.ChangeCellSprite( obj_selected, 1 );
							options.SetActive( true );// burayi yerine gore degistirmem gerek ;
						}

					}
				}
			}
		}
	}
	
	#region Methods
	public void Neutralize()
	{
		options.SetActive( false );
		obj_selected.transform.localScale = Vector2.one;
		BoardHandler.instance.ChangeCellSprite( obj_selected, 0 );
		obj_selected = null;
		obj_hit = null;
		x = -1;
		y = -1;
	}
	#endregion

}
