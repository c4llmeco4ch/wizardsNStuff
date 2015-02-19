using UnityEngine;
using System.Collections;

public class Earth : Element {
	
	public Earth(){
		setDmg(5);
		setCast(1);
		setSpd(2);
		setDot(true,0,5);
		setKnock(4);
		setRange(3);
		setMana(2);
		setDotN("stun");
		setName("earth");
	}
}
