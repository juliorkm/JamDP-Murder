﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour {

    private RectTransform rectTransform;

	public int width;
	public int height;
    public int cellSize;
	public GameObject tile_prefab;
	public GameObject player_prefab;

	private GameObject[] tiles;

	void Start () {
        rectTransform = GetComponent<RectTransform>();
		generateGrid();
		spawnPlayers();
	}
	
	void generateGrid() {
		tiles = new GameObject[width*height];
		for (int i = 0; i < width*height; i++) {

			GameObject go = Instantiate(tile_prefab);
            go.transform.SetParent(gameObject.transform, true);
            float x = - rectTransform.rect.width / 2 + cellSize * (.5f + i % width);
            float y = rectTransform.rect.height / 2 - cellSize * (.5f + i / width);
            go.transform.localPosition = new Vector2(x, y);
            go.transform.localScale = Vector3.one;
            go.GetComponent<GridTile>().generateTile(i);
			tiles[i] = go;
		}
    }

	void spawnPlayers() {
		int player_up_tile = Random.Range(0, width); 
		int player_down_tile = Random.Range(width*height - width, width*height);

        Vector2 player_up_pos = tiles[player_up_tile].GetComponent<RectTransform>().localPosition;
		Vector2 player_down_pos = tiles[player_down_tile].GetComponent<RectTransform>().localPosition;

        GameObject pUp = Instantiate(player_prefab);
        pUp.name = "Player (Up)";
        pUp.transform.SetParent(transform.parent, true);
        pUp.transform.localPosition = Vector3.zero;
        pUp.transform.localScale = Vector3.one;
        pUp.transform.localRotation = Quaternion.Euler(0,0,180); //flipar o player de cima
        GameObject pDown = Instantiate(player_prefab);
        pDown.name = "Player (Down)";
        pDown.transform.SetParent(transform.parent, true);
        pDown.transform.localPosition = Vector3.zero;
        pDown.transform.localScale = Vector3.one;

        //pUp.GetComponent<RectTransform>().localPosition = tiles[player_up_tile].GetComponent<RectTransform>().localPosition;
        //pDown.GetComponent<RectTransform>().localPosition = tiles[player_down_tile].GetComponent<RectTransform>().localPosition;

        pUp.GetComponent<Player>().generatePlayer(player_up_tile, player_up_pos);
        pDown.GetComponent<Player>().generatePlayer(player_down_tile, player_down_pos);
	}
}
