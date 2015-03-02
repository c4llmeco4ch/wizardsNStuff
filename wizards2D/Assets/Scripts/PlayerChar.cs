using UnityEngine;
using System;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PlayerChar : MonoBehaviour {
    public int hp; //the player's current hp
    public float mana; //the player's current mana
    public int es; //"0" if no element is stored, 1=fire, 2=water, 3=earth, 4=wind
    private int maxHp; //player's max hp
    private float maxMana; //player's max mana
    public float moveForce = 300f; // Amount of force added to move the player left and right.
    public float maxSpeed = 6f;	// The fastest the player can travel in the x axis.
    public bool jump; //true if player is currently jumping?
    public int dotVal; //damage taken per second
    public int dotT; //"0" when player is not burned or poisoned, else duration of DoT
    public bool isDead; //true if player is dead, else false
    public int stunT; //"0" when player is not stunned, else duration of stun remaining
    public bool facingRight; //true if sprite is facing right
    private bool block; //true if player is currenty blocking
    public bool casting; //true if player is in casting animation
    public int playerNum; //the player and controller number
    public Animator anim; //all the animations this char can have
    public GameObject[] spells; //list of spells player has access to; 0=slash, 1=missile, 2=wall
    public Element[] elements; //list of elements player has access to
    public GameObject elementLoaded; 
    public Image healthBar;
    public Image manaBar;
    private RectTransform healthBarTrans;
    private RectTransform manaBarTrans;
    const float healthSize = 240f / 100; //size of 1 health unit in terms of health bar
    const float manaSize = 204f / 100; //size of 1 mana unit in terms of mana bar
    public Image elementL; 
    public Image elementR;
    public bool charging; //whether the player is currently charging a spell
    public int chargeLeft; //how many frames till the charge ends

    //instantiate new instance of player char. @param playerNum determines start location
    public PlayerChar() {
        casting = false;
        maxHp = 100;
        maxMana = 100;
        es = 0;
        jump = false;
        dotVal = 0;
        hp = maxHp;
        mana = maxMana;
        isDead = false;
        stunT = 0;
        playerNum = 1;
        spells = new GameObject[4];
        elements = new Element[2];
    }

    public void Awake() {
        if(playerNum > 2 && GameInit.playerNum <= 2) {
            this.gameObject.SetActive(false);
            return;
        }
        Debug.Log("Player Num: "+playerNum);
        GameInit.players [playerNum - 1] = this;
        facingRight = true;
//		GameInit i = new GameInit ();
        elements [0] = GameInit.getPlayerElement(playerNum, 0); //new Earth();
        elements [1] = GameInit.getPlayerElement(playerNum, 1);//new Air();

        healthBarTrans = healthBar.GetComponent<RectTransform>() as RectTransform;
        manaBarTrans = manaBar.GetComponent<RectTransform>() as RectTransform;

//		elementL.s
        elementL.sprite = Resources.Load("UI Art Assets/" + elements [0].getName() + "-Element", typeof(Sprite)) as Sprite;
        elementR.sprite = Resources.Load("UI Art Assets/" + elements [1].getName() + "-Element", typeof(Sprite)) as Sprite;
		
		
        /*
		* instantiate missile for player
		*/
		
        /*
		* instantiate wall for player
		*/
    }

    // Update is called once per frame
    public void Update() {
        if (!isDead) {
            if(anim.GetCurrentAnimationClipState(0)!= null) {
                if(anim.GetCurrentAnimationClipState(0)[0].clip.name == "Cast Slash")
                    anim.SetBool("Slash", false);
                if (anim.GetCurrentAnimationClipState(0)[0].clip.name == "Cast Missile")
                    anim.SetBool("Missile", false);
            }
            float lt = XCI.GetAxis(XboxAxis.LeftTrigger, playerNum); //true if left trigger is pushed, else false
            float rt = XCI.GetAxis(XboxAxis.RightTrigger, playerNum); //true if right trigger is pushed, else false
						

            //Debug.Log (lt+" || "+rt);
            regenMana();
            if (mana > 100)
                mana = 100;
            //Code for casting Slash
            if (XCI.GetButtonDown(XboxButton.X, playerNum)) {
//                elementLoaded.SetActive(false);
                if (!(rt > .5 || lt > .5) || casting) {
                    //Actually, punch, but for now, nothing
                    //Debug.Log ("I'm here");
                } else {
                    bool justMade = false;
                    if (spells [0] == null) {
                        slashMaker();
                        //Slash s=spells[0].GetComponent("Slash") as Slash;
                        justMade = true;
                        //s.prepSlash(this,spells[0]);
                    }
                    //			Debug.Log(spells[0].transform.position+" HI");
                    Slash slash = spells [0].GetComponent("Slash") as Slash;
                    if (lt > .5)
                        slash.infuse(elements [0]);
                    else if (rt > .5)
                        slash.infuse(elements [1]);
                    slash.sound.Play();	
                    Debug.Log("Slash's Element is " + slash.getElement().getName());
                    if (mana < slash.getMana()) {
                        playNoMana();
                        slash.kill();
                    }
                    else {
                        reduceMana(slash);
                        casting = true;
                        if (slash.casting) {
                            return;
                        }
						slash.charge();
				
                        if (slash.facingRight && !facingRight)
                            slash.Flip();
                        else if (!slash.facingRight && facingRight)
                            slash.Flip();
                    }
                }
            }
			//Code for casting Missile
			else if (XCI.GetButtonDown(XboxButton.B, playerNum)) {
                Debug.Log("FIRE DA MISSILES");
                if (!(rt > .5 || lt > .5) || casting) {
                    //Actually, punch, but for now, nothing
                    //Debug.Log ("I'm here");
                } else {
                    bool justMade = false;
                    if (spells [1] == null) {
                        missileMaker();
                        //Slash s=spells[0].GetComponent("Slash") as Slash;
                        //justMade = true;
                        //s.prepSlash(this,spells[0]);
                    }
                    //			Debug.Log(spells[0].transform.position+" HI");
                    Missile missile = Instantiate(spells [1].GetComponent("Missile")) as Missile;
                    if (lt > .5)
                        missile.infuse(elements [0]);
                    else if (rt > .5)
                        missile.infuse(elements [1]);
                    missile.sound.Play();	
                    Debug.Log("Missile's Element is " + missile.getElement().getName());
                    if (mana < missile.getMana()){
                        playNoMana();
                        missile.kill();
                    }
                    else {
                        reduceMana(missile);
                        casting = true;
                        if (missile.casting) {
                            return;
                        }
                      
						missile.charge();

                        if (missile.facingRight && !facingRight)
                            missile.Flip();
                        else if (!missile.facingRight && facingRight)
                            missile.Flip();
                    }
                }
            } //else {
//            if (!(rt > .5 && lt > .5) && !casting) {
//										
//                if (lt > .5 || rt > .5) 
//                    elementLoaded.SetActive(true);
//                else
//                    elementLoaded.SetActive(false);
//            } else
//                elementLoaded.SetActive(false);
            //}
        }
        healthBarTrans.sizeDelta = new Vector2(hp * healthSize, healthBarTrans.sizeDelta.y);
        manaBarTrans.sizeDelta = new Vector2(mana * manaSize, manaBarTrans.sizeDelta.y);
    }
	
    public void slashMaker() {
        GameObject slash = Instantiate(Resources.Load("Prefabs/Slash", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Slash s = slash.GetComponent("Slash") as Slash;
        s.prepSlash(this, slash);
        //slash.transform.Translate(s.cast(),Space.World);
        spells [0] = slash;
//		Debug.Log("-.-");
    }
	
    public void missileMaker() {
        GameObject missile = Instantiate(Resources.Load("Prefabs/Missile", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Missile m = missile.GetComponent("Missile") as Missile;
        m.prepMissile(this);
		m.GetComponent<MeshRenderer>().enabled=false;
		m.GetComponent<BoxCollider>().enabled=false;
        //slash.transform.Translate(s.cast(),Space.World);
        spells [1] = missile;
        //		Debug.Log("-.-");
    }

    // FixedUpdate is called once per physics step 
    public void FixedUpdate() {
        if (!isDead) {
            // Cache the contoller input input.
            if (casting)
                return;
            float rawHorizontal = XCI.GetAxis(XboxAxis.LeftStickX, playerNum);
            float rawVertical = XCI.GetAxis(XboxAxis.LeftStickY, playerNum);
            rawHorizontal = (rawHorizontal * 0.35f);
            Vector3 direction = new Vector3(rawHorizontal, 0f, rawVertical);
            float speed = (direction).magnitude;


            //		Debug.Log ("Test speed: "+speed);
            if ((speed * rigidbody.velocity).magnitude < maxSpeed)
                rigidbody.AddForce(direction * moveForce);

            //		if (rigidbody.velocity.magnitude > maxSpeed)
            //						rigidbody.velocity = direction * maxSpeed;

            anim.SetFloat("Speed", speed * 5);

            if (rawHorizontal < 0 && facingRight)
                Flip();
            else if (rawHorizontal > 0 && !facingRight)
                Flip();
        }

    }

    public void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
		
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //check if the amount of incoming damage is greater than the player's current hp. if so, player is die
    //else, subtract player's hp by damage
    public void takeDamage(int dmg, Spell s) {
        Debug.Log("Taking " + dmg + " Damage as " + playerNum);
        int bd;
        if (hp == 0) {
            return;
        }
        if (!block)
            bd = dmg;
        else if (s is Slash && block)
            bd = dmg / 2;
        else
            bd = dmg - (dmg / 4);
        if (bd >= hp) {
            isDead = true;
            hp = 0;
            kill();
        } else {
            hp -= bd;
            Debug.Log("Player " + playerNum + ": " + hp);
        }
        if (s.getDotB()) {
            dotVal = s.getDotV();
            dotT = s.getDotT();
        }
    }
	
    public void setBlock(bool b) {
        block = b;
    }

    public bool getBlock() {
        return block;
    }
	
    public void reduceMana(Spell s) {
        mana -= s.getMana();
    }
	
    public void kill() {
        isDead = true;
        anim.SetBool("Dead", true);
        transform.Rotate(0, 0, 90);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
	
    public void regenMana() {
        if (mana < maxMana)
            mana += (float).075;
    }
	
    public void stunned(int d) {
        if (d <= stunT)
            return;
        else {
            stunT = d;
        }
    }
    public void playNoMana() {
        GetComponent<AudioSource>().Play();
    }
}
