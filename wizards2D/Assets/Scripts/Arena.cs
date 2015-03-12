using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arena : MonoBehaviour {
	
	static int pNum; //number of players in game
    static PlayerChar[] pc; //States of each player
    public PlayerStats[] ps = new PlayerStats[4];

	public Canvas canvas;
	public Text win;
    
    //all the different positons for player ui depending on the number of players
    public RectTransform uiLL;
    public RectTransform uiLR;
    public RectTransform uiM;
    public RectTransform uiRL;
    public RectTransform uiRR;
    
    //the player ui that will be used for the current number of players
    public RectTransform[] ui;

	//Called upon instantiation
	public void Start(){
		pNum = GameInit.playerNum;
		pc = GameInit.players;

        ui = new RectTransform[pNum];
        ui[0] = uiLL;
        switch(pNum){
            case 2: ui[1] = uiRR;
                break;
            case 3: ui[1] = uiRR;
                ui[2] = uiM;
                break;
            case 4: ui[1] = uiLR;
                ui[2] = uiRL;
                ui[3] = uiRR;
                break;
        }
        foreach(RectTransform r in ui)
            r.gameObject.SetActive(true);
        
        GameInit.arena = this;
	}
    
    //sets up the ui for each player
    public void setUI(int player) {
        player--;
        pc[player].elementL = ui[player].FindChild("Element L").GetComponent<Image>();
        pc[player].elementR = ui[player].FindChild("Element R").GetComponent<Image>();
        pc[player].playerPic = ui[player].FindChild("Player").GetComponent<Image>();
        pc[player].healthBarTrans = ui[player].FindChild("Health Bar").GetComponent<RectTransform>();
        pc[player].manaBarTrans = ui[player].FindChild("Mana Bar").GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	public void Update () {
		int allDead=0;
		for(int i = 0; i < pNum; i++){
			if(pc[i].isDead) 
				allDead++;
		}
		//Debug.Log("all dead: "+allDead+" out of "+pNum);
		if(allDead >= pNum-1)
			gameOver();
	}

	//Called when all but one player are dead
	public void gameOver(){
		Debug.Log ("GameOver");
        canvas.gameObject.SetActive(true);
		int winner = 0;
		setPlayerStats(0);
        for(int i = 1; i<pNum; i++){
			setPlayerStats(i);
            if(!pc[i].isDead)
                winner = i;
        }
        ps[winner].setTime(-1);
        ps[winner].win.gameObject.SetActive(true);
//		win.text = "Player "+i+" Won!";
	}
	
	public void setPlayerStats(int p) {
		ps[p].setColor(pc[p].color);
		ps[p].setSpellsCast(pc[p].spellsCast);
		ps[p].setTime(pc[p].timeAlive);
	}
    
    //onClick to restart game
    public void revenge(){
        Application.LoadLevel("ChooseElementScene");
    }
    
    //onClick to load main menu
    public void mainMenu(){
        Application.LoadLevel("MainMenu");
    }
}
