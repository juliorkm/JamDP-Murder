using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;
    private int bullets = 0;
    public GameObject projectile_prefab;

    public void MovePlayerLeft() {
        if (player.canAct)
            player.movePlayer(PlayerDir.LEFT);
    }

    public void MovePlayerRight() {
        if (player.canAct)
            player.movePlayer(PlayerDir.RIGHT);
    }

    public void ShootProjectile(bool dir) {
        if (player.canAct) {
            player.canAct = false;
            GameObject go;
            if (dir == true) {
                go = Instantiate(projectile_prefab, player.transform.position, Quaternion.identity);
            } else {
                go = Instantiate(projectile_prefab, player.transform.position, Quaternion.Euler(0,0,180));
            }
            go.transform.SetParent(transform.parent, false);
            go.GetComponent<Projectile>().SerializeProjectile(dir, player.rectTransform.anchoredPosition);
            StartCoroutine(Cooldown(.8f));
        }
    }

    IEnumerator Cooldown(float time) {
        yield return new WaitForSeconds(time);
        player.canAct = true;
    }
}
