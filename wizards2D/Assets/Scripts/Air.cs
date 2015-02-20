using UnityEngine;
using System.Collections;

public class Air : Element {

	public Air() {
		setDmg(1);
		setCast(5);
		setSpd(5);
		setDot(false,0,0);
		setRange(5);
		setKnock(3);
		setMana(5);
		setDotN("none");
		setName("Air");
	}
	

}
