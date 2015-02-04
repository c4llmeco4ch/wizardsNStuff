using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {
	int height; int width; //define size of board
	int pNum; //number of players in game
	bool[] alive; //for each player in game, true if alive, else false
	int[][]pCoor; //for each player in game, x,y coordinates for each player (-10,-10 if dead)
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
