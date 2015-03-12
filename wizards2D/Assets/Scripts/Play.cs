using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class Play : MonoBehaviour {
	public bool[] select = {false, false, false, false};
	public Button playButton;

	public Play() {
		GameInit.playButton = this;
	}

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		if(select[0] || select[1] || select[2] || select[3])
			playButton.OnSelect(null);
		else
			playButton.OnDeselect(null);
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
