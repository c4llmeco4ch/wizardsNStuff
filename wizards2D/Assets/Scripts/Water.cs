using UnityEngine;
using System.Collections;

public class Water : Element {
	
	public Water(){
		setDmg(0);
		setCast(0);
		setSpd(0);
		setDot(true,0,5);
		setRange(0);
		setKnock(2);
		setMana(0);
		setDotN("wet");
		setName("Water");
	}
}
