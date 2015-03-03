using UnityEngine;
using System.Collections;

public class Fire : Element {

	public Fire(){
		setDmg(-2);
		setCast(-2);
		setSpd(2);
		setDot(true,2,5);
		setRange(1);
		setKnock(-2);
		setMana(0);
		setDotN("burn");
		setName("Fire");
	}
	
}
