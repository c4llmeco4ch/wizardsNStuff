using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int playerNum = 1;
    public GameObject win; 
	// Use this for initialization
	void Start () {
        if(playerNum > 2 && GameInit.playerNum <= 2) {
            this.gameObject.SetActive(false);
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
