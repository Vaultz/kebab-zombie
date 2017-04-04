using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



	// Update is called once per frame
	void Update () {
		
		int horizontal = 0;
		int vertical = 0; 

		if (Input.GetKey (KeyCode.Z)) {		
			StartCoroutine (forward(0.05f));
		}

		if (Input.GetKey (KeyCode.S)) {
			StartCoroutine (back(0.05f));
		}

		if (Input.GetKey (KeyCode.D)) {
			StartCoroutine (right(0.05f));
		}

		if (Input.GetKey (KeyCode.Q)) {
			StartCoroutine (left(0.05f));
		}
	}

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
