using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour {

	public float speed;
	private Transform player;
	public float range;
	private Transform myTransform;
	private Vector3 oldposition;
	private Animator animPlayer;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Sword") 
		{
			//print (coll.gameObject.name);
			Destroy (gameObject);

		}

		if (coll.gameObject.tag == "Player") {
			print (coll.gameObject.name);
			Destroy (coll.gameObject.transform.parent.gameObject);

		}
	}

	void Start(){
	
		player = GameObject.Find ("Player").GetComponent<Transform>();
		myTransform = GetComponent<Transform> ();
		oldposition = myTransform.position;
		animPlayer = GetComponent<Animator> ();
	}

	void Update(){

		Vector2 directionMob = (myTransform.position - player.position).normalized;
		if ((myTransform.position - player.position).magnitude < range) {
			float dotProductHorizontal = Vector2.Dot (directionMob, Vector2.right);
			float dotProductVertical = Vector2.Dot (directionMob, Vector2.up);

			myTransform.position += (-(myTransform.position - player.position).normalized) * speed;

			if (Mathf.Abs (dotProductHorizontal) > Mathf.Abs (dotProductVertical)) {
				//LEFT
				if (dotProductHorizontal >= 0) {
					if (!animPlayer.GetCurrentAnimatorStateInfo (0).IsName ("moveLeft"))
						animPlayer.SetTrigger ("moveLeft");
				}
				//RIGHT
				else {
					if (!animPlayer.GetCurrentAnimatorStateInfo (0).IsName ("moveRight"))
						animPlayer.SetTrigger ("moveRight");
				}
			} else {
				//BOTTOM
				if (dotProductVertical >= 0) {
					if (!animPlayer.GetCurrentAnimatorStateInfo (0).IsName ("moveBottom"))
						animPlayer.SetTrigger ("moveBottom");
				}
				//TOP
				else {
					if (!animPlayer.GetCurrentAnimatorStateInfo (0).IsName ("moveTop"))
						animPlayer.SetTrigger ("moveTop");
				}
			}
			//oldposition = myTransform.position;
		} 
		else 
		{
			animPlayer.SetTrigger ("idle");
		}
	}
}
