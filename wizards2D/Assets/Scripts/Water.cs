using UnityEngine;
using System.Collections;

public class Water : Element {
	
	public Water(){
		setDmg(3);
		setCast(3);
		setSpd(3);
		setDot(true,0,5);
		setRange(3);
		setKnock(5);
		setMana(3);
		setDotN("wet");
		setName("Water");
	}
}
