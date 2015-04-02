using UnityEngine;
using System.Collections;

public class Water : Element {
	
	public Water(){
		setDmg(0);
		setCast(0);
		setSpd(0);
		setDot(true,0,5);
		setRange(0);
		setKnock(0);
		setMana(0);
		setDuration(-2);
		setDotN("wet");
		setName("Water");
        setColor(new Color(0.1568627451f, 0.6078431373f, 0.8941176471f));
	}
}
