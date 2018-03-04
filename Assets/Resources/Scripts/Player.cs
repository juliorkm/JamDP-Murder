using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDir{
	LEFT, RIGHT
}

public class Player : MonoBehaviour {

    private int tile_id;
    private RectTransform rectTransform;

	void Update() {

		//inputMovePlayer();
	}

    public void generatePlayer(int tile_id, Vector2 tile_pos) {
        rectTransform = GetComponent<RectTransform>();
        this.tile_id = tile_id;
		rectTransform.localPosition = tile_pos;
    }

	void inputMovePlayer() {
		if (Input.GetKeyDown(KeyCode.D))
            movePlayer(PlayerDir.RIGHT);
		else if (Input.GetKeyDown(KeyCode.A))
			movePlayer(PlayerDir.LEFT);
	}

	public void movePlayer (PlayerDir dir) {
		if (dir == PlayerDir.LEFT) {
			tile_id--;
		} else if (dir == PlayerDir.RIGHT) {
			tile_id++;
		}
	}

}
