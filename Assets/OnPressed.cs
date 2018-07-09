/*
Created By OFGONEN
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

		start.PointerDown();
		how2play.PointerDown();
		options.PointerDown();
		wtfAreWe.PointerDown();
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		Debug.Log( gameObject.name + " Pointer Down" );

		start.PointerUp();
		how2play.PointerUp();
		options.PointerUp();
		wtfAreWe.PointerUp();
	}
	#endregion
}