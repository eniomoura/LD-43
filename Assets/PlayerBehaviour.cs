using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	private List<GameObject> lives;
	public static float gameTimer;
	public GameObject lifeBar;
	public BoxCollider boxCollider;
	public Vector3 levelRespawnPoint;
	public Vector3 gameRespawnPoint;
	public Vector3 mapBoundariesMax;
	public Vector3 mapBoundariesMin;
	public Sprite attackingSprite;
	public Sprite attackedSprite;

	public bool hasEspada;
	public bool hasEscudo;
	public bool hasTocha;

	public bool isAttacking;
	public bool doingWindup;
	public float attackCooldown;
	public float attackDuration;
	public float attackTimer;
	private float slashTimer;

	// Use this for initialization
	void Start () {
		lives = new List<GameObject>();
		for(int i=0;i<lifeBar.transform.childCount;i++){
			Transform life = lifeBar.transform.GetChild(i);
			lives.Add(life.gameObject);
		}
		boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		boxCollider.enabled = false;
		CheckBoundaries();
		CheckAttack();
		gameTimer+=Time.deltaTime;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Enemy")){
			if(isAttacking){
				Destroy(col.gameObject);
			}else if(lives.Count<2&&lives.Count!=0){
				Destroy(lives[lives.Count-1].gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=gameRespawnPoint;
				Debug.Log("PERDEU O JOGO");
			}else{
				Destroy(lives[lives.Count-1].gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=levelRespawnPoint;
				gameTimer=0;
			}
		}
		if(col.gameObject.name.Contains("Shot")&&!hasEscudo){
			if(lives.Count<2&&lives.Count!=0){
				Destroy(lives[lives.Count-1].gameObject);
				Destroy(col.gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=gameRespawnPoint;
				Debug.Log("PERDEU O JOGO");
			}else{
				Destroy(lives[lives.Count-1].gameObject);
				Destroy(col.gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=levelRespawnPoint;
				gameTimer=0;
			}
		}
	}

	void CheckBoundaries(){
		Rigidbody rb = GetComponent<Rigidbody>();
		if(rb.position.x>mapBoundariesMax.x||rb.position.y>mapBoundariesMax.y||rb.position.z>mapBoundariesMax.z||rb.position.x<mapBoundariesMin.x||rb.position.y<mapBoundariesMin.y||rb.position.z<mapBoundariesMin.z){
			if(lives.Count<2&&lives.Count!=0){
				Destroy(lives[lives.Count-1].gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=gameRespawnPoint;
				Debug.Log("PERDEU O JOGO");
			}else{
				Destroy(lives[lives.Count-1].gameObject);
				lives.RemoveAt(lives.Count-1);
				transform.position=levelRespawnPoint;
				gameTimer=0;
			}
		}
	}

	void CheckAttack(){
		isAttacking=false;
		bool inputKeys=	Input.GetKey(KeyCode.S)||
						Input.GetKey(KeyCode.F)||
						Input.GetKey(KeyCode.LeftControl)||
						Input.GetKey(KeyCode.LeftShift);
		if(inputKeys&&GetComponent<Rigidbody>().velocity.x<0.1f){
			slashTimer=attackDuration;
		}
		if(slashTimer>0){
			isAttacking=true;
			boxCollider.enabled=true;
			slashTimer-=Time.deltaTime;
		}
		attackTimer-=Time.deltaTime;
	}
}
