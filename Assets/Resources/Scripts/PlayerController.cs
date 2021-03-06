﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Player player;
    public float reloadTime;
    public GameObject projectile_prefab;

    public Button shootButton;
    private RectTransform shootButtonIcon;
    private ButtonIconMovement shootButtonIconMovement;

    private Image cooldownClock;

    void Start() {
        shootButtonIconMovement = shootButton.GetComponentInChildren<ButtonIconMovement>();
        shootButtonIcon = shootButtonIconMovement.gameObject.GetComponent<RectTransform>();
    }

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
            shootButton.interactable = false;
            shootButtonIcon.localPosition = Vector2.zero;
            GameObject go;
            if (dir == true) {
                go = Instantiate(projectile_prefab, player.transform.position, Quaternion.identity);
            } else {
                go = Instantiate(projectile_prefab, player.transform.position, Quaternion.Euler(0,0,180));
            }
            go.transform.SetParent(transform.parent, false);
            go.GetComponent<Projectile>().SerializeProjectile(dir, player.rectTransform.anchoredPosition);
            AudioManager.instance.Play(AudioManager.instance.sfx_shot);
            StartCoroutine(Cooldown(.4f));
        }
    }

    public void ReloadAmmo() {
        if (player.canAct) {
            player.canAct = false;
            AudioManager.instance.Play(AudioManager.instance.sfx_reload);
            StartCoroutine(ReloadTime(reloadTime));
        }
    }

    IEnumerator Cooldown(float time) {
        for (int i = 0; i < 50; i++) {
            if (player != null) player.cooldownClock.fillAmount = 1 - i / 50f;
            yield return new WaitForSeconds(time / 50);
        }
        player.cooldownClock.fillAmount = 0f;
        player.canAct = true;
    }

    IEnumerator ReloadTime(float time) {
        for (int i = 0; i < 50; i++) {
            if (player != null) player.cooldownClock.fillAmount = 1 - i / 50f;
            yield return new WaitForSeconds(time / 50);
        }
        player.cooldownClock.fillAmount = 0f;
        player.canAct = true;
        shootButton.interactable = true;
        shootButtonIcon.localPosition = shootButtonIconMovement.nonPressedPosition;
    }
}
