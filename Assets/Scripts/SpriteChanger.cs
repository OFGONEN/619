/*
Created By OFGONEN
*/
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

	#region Variables
	public Sprite sprite;
	public Sprite normal;
	public Sprite negative;
	#endregion


	#region Methods
	public void ChangeToNegative()
	{
		sprite = negative;
	}

	public void ChangeToNormal()
	{
		sprite = normal;
	}
	#endregion

}
