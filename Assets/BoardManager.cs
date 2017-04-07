using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public List<GameObject> regularRooms;
	private List<GameObject> placedRooms; // rooms already placed on the grid
	public List<GameObject> specialRooms; // boss room, chest room, and other unique rooms

	private float xDistance;
	private float yDistance;

	private GameObject placedRoom;
	private Vector2 roomPositionToCompare;
	private List<char> cardinalPoints;

	// Use this for initialization
	void Start ()
	{
		xDistance = 20;
		yDistance = 20;
		cardinalPoints = new List<char> ();
		cardinalPoints.Add ('n');
		cardinalPoints.Add ('s');
		cardinalPoints.Add ('e');
		cardinalPoints.Add ('o');
		
		GenerateRooms ();
		GenerateDoors ();
	}

	private void GenerateRooms ()
	{
		//Number of regular rooms to place (primar for loop)
		int nbOfRegularRooms;
		nbOfRegularRooms = 8;

		// Creating the list of rooms we're gonna place
		placedRooms = new List<GameObject> ();

		// entryRoom
		Debug.Log("On instancie une "+regularRooms[0]);
		GameObject lastRoom = Instantiate (regularRooms[0], Vector2.zero, Quaternion.identity, transform); 	// transform : global position, rotation & scale
		//if(lastRoom != null) Debug.Log("1 room created!");
		lastRoom.name = ("i : " + 0);
		placedRooms.Add (lastRoom);
		//if(placedRooms[0] != null) Debug.Log("1 Room added to the List");
		// Caching the count of regular rooms.
		int sizeOfRegularRooms = regularRooms.Count;

		// placing regular rooms
		for (int i = 1; i < nbOfRegularRooms; i++) 
		{
			// Recording the coordinates of the last placed room
			Vector2 currentRoomPosition = lastRoom.transform.position;
			//print(currentRoomPosition);
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

				//Debug.Log("Is there something " + cardinalPoint + " of the current room?");


				// ... and take the position of the room corresponding to this cardinal point
				currentRoomPosition = GetCurrentRoomPosition(currentRoomPosition, cardinalPoint);
				/*switch (cardinalPoint) {
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
				}*/
				//Debug.Log("After swtching the cardinal point, the current room position is: " + currentRoomPosition);	
				// 3. comparing the future position with all the existing rooms
				int nbOfPlacedRooms = placedRooms.Count;

				// Taking care of the second room...
				if (nbOfPlacedRooms == 1) 
				{
					//Debug.Log("No!");
					lastRoom = Instantiate (placedRoom, currentRoomPosition, Quaternion.identity,transform);

					//if(lastRoom != null) Debug.Log("2 room created!");
					lastRoom.name = ("i : " + 1);
					placedRooms.Add (lastRoom);
					//if(placedRooms[1] != null) Debug.Log("2 Room added to the List");

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
						//print (isTherePlace.ToString() + i.ToString() + j.ToString());


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

	// placing the doors to the existing rooms
	private void GenerateDoors() {
		GameObject currentRoom, comparedRoom = new GameObject ();
		GameObject doorToLock = new GameObject ();

		// first we browse all the rooms
		int nbOfPlacedRooms = placedRooms.Count; 
		for (int i = 0; i < nbOfPlacedRooms ; i++) {
			//Debug.Log("I check the room "+i);
			//for each room
			currentRoom = placedRooms [i];

			// then we check every cardinalPoint
			foreach (char cardinalPoint in cardinalPoints) {
				//Debug.Log("Let's see the "+cardinalPoint+" point...");
				Vector2 currentRoomPosition = (Vector2) currentRoom.transform.position;
				currentRoomPosition = GetCurrentRoomPosition (currentRoomPosition, cardinalPoint);

				// then we compare every other room position with our currentRoom position*cardinalPoint
				bool isTherePlace = true;
				for (int j = 0; j < nbOfPlacedRooms ; j++) {
					if(currentRoomPosition == (Vector2) placedRooms[j].transform.position) {
						//Debug.Log ("We need to open the door here !");
						isTherePlace = false;


						Destroy(currentRoom.transform.FindChild ("door-"+cardinalPoint).gameObject);

					}
				
				}
			}

		}

	}


	// Get un new position for the currentRoom, depending on the given cardinalPoint
	private Vector2 GetCurrentRoomPosition(Vector2 currentRoomPosition, char cardinalPoint) {
		switch (cardinalPoint) {
			case 'n':
				return currentRoomPosition += yDistance * Vector2.up;


			case 's':
				return currentRoomPosition -= yDistance * Vector2.up;


			case 'e':
				return currentRoomPosition += xDistance * Vector2.right;


			case 'o':
				return currentRoomPosition -= xDistance * Vector2.right;


			default:
				//Debug.LogError ("You Shouldn't be there");
				return currentRoomPosition;
			}

	}

	private void Reroll ()
	{
		foreach (GameObject go in placedRooms)
		{
			Destroy(go);
		}
		GenerateRooms();
	}

	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}


}