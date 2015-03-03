using UnityEngine;
using System.Collections;

public class Air : Element {

	public Air() {
		setDmg(-2);
		setCast(2);
		setSpd(2);
		setDot(false,0,0);
		setRange(2);
		setKnock(0);
		setMana(2);
		setDotN("none");
		setName("Air");
	}
	

}
