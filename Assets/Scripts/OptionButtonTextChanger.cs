/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonTextChanger : MonoBehaviour {

	#region Variables
	public static OptionButtonTextChanger instance = null;

	public Text text_option_6;
	public Text text_option_1;
	public Text text_option_9;

	#endregion

	private void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
	}

	#region Methods
	public void ChangeNumber(int optionNumber , int number)
	{
		if( optionNumber == 6 )
			text_option_6.text = "" + number;
		else if( optionNumber == 1 )
			text_option_1.text = "" + number;
		else
			text_option_9.text = "" + number;
	}
	#endregion
}