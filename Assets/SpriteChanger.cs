/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour {

	#region Variables
	public Image image;
	public Sprite normal;
	public Sprite negative;
	#endregion


	#region Methods
	public void PointerDown()
	{
		Debug.Log( gameObject.name + " Pointer Down" );

		image.sprite = negative;
	}

	public void PointerUp()
	{
		Debug.Log( gameObject.name + " Pointer Down" );

		image.sprite = normal;
	}
	#endregion
	}