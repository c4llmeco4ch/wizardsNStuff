using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class ElementSelector : MonoBehaviour {
	public Button left;
	public Button right;
	public int playerNum;
	public int elementNum;
    public bool selected;

//	public static GameInit struc;
	
	public Image selectedElementText;
    public Image background;

	private Element selectedElement;

	private static Element[] elements = new Element[] { new Earth(), new Air(), new Fire(), new Water() };
	private int cur;
    
    const float modifier = 0.2f;
    
    
    int counter = 0;
    const int time = 15;

	// Use this for initialization
    void Start () {
//        if(playerNum > 2 && XCI.GetNumPluggedCtrlrs() <= 2  && playerNum > XCI.GetNumPluggedCtrlrs()) {
//            this.gameObject.SetActive(false);
//            return;
//        }
//        background = GetComponent<Image>();
        if(selected)
            background.color = new Color(background.color.r + modifier, background.color.g + modifier, background.color.b + modifier);
		cur = elementNum;
		selectedElement = elements [cur];
        saveElement();
	}
	
	// Update is called once per frame
	void Update () {
        if(selected){
            if(counter == 0){
    		    float x = XCI.GetAxis (XboxAxis.LeftStickX, playerNum);
                if(x < 0){
                    update(false);
                }
                else if(x > 0)
                    update(true);
            }
//            else if(counter == 5)
//            
            else
                counter--;
        }
	}

	public void update(bool inc) {
        if (inc && cur < elements.Length-1) {
            GetComponent<AudioSource>().Play();
			selectedElement = elements [++cur];
        } else if (!inc && cur > 0) {
            GetComponent<AudioSource>().Play();
			cur--;
			selectedElement = elements [cur];
		}
		selectedElementText.sprite = Resources.Load ("UI Art Assets/Selection/Text" + selectedElement.getName (), typeof(Sprite)) as Sprite;
        saveElement();
        
        counter = time;
	}
    
    public void saveElement() {
        GameInit.setPlayerElement (playerNum, elementNum, selectedElement);
    }

}
