using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stage : MonoBehaviour {
	/*
	//	Room[,] grid;
	int nbRegularRooms;

	public List<GameObject> regularRooms; 
	List<GameObject> specialRooms; // boss room, chest room, and other unique rooms
	List<GameObject> placedRooms; // rooms already placed on the grid

	public Stage(int nbRegularRooms) {
		//this.grid = new Room[nbRegularRooms, nbRegularRooms];

		this.nbRegularRooms = nbRegularRooms;
		this.regularRooms = new List<GameObject> ();
		this.specialRooms = new List<GameObject> ();
		this.placedRooms = new List<GameObject> ();
	}

	// Initialization
	void Awake() {

		//SetRegularRooms ();
		//SetSpecialRooms ();

		// placing the entrance at the middle of the grid
		//int coord = nbRegularRooms / 2;
		//grid [coord, coord] = startRoom ;

		EmptyRoom startRoom = new EmptyRoom ();
		//Instantiate (startRoom, Vector2.zero, Quaternion.identity);

		// placing the regular rooms on the grid
		for (int i = 0; i < nbRegularRooms; i++) {
		//	Instantiate(regularRooms[i], Vector2.up * 5f, Quaternion.identity);
		}

	}

	// fill the regular rooms list
	void SetRegularRooms () {
		List<GameObject> regularRoomTypes = new List<GameObject> ();
		regularRoomTypes.Add (new EmptyRoom());
		regularRoomTypes.Add (new LootRoom());
		regularRoomTypes.Add (new MonsterRoom());

		int regularRoomTypesSize = regularRoomTypes.Capacity;
		System.Random randomNbType = new System.Random ();
		// creating the regular rooms
		for (int i = 0; i < nbRegularRooms; i++) {
			GameObject randomRoom = regularRoomTypes[randomNbType.Next(0, regularRoomTypesSize)];
			regularRooms.Add(randomRoom);
		}
	}

	void SetSpecialRooms() {
		// creating the special rooms
		specialRooms.Add(new BossRoom());
		specialRooms.Add(new ChestRoom());
		specialRooms.Add(new ExitRoom());

	}

	// Called at first frame
	void Start () {
		Instantiate (salles[0], Vector2.zero, Quaternion.identity);
		Instantiate (salles[1], Vector2.up * 4f, Quaternion.identity);

	}

	// Update is called once per frame
	void Update () {

	}*/
}
