using UnityEngine;
using System.Collections;

public class Earth : Element {
	
	public Earth(){
		setDmg(2);
		setCast(-2);
		setSpd(-1);
		setDot(true,0,5);
		setKnock(1);
		setRange(0);
		setMana(-1);
		setDotN("stun");
		setName("Earth");
	}
}
