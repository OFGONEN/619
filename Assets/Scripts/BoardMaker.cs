﻿/*
Created By OFGONEN
*/
using UnityEngine;

public class BoardMaker : MonoBehaviour {

	#region Variables
	public GameObject cell;


	const int PIXELLENGTHX = 300;
	const int PIXELLENGTHY = 450;
	const int PIXELSIZECELL = 100;

	private float startPointX;
	private float startPointY;
	private float cellScale;
	private Vector2 positionToPut;

	private Vector2 tableSize;
	#endregion
	
	
	void Start () 
	{
		GetTableSize();
		BuildBoard( (int)tableSize.x, ( int )tableSize.y );
	}
	
	
	#region Methods
	public void BuildBoard(int sizeX , int sizeY)
	{
		DecideStartPoints( sizeX, sizeY );

		GameObject instance;
		for(int y = 0 ; y < sizeY ; y++ )
		{
			for(int x = 0 ; x < sizeX ; x++ )
			{
				instance = Instantiate( cell, positionToPut, Quaternion.identity );
				instance.name = "" + y + "-" + x;
				instance.transform.SetParent( transform );
				instance.transform.localScale = new Vector2( cellScale, cellScale );
				positionToPut.x = positionToPut.x + cellScale;
			}
			positionToPut.x = -startPointX;
			positionToPut.y = positionToPut.y - cellScale;
		}
	}

	private void DecideStartPoints(int sizeX , int sizeY)
	{
		float pixelSize = 600 / sizeX;
		cellScale = pixelSize / (float)PIXELSIZECELL;
		startPointX = ( 300 - pixelSize / 2 ) / 100;
		startPointY = ( 450 - pixelSize / 2 ) / 100;
		positionToPut = new Vector2( -startPointX, startPointY );
	}

	private void GetTableSize()
	{
		int tableNumber = PlayerPrefs.GetInt( "Table Size" );
		if( tableNumber == 0 )
		{
			tableSize.x = 6;
			tableSize.y = 9;
		}
		else if(tableNumber == 1 )
		{
			tableSize.x = 8;
			tableSize.y = 12;
		}
		else if(tableNumber == 2 )
		{
			tableSize.x = 12;
			tableSize.y = 18;
		}
	}
	#endregion
	
	}
