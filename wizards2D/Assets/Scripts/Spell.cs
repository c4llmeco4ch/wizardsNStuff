using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {

	//defines how a given spell will fire
	public abstract void cast();
	
	//defines what happens when an object collides with a given spell
	public abstract void onCollide(Object collider);

	//defines spell changes when an element is put on a spell
	public abstract void infuse(Element e);
}
