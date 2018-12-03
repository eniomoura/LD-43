using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float jumpHeight;
	public float playerSpeed;
	public float gravityStrength;
	public float jumpCooldown;
	private float jumpTimer;

	public Sprite idle;
	public Vector3 enemyInput;
	public bool enemyIsJumping;
	public float aiClock;
	public float loopTime;

	// Use this for initialization
	void Start () {
		enemyInput=Vector3.left;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateEnemyInputs();
		Rigidbody rb = GetComponent<Rigidbody>();
		jumpTimer-=0.1f;
		rb.velocity=(Vector3.right*enemyInput.x*playerSpeed)+Vector3.up*(rb.velocity.y-gravityStrength);
		RaycastHit hit;
		if(enemyIsJumping&&Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f)&&jumpTimer<0.1f){
			jumpTimer=jumpCooldown;
			rb.AddForce(Vector3.up*jumpHeight, ForceMode.Impulse);
		}
	}

	void CalculateEnemyInputs(){
		Animator an = GetComponent<Animator>();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		an.enabled=true;
		if(aiClock<=0){
			aiClock=loopTime;
			enemyInput*=-1;
			sr.flipX=!sr.flipX;
		}
		if(GetComponent<Rigidbody>().velocity.x==0){
			an.enabled=false;
			sr.sprite=idle;
		}
		aiClock-=Time.deltaTime;
	}
}