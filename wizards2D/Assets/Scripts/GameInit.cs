using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameInit {
	public static int playerNum = 0;
//	public static Element[,] elementChoices = new Element[4,2];
	public static List<Element[]> elementChoices = new List<Element[]>(4);
	public static PlayerChar[] players;
	public static bool[] playing = new bool[4];
	public static PlayerColor[] colors;
	public static List<PlayerColor> colorList = new List<PlayerColor>(4);
    public static LinkedList<ElementSprite> elementList;
    public static Play playButton;
    
    public static bool tutorial = true;
    
    public static Arena arena;

    //sets what element the player has selected
	public static void setPlayerElement(int player, int p, Element e) {
		Debug.Log ((e == null) + " " + player + ":" + p + ":"+ e.getName());
		elementChoices[player-1][p] = e;
//		Element[] temp;
//		elementChoices.TryGetValue(player, temp);//[p] = e;
////		elementChoices [player-1, p] = e;
	}

    //Returns the element the player has slelected for that slot.
	public static Element getPlayerElement(int player, int p) {
		return elementChoices [player-1][p];
	}

    //sets the number of players playing and creates a PlayerChar array of that size
	public static void setNumPlayers(int i){
		playerNum = i;
		players = new PlayerChar[i];
		colors = colorList.ToArray();
	}
	
	public static void setPlayerColor(int player, PlayerColor color) {
		colorList[player-1] = color;
	}
	
	public static PlayerColor getPlayerColor(int player) {
		return colors[player-1];
	}
	
	public static void consolidate() {
		if(playerNum<4) {
			for(int i = playing.Length-1; i > 0; i--) {
				if(!playing[i]) {
					Debug.Log("delete "+i+"  "+colorList.Count);
					colorList.RemoveAt(i);
					elementChoices.RemoveAt(i);
				}
			}
//			int offset = 0;
//			for(int i = 0; i < playing.Length-1; i++) {
//				if(!playing[i]) {
//					offset++;
//					colors[i] = colors[i+offset];
//					
//				}
//			}
		}
	}
    
    //class to store assets and elements for element slection screen
    public class ElementSprite {
        public Element element;
        public Sprite elementText;
        public Sprite box;
        public ElementSprite(Element e, Sprite t, Sprite b) {
            element = e;
            elementText = t;
            box = b;
            elementList.AddLast(this);
        }
    }
}
