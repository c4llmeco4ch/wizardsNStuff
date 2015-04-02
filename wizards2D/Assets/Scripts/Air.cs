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
		setDuration(0);
		setDotN("none");
        setName("Air");
        setColor(new Color(0.7450980392f, 1f, 1f));
	}
	

}
