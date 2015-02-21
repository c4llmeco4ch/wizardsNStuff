using UnityEngine;
using System.Collections;

public class GameInit {
	public static Element[,] elementChoices = new Element[4,2];

	public void setPlayerElement(int player, int p, Element e) {
		Debug.Log (player + ":" + p+ ":"+e.getName());
		elementChoices [player-1, p] = e;
	}

	public Element getPlayerElement(int player, int p) {
		return elementChoices [player-1, p];
	}
}
