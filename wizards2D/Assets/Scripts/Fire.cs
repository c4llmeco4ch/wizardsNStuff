using UnityEngine;
using System.Collections;

public class Fire : Element {

	public Fire(){
		setDmg(-2);
		setCast(-2);
		setSpd(1);
		setDot(true,2,5);
		setRange(1);
		setKnock(-2);
		setMana(0);
		setDotN("burn");
        setName("Fire");
        setColor(new Color(0.9137254902f, 0.4039215686f, 0.1803921569f));
	}
	
}
