using UnityEngine;
using System.Collections;

public class Air : Element {

	// Use this for initialization
	public void Awake () {
		setDmg(1);
		setCast(5);
		setSpd(5);
		setDot(false,0,0);
		setRange(5);
		setMana(5);
		setDotN("none");
		setName("air");
	}
	

}
