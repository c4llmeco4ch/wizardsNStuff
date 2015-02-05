using UnityEngine;
using System.Collections;

public interface Spell {//: MonoBehaviour {

	//defines how a given spell will fire
	void cast();
	
	//defines what happens when an object collides with a given spell
	void onCollide(Object collider);

	//defines spell changes when an element is put on a spell
	void infuse(Element e);
}
