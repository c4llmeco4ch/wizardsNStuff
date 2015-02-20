using UnityEngine;
using System.Collections;

public class Fire : Element {

	public Fire(){
		setDmg(2);
		setCast(3);
		setSpd(4);
		setDot(true,2,5);
		setRange(4);
		setKnock(1);
		setMana(3);
		setDotN("burn");
		setName("Fire");
	}
	
}
