using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	SpriteRenderer sr;
	public GameObject player;
	public Vector3 playerInput;
	public float jumpHeight;
	public float playerSpeed;
	public float gravityStrength;
	public float jumpCooldown;
	public float amuletBonus;
	private float jumpTimer;
	public float spawnFreeze;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rb = player.GetComponent<Rigidbody>();
		jumpTimer-=0.1f;
		if(PlayerBehaviour.gameTimer>spawnFreeze&&!GetComponent<PlayerBehaviour>().doingWindup){
			playerInput.x=Input.GetAxis("Horizontal");
			playerInput.y=Input.GetAxis("Jump");
		}else{
			playerInput.x=0;
			playerInput.y=0;
		}
		rb.velocity=(Vector3.right*playerInput.x*playerSpeed*amuletBonus)+Vector3.up*(rb.velocity.y-gravityStrength);
		MoveSprites(rb);
		RaycastHit hit;
		if(playerInput.y!=0&&Physics.Raycast(player.transform.position, Vector3.down, out hit, 0.5f)&&jumpTimer<0.1f&&PlayerBehaviour.gameTimer>spawnFreeze){
			if(!hit.collider.name.Contains("Turret")){
				jumpTimer=jumpCooldown;
				rb.AddForce(Vector3.up*jumpHeight, ForceMode.Impulse);
			}
		}
	}

	void MoveSprites(Rigidbody rb){
		if(rb.velocity.x<0){
			sr.flipX=true;
		}else if(rb.velocity.x>0){
			sr.flipX=false;
		}
	}
}
