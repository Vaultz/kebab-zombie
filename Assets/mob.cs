using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour {

	public float speed;
	private Transform player;
	public float range;
	private Transform myTransform;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			Destroy (gameObject);

		}
	}

	void Start(){
	
		player = GameObject.Find ("Player").GetComponent<Transform>();
		myTransform = GetComponent<Transform> ();

	}

	void Update(){

		if ((myTransform.position - player.position).magnitude < range) 
		{
			myTransform.position += (-(myTransform.position - player.position).normalized) * speed;
		}
	}
}
