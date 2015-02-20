using UnityEngine;
using System.Collections;

public class Wall : Spell {
	public PlayerChar p; //the player who is casting this slash
	GameObject g;
	public bool casting; //whether the slash is currently in effect
	public int framesLeft; //how many frames till the spell ends
	public bool facingRight; //true if sprite is facing right
	
	// Use this for initialization
	void Start () {
		g.transform.position=cast();
		casting=true;
		framesLeft=(int)(getSpd()*50);
	}
	
	//call this immediately after creating this object
	public void prepWall(PlayerChar p,GameObject g){this.p=p; this.g=g; facingRight = true;}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if(!casting)
			return;
		else if(framesLeft==0){
			kill();
		}
		else if(framesLeft>0)
			framesLeft--;
	}
	
	public void Awake(){
		casting=false;
		resetSpell();
	}
	
	//defines how a given spell will fire
	override public Vector3 cast(){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		if(p.facingRight){
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=p.collider.bounds.size.x/2+x+(float).75;
			return new Vector3((float)x+(getRange()/2),y,z);
		}
		else{
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=x-(float)1-(p.collider.bounds.size.x/2);
			return new Vector3((float)x-(getRange()/2),y,z);
		}
	}
	
	//defines what happens when an object collides with a given spell
	public void OnCollisionEnter(Collision c){}
	
	//define what happens when a spell is finished
	override public void kill(){
		casting=false;
		framesLeft=0;
		g.SetActive(false);
		resetSpell();
	}
	
	//defines what happens when a spell collides with a different spell
	override public void versus(Spell s){
		if(s is Slash){
			if(getElement().getName()=="air"){
				if(s.getElement().getName()=="air"){
					//knockback, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//apply burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//blow away, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="fire"){
				if(s.getElement().getName()=="air"){
					//applies burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					this.kill();
					s.kill();
				}
				else{
					//earth hits, then...
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="water"){
				if(s.getElement().getName()=="air"){
					//apply wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				else{
					//earth is wet, then...
					this.kill();
					s.kill();
				}
			}
			else{
				if(s.getElement().getName()=="air"){
					//earth hits
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//earth wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//earth hits, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
		}
		else if(s is Wall){
			if(getElement().getName()=="air"){
				if(s.getElement().getName()=="air"){
					//knockback, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//apply burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//blow away, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="fire"){
				if(s.getElement().getName()=="air"){
					//applies burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					this.kill();
					s.kill();
				}
				else{
					//earth hits, then...
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="water"){
				if(s.getElement().getName()=="air"){
					//apply wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				else{
					//earth is wet, then...
					this.kill();
					s.kill();
				}
			}
			else{
				if(s.getElement().getName()=="air"){
					//earth hits
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//earth wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//earth hits, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
		}
	}
	
	//call every time after spell fades to reset base values
	override public void resetSpell(){
		setName("Wall");
		element=null;
		setDot(false,0,0);
		setDmg(0);
		setKnock(1);
		setMana(10);
		setRange(2);
		setSpd((float).5);
	}
}
