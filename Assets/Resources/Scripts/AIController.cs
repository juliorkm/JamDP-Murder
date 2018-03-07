using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour {

    public bool direction; // True = upper player
    public PlayerController playerController;
    public Button leftButton, rightButton, loadButton, shootButton;
    private RectTransform leftButtonIcon, rightButtonIcon, loadButtonIcon, shootButtonIcon;
    private ButtonIconMovement leftButtonIconMovement, rightButtonIconMovement, loadButtonIconMovement, shootButtonIconMovement;

    void Start () {
        leftButtonIconMovement = leftButton.GetComponentInChildren<ButtonIconMovement>();
        leftButtonIcon = leftButtonIconMovement.gameObject.GetComponent<RectTransform>();
        rightButtonIconMovement = rightButton.GetComponentInChildren<ButtonIconMovement>();
        rightButtonIcon = rightButtonIconMovement.gameObject.GetComponent<RectTransform>();
        loadButtonIconMovement = loadButton.GetComponentInChildren<ButtonIconMovement>();
        loadButtonIcon = loadButtonIconMovement.gameObject.GetComponent<RectTransform>();
        shootButtonIconMovement = shootButton.GetComponentInChildren<ButtonIconMovement>();
        shootButtonIcon = shootButtonIconMovement.gameObject.GetComponent<RectTransform>();
        StartCoroutine(AILoop());
	}

    IEnumerator AILoop() {
        yield return new WaitForSeconds(Random.Range(1f, .5f));
        yield return Reload();

        while(true) {
            if (playerController.player.canAct) {
                yield return MoveLeft();
                yield return new WaitForSeconds(1f);
                yield return MoveRight();
                yield return new WaitForSeconds(1f);
                yield return MoveLeft();
                yield return new WaitForSeconds(1f);
                yield return Shoot();
                yield return new WaitForSeconds(1f);
                yield return Reload();
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator Reload() {
        loadButton.interactable = false;
        loadButtonIcon.localPosition = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(.12f, .2f));
        loadButton.interactable = true;
        loadButtonIcon.localPosition = loadButtonIconMovement.nonPressedPosition;
        playerController.ReloadAmmo();
    }

    IEnumerator MoveLeft() {
        leftButton.interactable = false;
        leftButtonIcon.localPosition = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(.12f, .2f));
        leftButton.interactable = true;
        leftButtonIcon.localPosition = leftButtonIconMovement.nonPressedPosition;
        playerController.MovePlayerLeft();
    }

    IEnumerator MoveRight() {
        rightButton.interactable = false;
        rightButtonIcon.localPosition = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(.12f, .2f));
        rightButton.interactable = true;
        rightButtonIcon.localPosition = rightButtonIconMovement.nonPressedPosition;
        playerController.MovePlayerRight();
    }
    
    IEnumerator Shoot() {
        shootButton.interactable = false;
        shootButtonIcon.localPosition = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(.12f,.2f));
        playerController.ShootProjectile(!direction);
    }
}
