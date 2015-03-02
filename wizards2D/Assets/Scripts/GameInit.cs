using UnityEngine;
using System.Collections;

public static class GameInit {
	public static int playerNum = 2;
	public static Element[,] elementChoices = new Element[4,2];
	public static PlayerChar[] players;
    
    public static Arena arena;

	public static void setPlayerElement(int player, int p, Element e) {
		Debug.Log (player + ":" + p+ ":"+e.getName());
		elementChoices [player-1, p] = e;
	}

	public static Element getPlayerElement(int player, int p) {
		return elementChoices [player-1, p];
	}

	public static void setNumPlayers(int i){
		playerNum = i;
		players = new PlayerChar[i];
	}
}
