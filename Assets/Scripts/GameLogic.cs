﻿/*
Created By OFGONEN
*/
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	#region Variables
	public static GameLogic instance = null;

	public int score_normal;
	public int score_upsidedown;

	private Vector2 size;
	private GameObject[,] array_cells;
	private int[,] array_numbers;
	private List<Vector2> list_cells_empty;

	private int score_player1;
	private int score_player2;
	private int currentPlayer;

	private int counter_empty_cells;

	private int score_toPut;
	private int number_toPut;
	private int counter_combo;

	private bool is_finded_Score;
	private bool is_in_UpSideDown;
	private bool can_GoUpSideDown;
	private bool can_EndTurn;
	private bool did_putted_number;

	private int number_selected_X;
	private int number_selected_Y;


	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		size = BoardMaker.instance.GetTableSize();
		array_cells = new GameObject[ ( int )size.x, ( int )size.y ];
		array_numbers = new int[ ( int )size.x, ( int )size.y ];
		counter_empty_cells = ( int )( size.x * size.y );
		currentPlayer = 1;
		list_cells_empty = new List<Vector2>();
	}

	#region Methods

	public void EndTurn()
	{
		if(can_EndTurn  )
		{
			Counter.instance.TooglePause(0);
			Mouse.instance.Neutralize();
			if( !did_putted_number )
			{
				if( currentPlayer == 1  )
					PutRandomNumber( Mouse.instance.DecreaseNumber( 1 ) );
				else
					PutRandomNumber( Mouse.instance.DecreaseNumber( 2 ) );
				if( counter_empty_cells == 0 )
				{
					EndGame();
					return;
				}
			}

			if(Mouse.instance.HasNumbertoPut( currentPlayer == 1 ? 2 : 1 ) )
			{
				if( is_in_UpSideDown )
				{
					BoardHandler.instance.EndUpsideDown( currentPlayer );
					ChangeNumbers();
				}
				else
				{
					UIAnimationHandler.instance.EndTurn( currentPlayer );
				}
				currentPlayer = 3 - currentPlayer;
				score_toPut = 0;
				number_toPut = 0;
				is_finded_Score = false;
				can_GoUpSideDown = false;
				is_in_UpSideDown = false;
				can_EndTurn = false;
				did_putted_number = false;
				counter_combo = 0;
				Mouse.instance.canHit = true;
				SoundEffectPlayer.instance.Neutralize();
			}
		}
		
	}

	void PutRandomNumber(int number)
	{
		if(number != 0)
		{
			Vector2 cord_cell = list_cells_empty[ Random.Range( 0, list_cells_empty.Count ) ];
			array_numbers[ ( int )cord_cell.x, ( int )cord_cell.y ] = number;
			list_cells_empty.Remove( new Vector2( ( int )cord_cell.x, ( int )cord_cell.y ) );
			counter_empty_cells--;
			BoardHandler.instance.ChangeNumberSprite( array_cells[ ( int )cord_cell.x, ( int )cord_cell.y ], number, false );
		}
	}
	public void GoUpsideDown()
	{
		if( can_GoUpSideDown )
		{
			ChangeNumbers();
			UIAnimationHandler.instance.GoUpsideDown( currentPlayer );
			counter_combo = 0;
			can_GoUpSideDown = false;
			can_EndTurn = false;
			is_in_UpSideDown = true;
			Mouse.instance.canHit = true;
		}
	}

	void EndGame()
	{
		Counter.instance.TooglePause( 0 );
		PanelHandler.instance.EndGamePanel( true );
	}


	public void UpdateArrayCell(GameObject cell , int x, int y)
	{
		array_cells[ x, y ] = cell;
		list_cells_empty.Add( new Vector2( x, y ) );
	}

	public void UpdateArrayNumber(int numberToPut,int x , int y)
	{
		number_selected_X = x;
		number_selected_Y = y;
		list_cells_empty.Remove( new Vector2(x , y) );
		did_putted_number = true;
		number_toPut = numberToPut;

		array_numbers[ x, y ] = numberToPut;

		counter_empty_cells--;

		UpdateScore();

		if(counter_empty_cells == 0 ) 
		{
			EndGame();
		}
	}

	void UpdateScore()
	{
		FindScore();
		if( is_finded_Score )
		{
			Debug.Log( "Scored " + number_toPut );
			Mouse.instance.canHit = true;
			if(counter_combo >= 2 )
			{
				if( !is_in_UpSideDown )
				{
					can_GoUpSideDown = true;
					UIHandler.instance.ChangeToOption( false );
					can_EndTurn = false;
					SoundEffectPlayer.instance.EarnUpsideDownSound();
				}
				else
					SoundEffectPlayer.instance.ScoredSound();

			}
			else
			{
				can_EndTurn = true;
				UIHandler.instance.ChangeToOption( true );
				SoundEffectPlayer.instance.ScoredSound();
			}
			
		}
		else
		{
			

			if(!Mouse.instance.HasNumbertoPut(currentPlayer == 1 ? 2 : 1))
			{
				Mouse.instance.canHit = true;
				if(!is_in_UpSideDown && can_GoUpSideDown)
				{
					can_GoUpSideDown = false;
					UIHandler.instance.ChangeToOption( true );
					UIHandler.instance.button_option.gameObject.SetActive( false );
				}
				return;
			}
			else
			{
				can_GoUpSideDown = false;
				can_EndTurn = true;
				UIHandler.instance.ChangeToOption( true );
			}

		}
	}

	void FindScore()
	{
		is_finded_Score = false;
		
		LookUp();
		LookUpRight();
		LookRight();
		LookDownRight();
		LookDown();

		if( is_in_UpSideDown )
		{
			LookDownLeft();
			LookLeft();
			LookUpLeft();
		}

		if(is_finded_Score)
			DeclareScore();
	}

	void DeclareScore()
	{
		if( is_in_UpSideDown )
			score_toPut = score_upsidedown  + counter_combo - 1;
		else
			score_toPut = score_normal  + counter_combo - 1;

		if(currentPlayer == 1 )
		{
			score_player1 += score_toPut;
			UIHandler.instance.UpdateScore( 1, score_player1 );
		}
		else
		{
			score_player2 += score_toPut;
			UIHandler.instance.UpdateScore( 2, score_player2 );
		}

		Counter.instance.Scored( is_in_UpSideDown );

	}

	void ScoredLine( int cell1_X , int cell1_Y , int cell2_X , int cell2_Y , int cell3_X , int cell3_Y)
	{
		Debug.Log( "ScoredLine" );
		BoardHandler.instance.ChangeCellSprite( array_cells[ cell1_X, cell1_Y ], 2 );
		BoardHandler.instance.ChangeCellSprite( array_cells[ cell2_X, cell2_Y ], 2 );
		BoardHandler.instance.ChangeCellSprite( array_cells[ cell3_X, cell3_Y ], 2 );
		if( is_in_UpSideDown )
		{
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell1_X, cell1_Y ], array_numbers[ cell1_X, cell1_Y ] == 1 ? 1 : 15 - array_numbers[ cell1_X, cell1_Y ], true );
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell2_X, cell2_Y ], array_numbers[ cell2_X, cell2_Y ] == 1 ? 1 : 15 - array_numbers[ cell2_X, cell2_Y ], true );
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell3_X, cell3_Y ], array_numbers[ cell3_X, cell3_Y ] == 1 ? 1 : 15 - array_numbers[ cell3_X, cell3_Y ], true );
		}
		else
		{
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell1_X, cell1_Y ], array_numbers[ cell1_X, cell1_Y ], true );
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell2_X, cell2_Y ], array_numbers[ cell2_X, cell2_Y ], true );
			BoardHandler.instance.ChangeNumberSprite( array_cells[ cell3_X, cell3_Y ], array_numbers[ cell3_X, cell3_Y ], true );
		}

	}

	void ChangeNumbers()
	{
		for( int x = 0 ; x < size.x ; x++ )
		{
			for( int y = 0 ; y < size.y ; y++ )
			{
				if( array_numbers[ x, y ] == 6 )
					array_numbers[ x, y ] = 9;
				else if( array_numbers[ x, y ] == 9 )
					array_numbers[ x, y ] = 6;
			}
		}
	}

	public bool IsEmpty( int x, int y )
	{
		if( array_numbers[ x, y ] == 0 )
			return true;
		else
			return false;
	}

	#region Look Methods
	void LookLeft()
	{
		Debug.Log( "LookLeft" );
		if( number_toPut == 6 )
		{
			if( number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_Y - 2 >= 0)
				{
					if( array_numbers[ number_selected_X, number_selected_Y - 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y - 1, number_selected_X, number_selected_Y - 2 );
				}
			}
		}
		else if( number_toPut == 1 )
		{
			if( number_selected_Y - 1 >= 0 )
			{
					if( array_numbers[ number_selected_X, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 6 )
						{
							FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y - 1 );
						}
					}
					else if( array_numbers[ number_selected_X, number_selected_Y - 1 ] == 9 )
					{
						if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 9 )
						{
							FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y - 1 );
						}
					}
				
			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 1 )
			{
				if( number_selected_Y + 2 < size.y )
				{
					if( array_numbers[ number_selected_X, number_selected_Y + 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y + 2 );
				}
			}
		}
	}
	void LookRight()
	{
		Debug.Log( "LookRight" );
		if( number_toPut == 6 )
		{
			if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 1 )
			{
				if( number_selected_Y + 2 < size.y )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X, number_selected_Y + 2 ] == 6 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y + 2 );
					}
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X, number_selected_Y + 2 ] == 9 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y + 2 );
					}
				}
			}
		}
		else if( number_toPut == 1 )
		{
			if( number_selected_Y - 1 >= 0 )
			{
				if( is_in_UpSideDown )
				{
					if( array_numbers[ number_selected_X, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 6 )
						{
							FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y - 1 );
						}
					}
					else if( array_numbers[ number_selected_X, number_selected_Y - 1 ] == 9 )
					{
						if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 9 )
						{
							FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y - 1 );
						}
					}
				}
				else
				{
					if( array_numbers[ number_selected_X, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_Y + 1 < size.y && array_numbers[ number_selected_X, number_selected_Y + 1 ] == 9 )
						{
							FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y + 1, number_selected_X, number_selected_Y - 1 );
						}
					}
				}
			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_Y - 2 >= 0 )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X, number_selected_Y - 2 ] == 9 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y - 1, number_selected_X, number_selected_Y - 2 );
					}
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X, number_selected_Y - 2 ] == 6 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X, number_selected_Y - 1, number_selected_X, number_selected_Y - 2 );
					}
				}
			}
		}
	}
	void LookDown()
	{
		Debug.Log( "LookDown" );
		if(number_toPut == 6 )
		{
			if(number_selected_X + 1 < size.x && array_numbers[number_selected_X + 1 , number_selected_Y ] == 1 )
			{
				if( number_selected_X + 2 < size.x )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X + 2, number_selected_Y  );
					else if(!is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y , number_selected_X + 2, number_selected_Y );

				}
			}
		}
		else if(number_toPut == 1 )
		{
			if( number_selected_X - 1 >= 0 )
			{
				if( is_in_UpSideDown )
				{
					if( array_numbers[number_selected_X - 1 , number_selected_Y] == 6 )
					{
						if( number_selected_X + 1 < size.x && array_numbers[ number_selected_X + 1, number_selected_Y ] == 6 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X + 1, number_selected_Y );
					}
					else if( array_numbers[ number_selected_X - 1, number_selected_Y ] == 9 )
					{
						if( number_selected_X + 1 < size.x && array_numbers[ number_selected_X + 1, number_selected_Y ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X + 1, number_selected_Y );
					}
				}
				else
				{
					if( array_numbers[ number_selected_X - 1, number_selected_Y ] == 6 )
					{
						if( number_selected_X + 1 < size.x && array_numbers[ number_selected_X + 1, number_selected_Y ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X + 1, number_selected_Y );
					}
				}
			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_X - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y ] == 1 )
			{
				if( number_selected_X - 2 >= 0  )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X - 2, number_selected_Y);
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y , number_selected_X - 2, number_selected_Y );
				}
			}
		}
	}
	void LookUpLeft()
	{
		Debug.Log( "LookUpLeft" );
		if( number_toPut == 6 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y - 1 >= 0  && array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_X - 2 >= 0 && number_selected_Y - 2 >= 0 )
				{
					if( array_numbers[ number_selected_X - 2, number_selected_Y - 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X - 2, number_selected_Y - 2 );
				}
			}
		}
		else if(number_toPut == 1)
		{
			if(number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y )
			{
				if(array_numbers[ number_selected_X + 1  , number_selected_Y + 1 ] == 6 )
				{
					if( number_selected_X - 1 >= 0 && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y + 1, number_selected_X - 1, number_selected_Y - 1 );
				}
				else if( array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 9 )
				{
					if( number_selected_X - 1 >= 0 && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y + 1, number_selected_X - 1, number_selected_Y - 1 );
				}
			}
		}
		else if(number_toPut == 9)
		{
			if( number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 1 )
			{
				if( number_selected_X + 2 < size.x && number_selected_Y + 2 < size.y )
				{
					if( array_numbers[ number_selected_X + 2, number_selected_Y + 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y + 1, number_selected_X + 2, number_selected_Y + 2 );
				}
			}
		}
	}
	void LookDownLeft()
	{
		Debug.Log( "LookDownLeft" );
		if( number_toPut == 6 )
		{
			if( number_selected_X + 1 < size.x && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_X + 2 < size.x && number_selected_Y - 2 >= 0 )
				{
					if( array_numbers[ number_selected_X + 2, number_selected_Y - 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X + 2, number_selected_Y - 2 );
				}
			}
		}
		else if( number_toPut == 1 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y )
			{
				if( array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 6 )
				{
					if( number_selected_X + 1 < size.x && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X - 1, number_selected_Y + 1 );
				}
				else if( array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 9 )
				{
					if( number_selected_X + 1 < size.x && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X - 1, number_selected_Y + 1 );
				}
			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 1 )
			{
				if( number_selected_X - 2 >= 0 && number_selected_Y + 2 < size.x )
				{
					if( array_numbers[ number_selected_X - 2, number_selected_Y + 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y + 1, number_selected_X - 2, number_selected_Y + 2 );
				}
			}
		}
	}
	void LookUpRight()
	{
		Debug.Log( "LookUpRight" );
		if(number_toPut == 6 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y &&  array_numbers[number_selected_X - 1 , number_selected_Y + 1] == 1 )
			{
				if( number_selected_X - 2 >= 0 && number_selected_Y + 2 < size.y )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y + 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y + 1, number_selected_X - 2, number_selected_Y + 2 );
					else if(!is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y + 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y + 1, number_selected_X - 2, number_selected_Y + 2 );
				}
			}
		}
		else if(number_toPut == 1)
		{
			if( number_selected_X + 1 < size.x && number_selected_Y - 1 >= 0)
			{
				if( is_in_UpSideDown )
				{
					if(array_numbers[number_selected_X +  1 , number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 6 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X - 1, number_selected_Y + 1 );
					}
					else if( array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 9 )
					{
						if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X - 1, number_selected_Y + 1 );
					}
				}
				else
				{
					if( array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_X - 1 >= 0 && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X - 1, number_selected_Y + 1 ] == 9)
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X - 1, number_selected_Y + 1 );
					}
				}
			}
		}
		else if(number_toPut == 9 )
		{
			if( number_selected_X + 1 < size.x && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X + 1, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_X + 2 < size.x && number_selected_Y - 2 >= 0 )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y - 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X + 2, number_selected_Y - 2 );
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y - 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y - 1, number_selected_X + 2, number_selected_Y - 2 );
				}
			}
		}
	}
	void LookDownRight()
	{
		Debug.Log( "LookDownRight" );
		if( number_toPut == 6 )
		{
			if( number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 1 )
			{
				if( number_selected_X + 2 < size.x && number_selected_Y + 2 < size.y )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y + 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y + 1, number_selected_X + 2, number_selected_Y + 2 );
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y + 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y + 1, number_selected_X + 2, number_selected_Y + 2 );
				}
			}
		}
		else if( number_toPut == 1 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y - 1 >= 0 )
			{
				if( is_in_UpSideDown )
				{
					if( array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 6 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X + 1, number_selected_Y + 1 );
					}
					else if( array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 9 )
					{
						if( number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X + 1, number_selected_Y + 1 );
					}
				}
				else
				{
					if( array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 6 )
					{
						if( number_selected_X + 1 < size.x && number_selected_Y + 1 < size.y && array_numbers[ number_selected_X + 1, number_selected_Y + 1 ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X + 1, number_selected_Y + 1 );
					}
				}
			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_X - 1 >= 0 && number_selected_Y - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y - 1 ] == 1 )
			{
				if( number_selected_X - 2 >= 0 && number_selected_Y - 2 >= 0 )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y - 2 ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X - 2, number_selected_Y - 2 );
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y - 2 ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y - 1, number_selected_X - 2, number_selected_Y - 2 );
				}
			}
		}
	}
	void LookUp()
	{
		Debug.Log( "LookUp" );
		if( number_toPut == 6 )
		{
			if( number_selected_X - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y ] == 1 )
			{
				if( number_selected_X - 2 >= 0 )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y ] == 6 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X - 2, number_selected_Y );
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X - 2, number_selected_Y ] == 9 )
						FindedScore( number_selected_X, number_selected_Y, number_selected_X - 1, number_selected_Y, number_selected_X - 2, number_selected_Y );

				}
			}
		}
		else if( number_toPut == 1 )
		{

			if( number_selected_X + 1 < size.x )
			{
				if( is_in_UpSideDown )
				{
					if( array_numbers[ number_selected_X + 1, number_selected_Y ] == 9 )
					{
						if( number_selected_X - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X - 1, number_selected_Y );
					}
					else if( array_numbers[ number_selected_X + 1, number_selected_Y ] == 6 )
					{
						if( number_selected_X - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y ] == 6 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X - 1, number_selected_Y );
					}
				}
				else
				{
					if( array_numbers[ number_selected_X + 1, number_selected_Y ] == 6 )
					{
						if( number_selected_X - 1 >= 0 && array_numbers[ number_selected_X - 1, number_selected_Y ] == 9 )
							FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X - 1, number_selected_Y );
					}
				}

			}
		}
		else if( number_toPut == 9 )
		{
			if( number_selected_X + 1 < size.x && array_numbers[ number_selected_X + 1, number_selected_Y ] == 1 )
			{
				if( number_selected_X + 2 < size.x )
				{
					if( is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y ] == 9 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X + 2, number_selected_Y );
					}
					else if( !is_in_UpSideDown && array_numbers[ number_selected_X + 2, number_selected_Y ] == 6 )
					{
						FindedScore( number_selected_X, number_selected_Y, number_selected_X + 1, number_selected_Y, number_selected_X + 2, number_selected_Y );
					}
				}
			}
		}
	}
	
	void FindedScore(int x_cell1 , int y_cell1 , int x_cell2 , int y_cell2 , int x_cell3 , int y_cell3)
	{
		counter_combo++;
		ScoredLine( x_cell1, y_cell1, x_cell2, y_cell2, x_cell3, y_cell3 );
		is_finded_Score = true;
	}
	#endregion
	#region Get Methods
	public int GetScores(int player)
	{
		if( player == 1 )
			return score_player1;
		else
			return score_player2;
	}

	public bool GetIsInUpsideDown()
	{
		return is_in_UpSideDown;
	}

	public void SetEndTurn(bool endturn)
	{
		can_EndTurn = endturn;
	}

	public int GetCurrentPlayer()
	{
		return currentPlayer;
	}

	public int GetWinner()
	{
		if( score_player1 == score_player2 )
			return 0;
		else if( score_player1 > score_player2 )
			return 1;
		else
			return 2;
	}
	#endregion




	#endregion

}
