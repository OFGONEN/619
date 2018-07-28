/*
Created By OFGONEN
*/
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

	#region Variables
	public static Mouse instance = null;
	public GameObject options;

	public bool canHit;

	private RaycastHit2D hitInfo;
	private GameObject obj_selected;
	private GameObject obj_hit;
	private float cell_normal_scale;
	private float cell_big_scale;

	int x , y;
	int size_x , size_y , size_table , size;

	int player1_counter_number_1 , player1_counter_number_6 , player1_counter_number_9;
	int player2_counter_number_1 , player2_counter_number_6 , player2_counter_number_9;

	List<int> active_numbers_player1;
	List<int> active_numbers_player2;



	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}

	private void Start()
	{
		active_numbers_player1 = new List<int>();
		active_numbers_player1.Add( 6 );
		active_numbers_player1.Add( 1 );
		active_numbers_player1.Add( 9 );

		active_numbers_player2 = new List<int>();
		active_numbers_player2.Add( 6 );
		active_numbers_player2.Add( 1 );
		active_numbers_player2.Add( 9 );

		cell_normal_scale =  BoardMaker.instance.GetCellScale();
		cell_big_scale = cell_normal_scale * 1.2f;
		x = (int)BoardMaker.instance.GetTableSize().x;
		y = ( int )BoardMaker.instance.GetTableSize().y;

		size = PlayerPrefs.GetInt( "Table Size" );
		size_table = PlayerPrefs.GetInt( "Table Size" ) + 1;
		size_x = ( int )BoardMaker.instance.GetTableSize().x - 1;
		size_y = ( int )BoardMaker.instance.GetTableSize().y - 1;


		int counter = ( x * y )/6;
		player1_counter_number_6 = counter;
		player1_counter_number_1 = counter;
		player1_counter_number_9 = counter;
		player2_counter_number_6 = counter;
		player2_counter_number_1 = counter;
		player2_counter_number_9 = counter;
		canHit = true;

	}

	void Update () 
	{
		if( Input.GetMouseButtonDown( 0 ) && canHit )
		{
			hitInfo = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );
			if( hitInfo )
			{
				obj_hit = hitInfo.collider.gameObject;
				if( obj_selected != null )
				{
					if( obj_hit.tag == "option" )
					{

						int number_put = ( int )obj_hit.name[ 0 ] - 48;

						if( GameLogic.instance.GetIsInUpsideDown() )
						{
							if(number_put == 1 && CanPut(number_put) )
							{
								BoardHandler.instance.ChangeNumberSprite( obj_selected, number_put, false );
								ResetMouse();
								GameLogic.instance.UpdateArrayNumber( number_put, x, y );
							}
							else
							{
								if(CanPut(number_put ) )
								{
									BoardHandler.instance.ChangeNumberSprite( obj_selected, 15 - number_put, false );
									ResetMouse();
									GameLogic.instance.UpdateArrayNumber( number_put, x, y );
								}
							}

						}
						else
						{
							if( CanPut( number_put ) )
							{
								BoardHandler.instance.ChangeNumberSprite( obj_selected, number_put, false );
								ResetMouse();
								GameLogic.instance.UpdateArrayNumber( number_put, x, y );
							}

						}

						

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
						if(obj_hit.name.Length == 3)
						{
							x = ( int )obj_hit.name[ 0 ] - 48;
							y = ( int )obj_hit.name[ 2 ] - 48;
						}
						else
						{
							if( obj_hit.name[ 1 ] == '-' )
							{
								x = ( int )obj_hit.name[ 0 ] - 48;
								y = ( 10 + ( ( int )obj_hit.name[ 2 ] - 48 ) );
							}
							else
							{
								if(obj_hit.name.Length == 4 )
								{
									x = (  10 + ( ( int )obj_hit.name[ 1 ] - 48 ) );
									y = ( int )obj_hit.name[ 3 ] - 48;
								}
								else
								{
									x = ( 10 + ( ( int )obj_hit.name[ 1 ] - 48 ) );
									y = ( 10 + ( ( int )obj_hit.name[ 4 ] - 48 ) );
								}
							}
							


						}

						if( GameLogic.instance.IsEmpty( x, y ) )
						{
							obj_selected = obj_hit;
							obj_selected.transform.localScale = new Vector2( cell_big_scale, cell_big_scale);
							BoardHandler.instance.ChangeCellSprite( obj_selected, 1 );
							OpenOptions();
						}

					}
				}
			}
		}
	}

	#region Methods
	void OpenOptions() // optinlarin guzel bir yerde cikmasi icin bu 
	{
		options.transform.position = obj_selected.transform.position;
		DecideLocation();
		if( GameLogic.instance.GetCurrentPlayer() == 1 )
		{
			options.transform.rotation = Quaternion.Euler( 0, 0, 0 );
			OptionButtonTextChanger.instance.ChangeNumber( 6, player1_counter_number_6 );
			OptionButtonTextChanger.instance.ChangeNumber( 9, player1_counter_number_9 );
			OptionButtonTextChanger.instance.ChangeNumber( 1, player1_counter_number_1 );
		}
		else
		{
			options.transform.rotation = Quaternion.Euler( 0, 0, 180 );
			OptionButtonTextChanger.instance.ChangeNumber( 6, player2_counter_number_6 );
			OptionButtonTextChanger.instance.ChangeNumber( 9, player2_counter_number_9 );
			OptionButtonTextChanger.instance.ChangeNumber( 1, player2_counter_number_1 );
		}
		options.SetActive( true );
	}

	void DecideLocation()
	{
		bool upsidedown = GameLogic.instance.GetIsInUpsideDown();
		if(x <= size_table)
		{
			if(   y < size_y - size_table )
			{
				if( upsidedown )
					UpLeft();
				else
					DownRight();
			}
			else
			{
				if( upsidedown )
					UpRight();
				else
					DownLeft();
			}
		}
		else if( y >= size_y - size_table )
		{
			if( upsidedown )
				DownRight();
			else
				UpLeft();
		}
		else
		{
			if( upsidedown )
				DownLeft();
			else
				UpRight();

		}
	}

	public bool HasNumbertoPut(int player)
	{
		if( player == 1 )
		{
			if( player1_counter_number_1 != 0 || player1_counter_number_6 != 0 || player1_counter_number_9 != 0 )
				return true;
		}
		else
		{
			if( player2_counter_number_1 != 0 || player2_counter_number_6 != 0 || player2_counter_number_9 != 0 )
				return true;
		}

		return false;
	}

	public int DecreaseNumber(int player)
	{
		int number = 3;

		if(player == 1 )
		{
			number = active_numbers_player1[ Random.Range( 0, active_numbers_player1.Count ) ];

			if( number == 6 )
				player1_counter_number_6--;
			else if( number == 1 )
				player1_counter_number_1--;
			else
				player1_counter_number_9--;
		}
		else
		{
			number = active_numbers_player2[ Random.Range( 0, active_numbers_player2.Count ) ];

			if( number == 6 )
				player2_counter_number_6--;
			else if( number == 1 )
				player2_counter_number_1--;
			else
				player2_counter_number_9--;
		}

		return number;
	}

	void UpRight()
	{
		options.transform.GetChild( 0 ).localPosition = new Vector2( 0, 1 );
		options.transform.GetChild( 1 ).localPosition = new Vector2( 1, 1 );
		options.transform.GetChild( 2 ).localPosition = new Vector2( 1, 0 );
		AddBump( 1, 1 );
	}

	void UpLeft()
	{
		options.transform.GetChild( 0 ).localPosition = new Vector2( -1, 0 );
		options.transform.GetChild( 1 ).localPosition = new Vector2( -1, 1 );
		options.transform.GetChild( 2 ).localPosition = new Vector2( 0, 1 );
		AddBump( -1, 1 );
	}

	void DownRight()
	{
		options.transform.GetChild( 0 ).localPosition = new Vector2( 0, -1 );
		options.transform.GetChild( 1 ).localPosition = new Vector2( 1, -1 );
		options.transform.GetChild( 2 ).localPosition = new Vector2( 1, 0 );
		AddBump( 1, -1 );
	}

	void DownLeft()
	{
		options.transform.GetChild( 0 ).localPosition = new Vector2( -1, 0 );
		options.transform.GetChild( 1 ).localPosition = new Vector2( -1, -1 );
		options.transform.GetChild( 2 ).localPosition = new Vector2( 0, -1 );
		AddBump( -1, -1 );
	}

	void AddBump(int x , int y)
	{
		if(size == 0 )
		{
			if( GameLogic.instance.GetCurrentPlayer() == 1 )
				options.transform.position += new Vector3( x * 0.5f, y * 0.5f, 0 );
			else
				options.transform.position -= new Vector3( x * 0.5f, y * 0.5f, 0 );
		}
		else if(size == 1)
		{
			if( GameLogic.instance.GetCurrentPlayer() == 1 )
				options.transform.position += new Vector3( x * 0.25f, y * 0.25f, 0 );
			else
				options.transform.position -= new Vector3( x * 0.25f, y * 0.25f, 0 );
		}
		else if(size == 2 )
		{
			if( GameLogic.instance.GetCurrentPlayer() == 1 )
				options.transform.position += new Vector3( x * 0.15f, y * 0.15f, 0 );
			else
				options.transform.position -= new Vector3( x * 0.15f, y * 0.15f, 0 );
		}
		else
		{
			if( GameLogic.instance.GetCurrentPlayer() == 1 )
				options.transform.position += new Vector3( -x * 0.1f, -y * 0.1f, 0 );
			else
				options.transform.position -= new Vector3( -x * 0.1f, -y * 0.1f, 0 );
		}
		
		

	}

	void ResetMouse()
	{
		obj_selected.transform.localScale = new Vector2( cell_normal_scale, cell_normal_scale );
		BoardHandler.instance.ChangeCellSprite( obj_selected, 0 );
		options.SetActive( false );
		canHit = false;
		obj_hit = null;
		obj_selected = null;
	}

	public void Neutralize()
	{
		if( obj_selected != null )
		{
			options.SetActive( false );
			options.transform.position = Vector2.zero;
			options.transform.rotation = Quaternion.identity;
			obj_selected.transform.localScale = new Vector2( cell_normal_scale, cell_normal_scale );
			BoardHandler.instance.ChangeCellSprite( obj_selected, 0 );
			obj_selected = null;
			obj_hit = null;
			x = -1;
			y = -1;
		}	
	}

	bool CanPut(int numbertoput)
	{
		if(GameLogic.instance.GetCurrentPlayer() == 1)
		{
			if( numbertoput == 6 )
			{
				if( player1_counter_number_6 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player1_counter_number_6 );
					player1_counter_number_6--;
					if( player1_counter_number_6 == 0 )
						active_numbers_player1.Remove( 6 );
					return true;
				}
			}
			else if( numbertoput == 9 )
			{
				if( player1_counter_number_9 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player1_counter_number_9 );
					player1_counter_number_9--;
					if( player1_counter_number_9 == 0 )
						active_numbers_player1.Remove( 9 );
					return true;
				}
			}
			else
			{
				if( player1_counter_number_1 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player1_counter_number_1 );
					player1_counter_number_1--;
					if( player1_counter_number_1 == 0 )
						active_numbers_player1.Remove( 1 );
					return true;
				}
			}
		}
		else
		{
			if( numbertoput == 6 )
			{
				if( player2_counter_number_6 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player2_counter_number_6 );
					player2_counter_number_6--;
					if( player2_counter_number_6 == 0 )
						active_numbers_player2.Remove( 6 );
					return true;
				}
			}
			else if( numbertoput == 9 )
			{
				if( player2_counter_number_9 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player2_counter_number_9 );
					player2_counter_number_9--;
					if( player2_counter_number_9 == 0 )
						active_numbers_player2.Remove( 9 );
					return true;
				}
			}
			else
			{
				if( player2_counter_number_1 != 0 )
				{
					OptionButtonTextChanger.instance.ChangeNumber( numbertoput, player2_counter_number_1 );
					player2_counter_number_1--;
					if( player2_counter_number_1 == 0 )
						active_numbers_player2.Remove( 1 );
					return true;
				}
			}
		}

		return false;
			
	}

	
	#endregion

}
