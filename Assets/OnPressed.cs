/*
Created By OFGONEN
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IPointerExitHandler
{

	#region Variables
	public SpriteChanger start;
	public SpriteChanger how2play;
	public SpriteChanger options;
	public SpriteChanger wtfAreWe;

	#endregion
	
	

	#region Methods
	public void OnPointerDown( PointerEventData eventData )
	{
		Debug.Log( gameObject.name + " Pointer Down" );
		GoNegative();
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		Debug.Log( gameObject.name + " Pointer Up" );
		GoNormal();
	}

	public void OnPointerExit( PointerEventData eventData )
	{
		Debug.Log( gameObject.name + " Pointer Exit" );
		GoNormal();		
	}

	void GoNegative()
	{
		start.PointerDown();
		how2play.PointerDown();
		options.PointerDown();
		wtfAreWe.PointerDown();
		Camera.main.backgroundColor = Color.black;
	}

	void GoNormal()
	{
		start.PointerUp();
		how2play.PointerUp();
		options.PointerUp();
		wtfAreWe.PointerUp();
		Camera.main.backgroundColor = Color.white;
	}
	#endregion
}