using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(XCI.GetButtonDown(XboxButton.Start))
            play();
	}

	public void play() {
		int temp = XCI.GetNumPluggedCtrlrs();
        if(temp <= 1)
            temp = 2;
//        GameInit.setNumPlayers(temp);
        Application.LoadLevel ("Tutorial 1");//("DefaultLevelScene");
	}
}
