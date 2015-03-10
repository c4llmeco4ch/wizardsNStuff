using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(XCI.GetButtonDown(XboxButton.B))
			Application.LoadLevel("MainMenu");
	}
}
