using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Tutorial : MonoBehaviour {
    public string nextScene;
    public string previousScene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(XCI.GetButtonDown(XboxButton.A))
            Application.LoadLevel(nextScene);
        else if(XCI.GetButtonDown(XboxButton.B))
            Application.LoadLevel(previousScene);
        else if(XCI.GetButtonDown(XboxButton.Start))
            Application.LoadLevel("DefaultLevelScene");
	}
    
}
