using UnityEngine;
using System.Collections;

public class Punch : Spell {

	public PlayerChar p; //the player who is casting this punch
	public GameObject g;
	public bool casting; //whether the punch is currently in effect
	public int framesLeft; //how many frames till the punch ends
	public bool facingRight; //true if sprite is facing right
	public int cd;
	
	// Use this for initialization
	public void start () {
		g.transform.position=cast();
		casting = true;
		p.casting = true;
		p.anim.SetBool("isPunching",true);
		g.GetComponent<BoxCollider>().enabled=true;
		framesLeft = 19;
	}
	
	public void prepPunch(PlayerChar pc, GameObject g){p=pc; this.g=g; facingRight=true;cd=0;}
	
	public void Awake(){
		casting = false; 
		setDmg(2); 
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if(cd>0){
			cd--;
		}
		if (casting){
			if(framesLeft>0){
				framesLeft--;
			}
			if(framesLeft==0 && p.anim.GetBool("isPunching")){
				p.anim.SetBool("isPunching",false);
				casting=false;
				p.casting=false;
				cd=60;
				this.GetComponent<BoxCollider>().enabled=false;
			}
		}
	}
	
	override public void kill(){
		casting = false;
		p.casting=false;
		framesLeft=0;
		g.GetComponent<BoxCollider>().enabled=false;
		
	}
	
	public void OnCollisionEnter(Collision c){
		Debug.Log ("Anything!!??!!"+c.gameObject.name);
		if(c.gameObject.tag.Equals("Player")){
			Debug.Log("Collision!!");
			PlayerChar pc=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			pc.takeDamage(getDmg (),this); //should I make a different method for punches and have this not be a spell?
		}
	}
	
	public Vector3 cast(){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		if(p.facingRight){
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=p.collider.bounds.size.x/2+x+(float).75;
			return new Vector3((float)x+(getRange()/4),y,z);
		}
		else{
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=x-(float)1-(p.collider.bounds.size.x/2);
			return new Vector3((float)x-(getRange()/4),y,z);
		}
	}
	
	override public void resetSpell(){return;}
	
	override public void versus(Spell s){return;}
}
