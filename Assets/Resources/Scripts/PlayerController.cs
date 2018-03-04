using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;

    public void MovePlayerLeft() {
        if (player.canAct)
            player.movePlayer(PlayerDir.LEFT);
    }

    public void MovePlayerRight() {
        if (player.canAct)
            player.movePlayer(PlayerDir.RIGHT);
    }
}
