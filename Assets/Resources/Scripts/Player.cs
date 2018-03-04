﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDir{
	LEFT, RIGHT
}

public class Player : MonoBehaviour {

    [HideInInspector]
    public bool canAct = true;

    private int tile_id;
    private RectTransform rectTransform;
    [HideInInspector]
    public GridSpawner grid;

	void Update() {
        if (canAct)
    		inputMovePlayer();
	}

    public void generatePlayer(int tile_id, Vector2 tile_pos) {
        rectTransform = GetComponent<RectTransform>();
        this.tile_id = tile_id;
		rectTransform.anchoredPosition = tile_pos;
    }

	void inputMovePlayer() {
		if (Input.GetKeyDown(KeyCode.D))
            movePlayer(PlayerDir.RIGHT);
		else if (Input.GetKeyDown(KeyCode.A))
			movePlayer(PlayerDir.LEFT);
	}

	public void movePlayer (PlayerDir dir) {
		if (dir == PlayerDir.LEFT) {
            if (tile_id % grid.width > 0) {
                StartCoroutine(PlayerMovement(grid.tiles[tile_id-1].anchoredPosition));
			    tile_id--;
            }
		} else if (dir == PlayerDir.RIGHT) {
            if (tile_id % grid.width < grid.width - 1) {
                StartCoroutine(PlayerMovement(grid.tiles[tile_id+1].anchoredPosition));
			    tile_id++;
            }
		}
	}

    IEnumerator PlayerMovement(Vector2 target) {
        canAct = false;
        while (Vector2.Distance(rectTransform.anchoredPosition, target) > 15) {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target, .3f);
            yield return new WaitForEndOfFrame();
        }
        rectTransform.anchoredPosition = target;
        yield return new WaitForSeconds(.05f); //cooldown
        canAct = true;
    }

}
