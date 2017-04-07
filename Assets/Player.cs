using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public ParticleSystem attackUp;
	public ParticleSystem attackBack;
	public ParticleSystem attackLeft;
	public ParticleSystem attackRight;

	private Camera currentCamera;
	private Animator animPlayer;
	private Vector2 tempPosition;


	void Start() {

		currentCamera = GameObject.Find ("Main Camera").gameObject.GetComponent<Camera>() ;

		//hidden collider player (attack collider)
		animPlayer = GetComponent<Animator> ();
		BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
		foreach( BoxCollider2D comp in colliders )
		{
			if (comp.name != "personnage") {
				comp.enabled = false ;

			}
		}

	}



	// Update is called once per frame
	void Update () {

		Vector2 playerPosition, cameraPosition;
		cameraPosition = currentCamera.transform.position;
		playerPosition = (Vector2) gameObject.transform.position;

		if (!(gameObject.GetComponentInChildren<SpriteRenderer> ().isVisible)) {


			// first movement
			if (cameraPosition.x == 0 && cameraPosition.y == 0) {
				
				//Debug.Log ("Premier mouvement");
				// update the camera position
				//if (playerPosition.x != 0 || playerPosition.y != 0) {
					// get the player & camera position
					// move the camera
					Debug.Log ("N MOUVEMENT : PLAYERPOS : " + playerPosition + " ; CAMERAPOS : " + cameraPosition);
					
					// if (valeurabsolue)player.x > (valeurabsolue)player.y : décalage sur l'axe X
					if (Mathf.Abs (playerPosition.x) > Mathf.Abs (playerPosition.y)) {
						
						if (playerPosition.x > cameraPosition.x) {
							Debug.Log ("Décalage vers la droite ");
							
							currentCamera.transform.position += Vector3.right * 20f;
							//StartCoroutine(MoveCamera(currentCamera.transform.position + 20*Vector3.right));
						} else if (playerPosition.x < cameraPosition.x) {
							Debug.Log ("Décalage vers la gauche ");
							
							currentCamera.transform.position += Vector3.left * 20f;
							//StartCoroutine(MoveCamera(currentCamera.transform.position + 20*Vector3.left));
						}
					} else {
						if (playerPosition.y > cameraPosition.y) {
							Debug.Log ("Décalage vers le haut ");
							
							currentCamera.transform.position += Vector3.up * 20f;
							//StartCoroutine(MoveCamera(currentCamera.transform.position + 20*Vector3.up));
						} else if (playerPosition.y < cameraPosition.y)  {
							Debug.Log ("Décalage vers le bas ");
							
							currentCamera.transform.position += Vector3.down * 20f;
							//StartCoroutine(MoveCamera(currentCamera.transform.position + 20*Vector3.down));
						}
						
					}
				//}
			}

			// other movements : we compare the old position with the new one
			else {
				//Debug.Log ("OLD : " + tempPosition + " ; NEW : " + playerPosition);

				float newX = Mathf.Abs (tempPosition.x) - Mathf.Abs (playerPosition.x);
				float newY = Mathf.Abs (tempPosition.y) - Mathf.Abs (playerPosition.y);
				//Debug.Log ("ABS X :" + Mathf.Abs(newX) + " ABS Y : " + Mathf.Abs(newY));
				//Debug.Log(tempPosition);
				if (Mathf.Abs(newX) > Mathf.Abs(newY)) {
					Debug.Log ("DECALAGE X");

					if (playerPosition.x > tempPosition.x) {
						Debug.Log ("X+");
						currentCamera.transform.position += Vector3.right * 20f;
					} 
					else if (playerPosition.x < tempPosition.x) {
						Debug.Log ("X-");
						currentCamera.transform.position += Vector3.left * 20f;
					}
				} else {
					Debug.Log ("DECALAGE Y");

					if (playerPosition.y > tempPosition.y) {
						Debug.Log ("Y+");
						currentCamera.transform.position += Vector3.up * 20f;
					} 
					else if (playerPosition.y < tempPosition.y) {
						Debug.Log ("Y-");
						currentCamera.transform.position += Vector3.down * 20f;
					}
				}

			}
			tempPosition = playerPosition;
		}


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
			attackUp.Play ();
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animPlayer.SetBool ("attackTop", false);
		}

		//ATTACK BOTTOM
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			StartCoroutine (GestionCollider ("colider-bottom"));
			animPlayer.SetBool ("attackBottom", true);
			attackBack.Play ();
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			animPlayer.SetBool ("attackBottom", false);
		}

		//ATTACK LEFT
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			StartCoroutine (GestionCollider ("colider-left"));
			animPlayer.SetBool ("attackLeft", true);
			attackLeft.Play ();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			animPlayer.SetBool ("attackLeft", false);
		}

		//ATTACHE RIGHT
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			StartCoroutine (GestionCollider ("colider-right"));
			animPlayer.SetBool ("attackRight", true);
			attackRight.Play ();
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
