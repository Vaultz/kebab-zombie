using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour {

	private Transform player;
	public GameObject bullet;
	public float range;
	private bool coolDown = false;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Sword") 
		{
			Destroy (gameObject);

		}
	}

	void Start(){
		player = GameObject.Find ("Player").GetComponent<Transform>();
	}

	void Update()
	{
		if (!coolDown && (transform.position - player.position).magnitude < range) 
		{
			castBullet ();
			coolDown = true;
			StartCoroutine (CoolDownCoroutine ());
		}


	}

	void castBullet ()
	{
		player = GameObject.Find ("Player").GetComponent<Transform>();
		GameObject newBullet = Instantiate (bullet, transform.position, Quaternion.identity);
		newBullet.GetComponent<bulletBehaviour> ().direction = -(transform.position - player.position).normalized;
	}

	IEnumerator CoolDownCoroutine ()
	{
		yield return new WaitForSeconds (2f);
		coolDown = false;
	}

}
