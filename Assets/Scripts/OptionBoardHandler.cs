/*
Created By OFGONEN
*/
using UnityEngine;

public class OptionBoardHandler : MonoBehaviour {
	
	#region Variables
	#endregion
	
	#region Methods
	public void ChangePlayerNameTo()
	{
		int player = GameLogic.instance.GetCurrentPlayer();
		if( player == 1)
			UIHandler.instance.ChangePlayerName( true );
		else
			UIHandler.instance.ChangePlayerName( false );

	}

	public void ChangeToPlayerName()
	{
		int player = GameLogic.instance.GetCurrentPlayer();
		if( player == 1 )
			UIHandler.instance.ChangeToPlayerName(true);
		else 
			UIHandler.instance.ChangeToPlayerName(false);
	}
	#endregion

}
