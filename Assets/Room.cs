using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	private string type;
	public int x;
	public int y;

	public Room(string type, int x, int y) {
		this.type = type;
		this.x = x;
		this.y = y;
	}

	public string getType() {
		return this.type;
	}

	public int getX() {
		return this.x;
	}

	public int getY() {
		return this.y;
	}

}
