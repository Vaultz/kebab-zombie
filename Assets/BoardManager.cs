using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public List<GameObject> regularRooms;
	private List<GameObject> placedRooms; // rooms already placed on the grid
	public List<GameObject> specialRooms; // boss room, chest room, and other unique rooms

	public float xDistance;
	public float yDistance;

	private GameObject placedRoom;
	//private GameObject futureRoomObject;
	private Vector2 roomPositionToCompare;

	// Use this for initialization
	void Start ()
	{
		xDistance = 12;
		yDistance = 8;
		GenerateMap();
	}

	private void GenerateMap ()
	{
		//Number of regular rooms to place (primar for loop)
		int nbOfRegularRooms;
		nbOfRegularRooms = 8;

		// Creating the list of rooms we're gonna place
		placedRooms = new List<GameObject> ();

		// Creating a list of cardinal points
		List<char> cardinalPoints = new List<char> ();
		char randomCardinalPoint;

		cardinalPoints.Add ('n');
		cardinalPoints.Add ('s');
		cardinalPoints.Add ('e');
		cardinalPoints.Add ('o');


		// entryRoom
		GameObject lastRoom = Instantiate (regularRooms[0], Vector2.zero, Quaternion.identity, transform); 	// transform : global position, rotation & scale
		if(lastRoom != null) Debug.Log("1 room created!");

		placedRooms.Add (lastRoom);
		if(placedRooms[0] != null) Debug.Log("1 Room added to the List");
		// Caching the count of regular rooms.
		int sizeOfRegularRooms = regularRooms.Count;

		// placing regular rooms
		for (int i = 1; i < nbOfRegularRooms; i++) 
		{
			// Recording the coordinates of the last placed room
			Vector2 currentRoomPosition = lastRoom.transform.position;
			print(currentRoomPosition);
			// Picking a room between all regular rooms...
			int randomInt = Random.Range(0, sizeOfRegularRooms);
			randomInt = Random.Range(0, regularRooms.Count);
			placedRoom = regularRooms[randomInt];
			// ... Now you've got tour gameobject to instantiate, let's find where.

			bool newRoomIsPlaced = false;

			// If there's no room placed, the while loop will do its thing.
			while (!newRoomIsPlaced) {

				// We take a random cardinal point...
				char cardinalPoint = cardinalPoints [Random.Range(0, 4)];

				Debug.Log("Is there something " + cardinalPoint + " of the current room?");

				switch (cardinalPoint) {
				// ... and take the position of the room corresponding to this cardinal point
				case 'n':
					currentRoomPosition += yDistance * Vector2.up;
					break;

				case 's':
					currentRoomPosition -= yDistance * Vector2.up;
					break;

				case 'e':
					currentRoomPosition += xDistance * Vector2.right;
					break;

				case 'o':
					currentRoomPosition -= xDistance * Vector2.right;
					break;

				default:
					Debug.LogError ("You Shouldn't be there");
					break;
				}
				Debug.Log("After swtching the cardinal point, the current room position is: " + currentRoomPosition);	
				// 3. comparing the future position with all the existing rooms
				int nbOfPlacedRooms = placedRooms.Count;

				// Taking care of the second room...
				if (nbOfPlacedRooms == 1) 
				{
					Debug.Log("No!");
					lastRoom = Instantiate (placedRoom, currentRoomPosition, Quaternion.identity,transform);

					if(lastRoom != null) Debug.Log("2 room created!");

					placedRooms.Add (lastRoom);
					if(placedRooms[1] != null) Debug.Log("2 Room added to the List");

					newRoomIsPlaced = true;
				} 
				// ... and after that, all the others
				else 
				{
					// verifying if we can place the room
					//					roomPositionToCompare.x = 0;
					//					roomPositionToCompare.y = 0;

					bool isTherePlace = true;

					for (int j = 0;  j < nbOfPlacedRooms-1 ; j++) 
					{
						roomPositionToCompare = placedRooms [j].transform.position;
						isTherePlace = (currentRoomPosition != roomPositionToCompare);
						print (isTherePlace.ToString() + i.ToString() + j.ToString());


					}


					if (isTherePlace) {
						
						//print ("TURN "+i+" Future X : "+currentRoomPosition.x+" Y : "+currentRoomPosition.y + 
						//	"\n Placed X :"+roomPositionToCompare.x+" Y : "+roomPositionToCompare.y);

						lastRoom = Instantiate (placedRoom, currentRoomPosition, Quaternion.identity, transform);
						lastRoom.name = ("i : " + i);
						placedRooms.Add (lastRoom); 
						newRoomIsPlaced = true;	

					} 
				}

			}

		}
	}

	private void Reroll ()
	{
		foreach (GameObject go in placedRooms)
		{
			Destroy(go);
		}
		GenerateMap();
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R)) Reroll();
	}
}