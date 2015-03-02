using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class CharSelector : MonoBehaviour {
    public int playerNum = 1;
    enum CurElement{L, R};
    CurElement selected = CurElement.L;
    public ElementSelector left;
    public ElementSelector right;
    
    int counter = 0;
    const int time = 20;
    
    private const float modifier = 0.2f;

	// Use this for initialization
	void Start () {
        if(playerNum > 2 && XCI.GetNumPluggedCtrlrs() <= 2) {
            this.gameObject.SetActive(false);
            return;
        }
//        left.modifier = modifier;
	    left.selected = true;
        left.saveElement();
//        right.modifier = modifier;
        right.selected = false;
        right.saveElement();
        Debug.Log(GameInit.getPlayerElement(playerNum, 0).getName());
	}
    
//    public void Awake () {
//        left.background.color = new Color(left.background.color.r + 5, left.background.color.g, left.background.color.b);
//        
//    }
	
	// Update is called once per frame
	void OnGUI () {
//        if(counter == 0){
    	    float y = XCI.GetAxis(XboxAxis.LeftStickY, playerNum);
            if(y < -0.4 && selected != CurElement.R) {
                selected = CurElement.R;
                left.selected = false;
            left.background.color = new Color(left.background.color.r - modifier, left.background.color.g - modifier, left.background.color.b - modifier);
                right.selected = true;
            right.background.color = new Color(right.background.color.r + modifier, right.background.color.g + modifier, right.background.color.b + modifier);
//                right.background.color.r = right.background.color.r + 10;
                counter = time;
            }
            else if(y > 0.4 && selected != CurElement.L) {
                selected = CurElement.L;
                right.selected = false;
            right.background.color = new Color(right.background.color.r - modifier, right.background.color.g - modifier, right.background.color.b - modifier);
                left.selected = true;
            left.background.color = new Color(left.background.color.r + modifier, left.background.color.g + modifier, left.background.color.b + modifier);
                counter = time;
            }
//        }
//        else
//            counter--;
	}
}
