using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	public Sprite idle;
	public RuntimeAnimatorController andando;
	public RuntimeAnimatorController atacando;

	Animator an;
	SpriteRenderer sr;

	PlayerBehaviour playerBehaviour;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		an = GetComponent<Animator>();
		playerBehaviour = GetComponent<PlayerBehaviour>();
		rb = GetComponent<Rigidbody>();
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		an.enabled=true;
		if(playerBehaviour.isAttacking){
			an.runtimeAnimatorController = atacando;
		}else{
			if(rb.velocity.x!=0){
				an.runtimeAnimatorController = andando;
			}else{
				an.enabled=false;
				sr.sprite = idle;
			}

		}
	}
}
