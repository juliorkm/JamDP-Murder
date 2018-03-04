using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour {

	public int width;
	public int height;
	public GameObject tile_prefab;
	public GameObject player_prefab;

	private GameObject[] tiles;

	void Start () {
		generateGrid();
		StartCoroutine(spawnPlayers());
	}
	
	void generateGrid() {
		tiles = new GameObject[width*height];
		for (int i = 0; i < width*height; i++) {

			GameObject go = Instantiate(tile_prefab);
        	go.transform.SetParent(this.gameObject.transform, false);
			go.GetComponent<GridTile>().generateTile(i);
			tiles[i] = go;
		}
	}

	IEnumerator spawnPlayers() {
		int player_up_tile = Random.Range(0, width); 
		int player_down_tile = Random.Range(width*height - width, width*height);

		Vector2 player_up_pos = tiles[player_up_tile].transform.position;
		Vector2 player_down_pos = tiles[player_down_tile].transform.position;

		GameObject p1 = Instantiate(player_prefab);
		p1.transform.SetParent(this.gameObject.transform.parent, false);
		GameObject p2 = Instantiate(player_prefab);
		p2.transform.SetParent(this.gameObject.transform.parent, false);

		yield return new WaitForSeconds(0.4f);

		// p1.transform.position = tiles[player_up_tile].transform.position;

		p1.GetComponent<Player>().generatePlayer(player_up_tile, player_up_pos);
		// p2.GetComponent<Player>().generatePlayer(player_down_tile, player_down_pos);
	}
}
