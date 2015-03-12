using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class Play : MonoBehaviour {
	public bool[] select = {false, false, false, false};
	public Button playButton;
	public GameObject no;

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
//	    if(XCI.GetButtonDown(XboxButton.Start))
//            play();
	}

	public void play() {
//		int temp = XCI.GetNumPluggedCtrlrs();
//        if(temp <= 1)
//            temp = 2;
		Debug.Log("Players: "+GameInit.playerNum);
		if(GameInit.playerNum<2){
			no.SetActive(true);
			no.GetComponent<NotEnoughPlayers>().Start();
			return;
		}
		
		GameInit.consolidate();
		GameInit.setNumPlayers(GameInit.playerNum);
		
        Application.LoadLevel ("DefaultLevelScene");
	}
}
