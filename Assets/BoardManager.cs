using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public List<GameObject> regularRooms;
	private List<GameObject> placedRooms; // rooms already placed on the grid
	public List<GameObject> specialRooms; // boss room, chest room, and other unique rooms

	private GameObject placedRoom;
	//private GameObject futureRoomObject;
	private Vector2 roomPositionToCompare;

	// Use this for initialization
	void Start () {
		int nbOfRegularRooms;
		nbOfRegularRooms = 8;
		placedRooms = new List<GameObject> ();
		List<char> cardinalPoints = new List<char> ();
		char randomCardinalPoint;

		cardinalPoints.Add ('n');
		cardinalPoints.Add ('s');
		cardinalPoints.Add ('e');
		cardinalPoints.Add ('o');


		// entryRoom
		GameObject lastRoom = Instantiate (regularRooms[0], Vector2.zero, Quaternion.identity, transform); 	// transform : global position, rotation & scale

		placedRooms.Add (lastRoom);

		int sizeOfRegularRooms = regularRooms.Count;

		// placing regular rooms
		for (int i = 1; i < nbOfRegularRooms; i++) 
		{
			// setting the future room up
			int randomInt = Random.Range(0, sizeOfRegularRooms);
			//futureRoomObject = regularRooms [randomInt];
			// TODO
			// 1. Choisir une randomPlacedRoom et récupérer sa position
			// 2. boucler sur les points cardinaux : récupérer la position d'une salle future située au <cardinalPoint> de la randomPlacedRoom
			// 3. boucler sur les autres placedRoom :
			// 4. si la position de la salle future (randomPlacedRoom.position*cardinalPoint) ne correspond à aucune position des salles existantes : on peut placer une nouvelle salle
			
			// 1. getting a random placed room
			randomInt = Random.Range(0, regularRooms.Count);
			placedRoom = regularRooms[randomInt];

			// 2. browsing the cardinalPoints and getting the position of the future room
			Vector2 currentRoomPosition = placedRoom.transform.position;

			// temporarily removing the room
			//if (placedRooms.Count>1) placedRooms.Remove (placedRoom);
			
			// TODO : real random cardinalPoints randomCardinalPoint = cardinalPoints [Random.Range (0, 3)];
			bool newRoomIsPlaced = false;

			while (!newRoomIsPlaced) {

				char cardinalPoint = cardinalPoints [Random.Range(0, 4)];
				switch (cardinalPoint) {
				// getting the position of the future room 
				case 'n':
					currentRoomPosition += 200 * Vector2.up;
					break;
					
				case 's':
					currentRoomPosition -= 200 * Vector2.up;
					break;
					
				case 'e':
					currentRoomPosition.Set (currentRoomPosition.x * (-150), currentRoomPosition.y);
					break;
					
				case 'o':
					currentRoomPosition.Set (currentRoomPosition.x * 150, currentRoomPosition.y);
					break;
					
				default:
					Debug.LogError ("You Shouldn't be there");
					break;
				}
				Debug.Log (currentRoomPosition);
			
				// 3. comparing the future position with all the existing rooms
				int nbOfPlacedRooms = placedRooms.Count;

				if (nbOfPlacedRooms == 1) {
					lastRoom = Instantiate (placedRoom, currentRoomPosition, Quaternion.identity, transform);
					placedRooms.Add (lastRoom);
					newRoomIsPlaced = true;
				} 
				else {
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
						/*
						print ("TURN "+i+" Future X : "+currentRoomPosition.x+" Y : "+currentRoomPosition.y + 
							"\n Placed X :"+roomPositionToCompare.x+" Y : "+roomPositionToCompare.y);
*/
						lastRoom = Instantiate (placedRoom, currentRoomPosition, Quaternion.identity, transform);
						lastRoom.name = ("i : " + i);
						placedRooms.Add (lastRoom); 
						newRoomIsPlaced = true;	

					} 
				}
			}

		}

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
