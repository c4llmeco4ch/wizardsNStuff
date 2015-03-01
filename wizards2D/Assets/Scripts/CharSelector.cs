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

	// Use this for initialization
	void Start () {
        if(playerNum > 2 && XCI.GetNumPluggedCtrlrs() <= 2)
            this.gameObject.SetActive(false);
	    left.selected = true;
        left.saveElement();
        right.selected = false;
        right.saveElement();
	}
	
	// Update is called once per frame
	void OnGUI () {
//        if(counter == 0){
    	    float y = XCI.GetAxis(XboxAxis.LeftStickY);
            if(y < -0.4 && selected != CurElement.R) {
                selected = CurElement.R;
                left.selected = false;
                right.selected = true;
                counter = time;
            }
            else if(y > 0.4 && selected != CurElement.L) {
                selected = CurElement.L;
                right.selected = false;
                left.selected = true;
                counter = time;
            }
//        }
//        else
//            counter--;
	}
}
