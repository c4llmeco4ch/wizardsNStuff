using UnityEngine;
using System.Collections;

public class Punch : Spell {

	public PlayerChar p; //the player who is casting this punch
	public bool casting; //whether the punch is currently in effect
	public int framesLeft; //how many frames till the punch ends
	public bool facingRight; //true if sprite is facing right
	public int dmg;      //how much damage a given punch inflicts
	
	// Use this for initialization
	public void start () {
		casting = true;
		p.casting = true;
		this.transform.position=cast();
		p.anim.SetBool("isPunching",true);
		this.GetComponent<MeshRenderer>().enabled=true;
		this.GetComponent<BoxCollider>().enabled=true;
		framesLeft = 50;
		sound.Play();
	}
	
	public void prepPunch(PlayerChar pc){p=pc; facingRight=true;}
	
	public void Awake(){casting = false; dmg=5;}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if(!casting)
			return;
		else{	
			if(framesLeft==0){
				kill();
			}
			else if(framesLeft>0)
				framesLeft--;	
		}
	}
	
	override public void kill(){
		casting = false;
		p.casting=false;
		framesLeft=0;
		this.GetComponent<MeshRenderer>().enabled=false;
		this.GetComponent<BoxCollider>().enabled=false;
	}
	
	public void OnCollisionEnter(Collision c){
		Debug.Log ("Anything!!??!!"+c.gameObject.name);
		if(c.gameObject.tag.Equals("Player")){
			Debug.Log("Collision!!");
			PlayerChar pc=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			pc.takeDamage(this.getDmg(),this); //should I make a different method for punches and have this not be a spell?
		}
	}
	
	public Vector3 cast(){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		if(p.facingRight){
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=p.collider.bounds.size.x/2+x;
			return new Vector3((float)x,y,z);
		}
		else{
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=x-(p.collider.bounds.size.x/2);
			return new Vector3((float)x,y,z);
		}
	}
	
	override public void resetSpell(){}
	
	override public void versus(Spell s){}
}
