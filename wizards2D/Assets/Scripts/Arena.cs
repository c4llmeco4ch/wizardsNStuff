using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arena : MonoBehaviour {
//	private static Arena _instance;
//	public static Arena instance {get {return _instance;}}	
	
	static int pNum; //number of players in game
    static PlayerChar[] pc; //States of each player
    public PlayerStats[] ps = new PlayerStats[4];

	public Canvas canvas;
	public Text win;
    
    public RectTransform uiLL;
    public RectTransform uiLR;
    public RectTransform uiM;
    public RectTransform uiRL;
    public RectTransform uiRR;
    
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
    
    public void setUI(int player) {
        player--;
        pc[player].elementL = ui[player].FindChild("Element L").GetComponent<Image>();
        pc[player].elementR = ui[player].FindChild("Element R").GetComponent<Image>();
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
        for(int i = 1; i<pNum; i++)
            if(!pc[i].isDead)
                winner = i;
        ps[winner].win.gameObject.SetActive(true);
//		win.text = "Player "+i+" Won!";
	}
    
    public void revenge(){
        Application.LoadLevel("ChooseElementScene");
    }
}
