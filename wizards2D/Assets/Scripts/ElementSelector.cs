using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElementSelector {//: MonoBehaviour {
	public Button left;
	public Button right;
	public Image selectedElementText;
	public int playerNum;
	public int elementNum;

	public GameInit struc;
	private Element selectedElement;


	// Use this for initialization
	void Start () {
		if(elementNum == 0)
			selectedElement = new Earth();
		else
			selectedElement = new Air();
		struc = new GameInit ();
//		struc.;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Left() {

	}

	public void Right() {

	}
}
