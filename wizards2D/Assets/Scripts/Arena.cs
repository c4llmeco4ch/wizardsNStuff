using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {
	private static Arena _instance;
	public static Arena instance {get {return _instance;}}	
	
	int pNum; //number of players in game
	PlayerChar[] pc; //States of each player
	
	//Called upon instantiation
	public void Awake(){
		if(_instance!=null) Destroy(_instance.gameObject);
		_instance=this;
	}
	
	// Update is called once per frame
	public void Update () {
		int allDead=0;
		foreach(PlayerChar p in pc){
			if(p.isDead) 
				allDead++;
		}
		if(allDead>1)
			gameOver();
	}
	
	//Called when all but one player are dead
	public void gameOver(){
		//do stuff
	}
}
