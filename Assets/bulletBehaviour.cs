using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour {

	public Vector2 direction;
	public float speed;


	// Use this for initialization
	void OnEnable () 
	{
		StartCoroutine (DelayDeath ());
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (speed * direction);
	}

	IEnumerator DelayDeath ()
	{
		yield return new WaitForSeconds (2f);
		Destroy (gameObject);
	}


	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") 
		{
			Destroy (coll.gameObject.transform.parent.gameObject);

		}
		if (coll.gameObject.tag == "Untagged") 
		{
			Destroy (gameObject);

		}
	}
}
