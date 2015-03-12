using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int playerNum = 1;
    public GameObject win; 
    public Text spells;
    public Text time;
    public Image player;
	// Use this for initialization
	void Start () {
        if(playerNum > 2 && GameInit.playerNum <= 2 && playerNum > GameInit.playerNum) {
            this.gameObject.SetActive(false);
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setColor(PlayerColor color) {
		player.sprite = Resources.Load("UI Art Assets/Summary/player_"+color.ToString(), typeof(Sprite)) as Sprite;
	}
    
    public void setSpellsCast(int numCast) {
        spells.text = "Spells Cast: "+numCast;
    }
    
    public void setTime(int t) {
    	if(t == -1)
			time.text = "";
		else
    		time.text = "Time of Death: "+t;
    }
}
