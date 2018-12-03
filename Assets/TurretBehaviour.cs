using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

	public GameObject shot;
	public Vector3 direction;
	public float shotCooldown;
	public float duration;
	public float shotTimer;
	public float shotVelocity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		shotTimer-=Time.deltaTime;
		if(shotTimer<=0){
			GameObject newShot = Instantiate(shot, transform.position, Quaternion.identity);
			newShot.GetComponent<Rigidbody>().velocity=direction*shotVelocity;
			Destroy(newShot, duration);
			shotTimer=shotCooldown;
		}
	}
}
