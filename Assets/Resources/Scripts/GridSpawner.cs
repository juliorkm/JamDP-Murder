﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpawner : MonoBehaviour {

    private RectTransform rectTransform;

	public int width;
	public int height;
    public int cellSize;
	public GameObject tile_prefab;
	public GameObject player_prefab;

	[HideInInspector]
    public RectTransform[] tiles;

    public PlayerController[] playerControllers;

    public GameObject upperCpuLock, bottomCpuLock;

	void Start () {
        rectTransform = GetComponent<RectTransform>();
		generateGrid();
		spawnPlayers();
	}
	
	void generateGrid() {
		tiles = new RectTransform[width*height];
		for (int i = 0; i < width*height; i++) {

			GameObject go = Instantiate(tile_prefab);
            go.transform.SetParent(gameObject.transform, true);
            float x = - rectTransform.rect.width / 2 + cellSize * (.5f + i % width);
            float y = rectTransform.rect.height / 2 - cellSize * (.5f + i / width);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            go.transform.localScale = Vector3.one;
            go.GetComponent<GridTile>().generateTile(i);
			tiles[i] = go.GetComponent<RectTransform>();
		}
    }

	void spawnPlayers() {
		int player_up_tile = Random.Range(0, width); 
		int player_down_tile = Random.Range(width*height - width, width*height);

        Vector2 player_up_pos = tiles[player_up_tile].GetComponent<RectTransform>().anchoredPosition;
		Vector2 player_down_pos = tiles[player_down_tile].GetComponent<RectTransform>().anchoredPosition;

        if (ColorManager.upperPlayerColor != PlayerColor.CPU) upperCpuLock.SetActive(false);
        GameObject pUp = Instantiate(player_prefab);
        pUp.name = "Player (Up)";
        pUp.tag = "pUp";
        pUp.transform.SetParent(transform.parent, true);
        pUp.transform.localPosition = Vector3.zero;
        pUp.transform.localScale = Vector3.one;
        pUp.transform.localRotation = Quaternion.Euler(0,0,180); //flipar o player de cima
        pUp.GetComponent<Image>().color = ColorManager.colors[(int)ColorManager.upperPlayerColor];
        pUp.transform.GetChild(0).GetComponent<Image>().color = new Color(ColorManager.colors[(int)ColorManager.upperPlayerColor].r + .15f,
                                                            ColorManager.colors[(int)ColorManager.upperPlayerColor].g + .15f,
                                                            ColorManager.colors[(int)ColorManager.upperPlayerColor].b + .15f,
                                                            .5f);
        if (ColorManager.bottomPlayerColor != PlayerColor.CPU) bottomCpuLock.SetActive(false);
        GameObject pDown = Instantiate(player_prefab);
        pDown.name = "Player (Down)";
        pDown.tag = "pDown";
        pDown.transform.SetParent(transform.parent, true);
        pDown.transform.localPosition = Vector3.zero;
        pDown.transform.localScale = Vector3.one;
        pDown.GetComponent<Image>().color = ColorManager.colors[(int)ColorManager.bottomPlayerColor];
        pDown.transform.GetChild(0).GetComponent<Image>().color = new Color(ColorManager.colors[(int)ColorManager.bottomPlayerColor].r + .15f,
                                                            ColorManager.colors[(int)ColorManager.bottomPlayerColor].g + .15f,
                                                            ColorManager.colors[(int)ColorManager.bottomPlayerColor].b + .15f,
                                                            .5f);

        Player pUpPlayer = pUp.GetComponent<Player>();
        pUpPlayer.generatePlayer(player_up_tile, player_up_pos);
        pUpPlayer.grid = this;
        Player pDownPlayer = pDown.GetComponent<Player>();
        pDownPlayer.generatePlayer(player_down_tile, player_down_pos);
        pDownPlayer.grid = this;

        playerControllers[0].player = pUpPlayer;
        playerControllers[1].player = pDownPlayer;
    }
}
