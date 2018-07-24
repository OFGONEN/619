/*
Created By OFGONEN
*/
using UnityEngine;

public class BoardMaker : MonoBehaviour {

	#region Variables
	public static BoardMaker instance = null;
	public GameObject cell;


	const int PIXEL_LENGTH_X = 600;
	const int PIXEL_LENGTH_Y = 900;
	const int PIXEL_SIZE_CELL = 100;

	private float startPointX;
	private float startPointY;
	private float cellScale;
	private Vector2 positionToPut;
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
		BuildBoard( (int)GetTableSize().x, ( int )GetTableSize().y );
	}
	
	
	#region Methods
	public void BuildBoard(int sizeX , int sizeY)
	{
		DecideStartPoints( sizeX, sizeY );

		GameObject instance;
		for(int x = 0 ; x < sizeX ; x++ )
		{
			for(int y = 0 ; y < sizeY ; y++ )
			{
				instance = Instantiate( cell, positionToPut, Quaternion.identity );
				instance.name = "" + x + "-" + y;
				instance.transform.SetParent( transform );
				instance.transform.localScale = new Vector2( cellScale, cellScale );
				GameLogic.instance.UpdateArrayCell( instance, x, y );
				positionToPut.x = positionToPut.x + cellScale;
			}
			positionToPut.x = -startPointX;
			positionToPut.y = positionToPut.y - cellScale;
		}
	}

	private void DecideStartPoints(int sizeX , int sizeY)
	{
		float pixelSize = PIXEL_LENGTH_X / sizeY;
		cellScale = pixelSize / PIXEL_SIZE_CELL;
		startPointX = ( PIXEL_LENGTH_X / 2 - pixelSize / 2 ) / 100;
		startPointY = ( PIXEL_LENGTH_Y / 2 - pixelSize / 2 ) / 100;
		positionToPut = new Vector2( -startPointX, startPointY );
	}

	public Vector2 GetTableSize()
	{
		Vector2 tableSize;

		int tableNumber = PlayerPrefs.GetInt( "Table Size" );
		if( tableNumber == 0 )
		{
			tableSize.x = 9;
			tableSize.y = 6;
		}
		else if(tableNumber == 1 )
		{
			tableSize.x = 12;
			tableSize.y = 8;
		}
		else 
		{
			tableSize.x = 18;
			tableSize.y = 12;
		}

		return tableSize;
	}

	
	#endregion
	
	}
