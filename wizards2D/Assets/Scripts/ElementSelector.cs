using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class ElementSelector : MonoBehaviour {
	public Button left;
	public Button right;
	public int playerNum;
	public int elementNum;

	public GameInit struc;
	
	public Image selectedElementText;

	private Element selectedElement;

	private static Element[] elements = new Element[] { new Earth(), new Air(), new Fire(), new Water() };
	private int cur;

	// Use this for initialization
	void Start () {
		cur = elementNum;
		selectedElement = elements [cur];
		struc = new GameInit ();
		struc.setPlayerElement (playerNum, elementNum, selectedElement);
	}
	
	// Update is called once per frame
	void Update () {
		float x = XCI.GetAxis (XboxAxis.LeftStickX, playerNum);

	}

	public void update(bool inc) {
		if (inc && cur < elements.Length-1) {
						selectedElement = elements [++cur];
				} else if (!inc && cur > 0) {
						cur--;
						selectedElement = elements [cur];
				}
		selectedElementText.sprite = Resources.Load ("UI Art Assets/Selection/Text" + selectedElement.getName (), typeof(Sprite)) as Sprite;
		struc.setPlayerElement (playerNum, elementNum, selectedElement);
	}

}
