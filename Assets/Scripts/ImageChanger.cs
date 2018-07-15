/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {

	#region Variables
	public Image image;
	public Sprite normal;
	public Sprite negative;
	#endregion


	#region Methods
	public void ChangeToNegative()
	{
		image.sprite = negative;
	}

	public void ChangeToNormal()
	{
		image.sprite = normal;
	}
	#endregion
	}