using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Animator animPlayer;

	void Start(){

		//hidden collider player (attack collider)
		animPlayer = GetComponent<Animator> ();
		BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
		foreach( BoxCollider2D comp in colliders )
		{
			comp.enabled = false ;
		}

	}


	void OnCollisionEnter2D(Collision2D coll) {

		print ("mob hit");

		if (coll.gameObject.tag == "Enemy") {
			Destroy (gameObject);

		}
	}


	// Update is called once per frame
	void Update () {


		// RUN 
		if (Input.GetKey (KeyCode.Z)) {		
			animPlayer.SetBool ("walkUp", true);
			StartCoroutine (forward(0.05f));
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			animPlayer.SetBool ("walkUp", false);
		}

		//BACK
		if (Input.GetKey (KeyCode.S)) {
			animPlayer.SetBool ("walkDown", true);
			StartCoroutine (back(0.05f));
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			animPlayer.SetBool ("walkDown", false);
		}

		//RIGHT
		if (Input.GetKey (KeyCode.D)) {
			animPlayer.SetBool ("WalkRight", true);
			StartCoroutine (right(0.05f));
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			animPlayer.SetBool ("WalkRight", false);
		}

		//LEFT
		if (Input.GetKey (KeyCode.Q)) {
			animPlayer.SetBool ("walkLeft", true);
			StartCoroutine (left(0.05f));
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			animPlayer.SetBool ("walkLeft", false);
		}


		//ATTACK TOP
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			StartCoroutine (GestionCollider ("colider-top"));
			animPlayer.SetBool ("attackTop", true);
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animPlayer.SetBool ("attackTop", false);
		}

		//ATTACK BOTTOM
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			StartCoroutine (GestionCollider ("colider-bottom"));
			animPlayer.SetBool ("attackBottom", true);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			animPlayer.SetBool ("attackBottom", false);
		}

		//ATTACK LEFT
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			StartCoroutine (GestionCollider ("colider-left"));
			animPlayer.SetBool ("attackLeft", true);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			animPlayer.SetBool ("attackLeft", false);
		}

		//ATTACHE RIGHT
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			StartCoroutine (GestionCollider ("colider-right"));
			animPlayer.SetBool ("attackRight", true);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			animPlayer.SetBool ("attackRight", false);
		}
	}

	//function for attack
	IEnumerator GestionCollider(string arrow){
		
		BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
		BoxCollider2D CurrentComp = null;

		foreach( BoxCollider2D comp in colliders )
		{
			if(comp.name==arrow){
				comp.enabled = true;
				CurrentComp = comp;
				break;
			}
		}	
		yield return new WaitForSeconds (0.1f);
		CurrentComp.enabled = false;
	}

	//function for move top
	IEnumerator forward(float time)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.up); 

		if (hit.distance > 1.5f || hit.collider == null) {
			while (time > 0) {
				time -= Time.deltaTime;
				transform.Translate (4 * Vector3.up * Time.deltaTime);

				yield return null;
			}
		}

	}

	//function for move bottome
	IEnumerator back(float time)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down); 

		if (hit.distance > 1.5f || hit.collider == null) {
			while (time > 0) {
				time -= Time.deltaTime;
				transform.Translate (4 * Vector3.down * Time.deltaTime);

				yield return null;
			}
		}
	}

	//function for move right
	IEnumerator right(float time)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right); 

		if (hit.distance > 1.5f || hit.collider == null) {
			while (time > 0) {
				time -= Time.deltaTime;
				transform.Translate (4 * Vector3.right * Time.deltaTime);

				yield return null;
			}
		}
	}

	//function for move left
	IEnumerator left(float time)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.left); 

		if (hit.distance > 1.5f || hit.collider == null) {
			while (time > 0) {
				time -= Time.deltaTime;
				transform.Translate (4 * Vector3.left * Time.deltaTime);

				yield return null;
			}
		}
	}
}
