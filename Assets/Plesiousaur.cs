using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plesiousaur : MonoBehaviour {

	public GameObject prompt;
	public GameObject cutscene;
	public GameObject player;
	public GameObject boss;
	public PlayerBehaviour pb;
	public Movement movement;
	public GameObject itemBar;
	public List<GameObject> items;
	public Sprite escudo;
	public Sprite tocha;
	public Sprite amuleto;
	public Sprite espada;
	public bool shownCutscene;
	public bool hasSelected;

	void Start(){
		pb = player.GetComponent<PlayerBehaviour>();
		movement = player.GetComponent<Movement>();
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag.Equals("Player")){
			prompt.SetActive(true);
			GetComponent<BoxCollider>().enabled=false;
		}
		items=new List<GameObject>();
		for(int i=0;i<itemBar.transform.childCount;i++){
			items.Add(itemBar.transform.GetChild(i).gameObject);
		}
	}

	void Update(){
		if(prompt.activeSelf){
			pb.doingWindup=true;
			if(!hasSelected){
				if(Input.GetKeyDown(KeyCode.Alpha1)||Input.GetKeyDown(KeyCode.Keypad1)){
					cutscene.GetComponent<Image>().sprite=amuleto;
					movement.amuletBonus=1;
					Destroy(items[0].gameObject);
					hasSelected=true;
				}else if(Input.GetKeyDown(KeyCode.Alpha2)||Input.GetKeyDown(KeyCode.Keypad2)){
					cutscene.GetComponent<Image>().sprite=escudo;
					pb.hasEscudo=false;
					Destroy(items[1].gameObject);
					hasSelected=true;
				}else if(Input.GetKeyDown(KeyCode.Alpha3)||Input.GetKeyDown(KeyCode.Keypad3)){
					cutscene.GetComponent<Image>().sprite=espada;
					pb.hasEspada=false;
					Destroy(items[2].gameObject);
					hasSelected=true;
				}else if(Input.GetKeyDown(KeyCode.Alpha4)||Input.GetKeyDown(KeyCode.Keypad4)){
					cutscene.GetComponent<Image>().sprite=tocha;
					pb.hasTocha=false;
					Destroy(items[3].gameObject);
					hasSelected=true;
				}
			}else if(!shownCutscene){
				prompt.GetComponent<CanvasGroup>().alpha-=Time.deltaTime;
				cutscene.GetComponent<CanvasGroup>().alpha+=Time.deltaTime;
				if(cutscene.GetComponent<CanvasGroup>().alpha>=1){
					shownCutscene=true;
				}
			}else if(shownCutscene){
				pb.doingWindup=false;
				cutscene.GetComponent<CanvasGroup>().alpha-=Time.deltaTime/3;
				if(cutscene.GetComponent<CanvasGroup>().alpha<=0){
					Destroy(boss);
					prompt.SetActive(false);
				}
			}else{
				Debug.LogError("SOMETHING CUTSCENEY WENT WRONG!");
			}
		}
	}
}
