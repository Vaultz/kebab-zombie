using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") 
		{
			Destroy (gameObject);

		}
	}
}
