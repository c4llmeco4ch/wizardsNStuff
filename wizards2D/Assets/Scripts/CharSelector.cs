using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class CharSelector : MonoBehaviour {
    public int playerNum;
    enum CurElement{L, R, P};
    CurElement selected = CurElement.L;
    public ElementSelector left;
    public ElementSelector right;
    public PlayerColor color;
    public Image[] images = new Image[12];
    public bool isActive = false;
    public GameObject activeA;
//    Color alpha = new Color(1f,1f,1f,0.5f);
    
    int counter = 0;
    const int time = 20;
    
    private const float modifier = 0.3f;
    
    bool init = false;

	// Use this for initialization
	void Start () {
        
        switch(playerNum){
        	case 1: color = PlayerColor.Blue;
        	break;
        	case 2: color = PlayerColor.Red;
        	break;
        	case 3: color = PlayerColor.Green;
        	break;
        	case 4: color = PlayerColor.Yellow;
        	break;
		}
		saveColor();
		
		left.playerNum = playerNum;
        right.playerNum = playerNum;
        left.other = right;
        right.other = left;
	    left.selected = true;
        left.saveElement();
//        right.modifier = modifier;
        right.selected = false;
        right.playerNum = playerNum;
        right.saveElement();
		Debug.Log(GameInit.getPlayerElement(playerNum, 0).getName());
//		activate(false);
	}
	
	void Awake() {
//		if(playerNum == 1 || playerNum == 2)
//			activate(true);
//		else
			activate(false);
			
	}
	
	void Update () {
//		saveColor();
		if(!isActive) {
			if(XCI.GetButtonDown(XboxButton.B, playerNum))
				Application.LoadLevel("MainMenu");
			else if(XCI.GetButtonDown(XboxButton.A, playerNum))
				activate(true);
		}
		else {
			if(XCI.GetButtonDown(XboxButton.B, playerNum))
				activate(false);
			else if(selected == CurElement.P && XCI.GetButtonDown(XboxButton.A, playerNum))
				GameInit.playButton.play();
		}
	}
	
	// Update is called once per frame
    void OnGUI () {
    	if(isActive) {
			if(counter == 0){
				Play play = GameInit.playButton;
			    float y = XCI.GetAxis(XboxAxis.LeftStickY, playerNum);
		        if(y < -0.4) {
					if(selected == CurElement.L) {
					   selected = CurElement.R;
		                left.selected = false;
		                right.selected = true;
		                right.background.color = new Color(right.background.color.r + modifier, right.background.color.g + modifier, right.background.color.b + modifier);
		                left.background.color = new Color(left.background.color.r - modifier, left.background.color.g - modifier, left.background.color.b - modifier);
		            //                right.background.color.r = right.background.color.r + 10;
		                counter = time;
		            }
		            else if(selected == CurElement.R) {
		            	selected = CurElement.P;
		            	play.select[playerNum-1] = true;
		            	right.selected = false;
						right.background.color = new Color(right.background.color.r - modifier, right.background.color.g - modifier, right.background.color.b - modifier);
						counter = time;
					}
				}
				else if(y > 0.4) {
					if(selected == CurElement.R) {
						selected = CurElement.L;
		                right.selected = false;
		                left.selected = true;
		                left.background.color = new Color(left.background.color.r + modifier, left.background.color.g + modifier, left.background.color.b + modifier);
		                right.background.color = new Color(right.background.color.r - modifier, right.background.color.g - modifier, right.background.color.b - modifier);
		                counter = time;
		            }
		            else if(selected == CurElement.P) {
						selected = CurElement.R;
						play.select[playerNum-1] = false;
						right.selected = true;
						right.background.color = new Color(right.background.color.r + modifier, right.background.color.g + modifier, right.background.color.b + modifier);
						counter = time;
					}
				}
			}
			else
				counter--;
		}
	}
	
	void activate(bool act) {
//		if(!init)
//			init = true;
//		else
//			if(GameInit.playerNum <= 2)
//				return;
		isActive = act;
		left.isActive = act;
		right.isActive = act;
		GameInit.playing[playerNum-1] = act;
		foreach(Image i in images) {
			Color temp = i.color;
			if(act)
				temp.a = 1f;
			else
				temp.a = 0.5f;
			i.color = temp;;
		}
		if(act) {
			activeA.SetActive(false);
			left.saveElement();
			right.saveElement();
			saveColor();
			GameInit.playerNum++;
		}
		else {
			if(GameInit.playerNum > 0) {
				GameInit.playerNum--;
			}
			activeA.SetActive(true);
		}
	}
	
	void saveColor() {
		GameInit.setPlayerColor(playerNum, color);
	}
}
