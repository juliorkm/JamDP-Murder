using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

	int id;

	public void generateTile(int i) {
		gameObject.name = "Tile " + i;
		
		id = i;
	}
}
