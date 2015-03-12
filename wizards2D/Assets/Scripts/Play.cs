using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class Play : MonoBehaviour {
	public bool select1 = false;
	public bool select2 = false;
	public bool select3 = false;
	public bool select4 = false;
	public Button playButton;

	public Play() {
		GameInit.playButton = this;
	}

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
//		if(select1 || select2 || select3 || select4)
//			this.
	    if(XCI.GetButtonDown(XboxButton.Start))
            play();
	}

	public void play() {
//		int temp = XCI.GetNumPluggedCtrlrs();
//        if(temp <= 1)
//            temp = 2;
        GameInit.setNumPlayers(2);
        Application.LoadLevel ("DefaultLevelScene");
	}
}
