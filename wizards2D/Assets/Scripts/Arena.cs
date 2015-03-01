using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arena : MonoBehaviour {
//	private static Arena _instance;
//	public static Arena instance {get {return _instance;}}	
	
	static int pNum; //number of players in game
    static PlayerChar[] pc; //States of each player

	public Canvas canvas;
	public Text win;

	//Called upon instantiation
	public void Awake(){
		pNum = GameInit.playerNum;
		pc = GameInit.players;

//		if(_instance!=null) Destroy(_instance.gameObject);
//		_instance=this;
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
//		canvas
		win.text = "Game Over!";
	}
    
    public void revenge(){
        Application.LoadLevel("ChooseElementScene");
    }
}
