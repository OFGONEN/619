/*
Created By OFGONEN
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IPointerExitHandler
{

	#region Variables
	public ImageChanger start;
	public ImageChanger how2play;
	public ImageChanger options;
	public ImageChanger wtfAreWe;

	#endregion
	
	

	#region Methods
	public void OnPointerDown( PointerEventData eventData )
	{
		GoNegative();
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		GoNormal();
	}

	public void OnPointerExit( PointerEventData eventData )
	{
		GoNormal();		
	}

	void GoNegative()
	{
		start.ChangeToNegative();
		how2play.ChangeToNegative();
		options.ChangeToNegative();
		wtfAreWe.ChangeToNegative();
		Camera.main.backgroundColor = Color.black;
	}

	void GoNormal()
	{
		start.ChangeToNormal();
		how2play.ChangeToNormal();
		options.ChangeToNormal();
		wtfAreWe.ChangeToNormal();
		Camera.main.backgroundColor = Color.white;
	}
	#endregion
}