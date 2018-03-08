using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour {

    public bool direction; // True = upper player
    public PlayerController playerController, enemyController;
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
        yield return new WaitForSeconds(Random.Range(.05f, .15f));
        yield return Reload();

        while(true) {
            if (playerController.player.canAct) { // when not stunned
                if (shootButton.interactable) { // when has bullet
                    if (DistanceToEnemy() == 0) { // when is aligned with enemy
                        if (!enemyController.player.canAct) { // when enemy is stunned in front of it
                            if (FindObjectOfType<Projectile>() == null) { // and there's no dagger flying, 100% chance to shoot
                                yield return Shoot();
                            } else { // and there is a dagger flying
                                if (Random.Range(0f,1f) < .3f) { // 30% chance to shoot
                                    yield return Shoot();
                                } else { // 70% chance to move away
                                    yield return MoveRandom();
                                }
                            }
                        } else { // if enemy is in front but not stunned
                            if (Random.Range(0f, 1f) < .85f) { // 85% chance to shoot
                                yield return Shoot();
                            } else { // 15% chance to move or stay still
                                if (Random.Range(0f, 1f) < .3f) { // 30% (4.5%) chance to stay still
                                    yield return new WaitForSeconds(Random.Range(.4f,.6f));
                                } else { // 70% (10.5%) chance to move away
                                    yield return MoveRandom();
                                }
                            }
                        }
                    } else if (Mathf.Abs(DistanceToEnemy()) < 2) { // when is 1 tile away from enemy
                        if (!enemyController.player.canAct) { // when enemy is stunned a tile away from it
                            if (FindObjectOfType<Projectile>() == null) { // and there's no dagger flying
                                if (Random.Range(0f, 1f) < .8f) { // 80% chance to chase
                                    if (DistanceToEnemy() < 0) yield return MoveLeft();
                                    else yield return MoveRight();
                                } else { // 20% chance to stay still
                                    yield return new WaitForSeconds(Random.Range(.4f, .6f));
                                }
                            } else { // if there is a dagger flying
                                if (Random.Range(0f, 1f) < .5f) { // 50% chance to stay still
                                    yield return new WaitForSeconds(Random.Range(.4f, .6f));
                                } else { // 50% chance to move randomly
                                    yield return MoveRandom();
                                }
                            }
                        } else { // when enemy isn't stunned
                            if (Random.Range(0f, 1f) < .15f) { // 15% chance to shoot
                                yield return Shoot();
                            } else { // 85% chance to move or stay still
                                if (Random.Range(0f, 1f) < .7f) { // 70% (59.5%) chance to move randomly
                                    yield return MoveRandom();
                                } else { // 30% (25.5%) chance to stay still
                                    yield return new WaitForSeconds(Random.Range(.4f, .6f));
                                }
                            }
                        }
                    } else { //when enemy is 2 or more tiles away
                        if (Random.Range(0f, 1f) < .8f) { // 80% chance to chase
                            if (DistanceToEnemy() < 0) yield return MoveLeft();
                            else yield return MoveRight();
                        }
                    }
                } else { // when has no bullet
                    if (DistanceToEnemy() == 0) { // when is aligned with enemy
                        if (!enemyController.player.canAct) { // when enemy is stunned in front of it
                            if (FindObjectOfType<Projectile>() == null) { // and there's no dagger flying
                                if (Random.Range(0f,1f) < .85f) { // 85% chance to reload
                                    yield return Reload();
                                } else { // 15% chance to move randomly
                                    yield return MoveRandom();
                                }
                            } else { // and there is a dagger flying
                                if (Random.Range(0f,1f) < .85f) { // 85% chance to dodge
                                    yield return MoveRandom();
                                } else { // 15% chance to stay still
                                    yield return new WaitForSeconds(Random.Range(.4f, .6f));
                                }
                            }
                        } else { // if the enemy is in front but not stunned
                            if (Random.Range(0f,1f) < .85f) { // 85% chance to dodge
                                yield return MoveRandom();
                            } else { // 15% chance to stay still
                                yield return new WaitForSeconds(Random.Range(.4f, .6f));
                            }
                        }
                    } else if (Mathf.Abs(DistanceToEnemy()) < 2) { // when is 1 tile away from enemy
                        if (!enemyController.player.canAct) { // when enemy is stunned a tile away from it
                            if (FindObjectOfType<Projectile>() == null) { // and there's no dagger flying
                                if (Random.Range(0f,1f) < .75f) { // 75% chance to reload
                                    yield return Reload();
                                } else { // 25% chance to move randomly
                                    yield return MoveRandom();
                                }
                            } else { // if there is a dagger flying
                                if (Random.Range(0f,1f) < .9f) { // 90% chance to reload
                                    yield return Reload();
                                } else { // 10% chance to move randomly
                                    yield return MoveRandom();
                                }
                            }
                        } else { // when enemy isn't stunned
                            if (Random.Range(0f,1f) < .75f) { // 75% chance to move randomly
                                yield return MoveRandom();
                            } else { // 25% chance to move reload
                                yield return Reload();
                            }
                        }
                    } else { // when enemy is 2 or more tiles away, 100% chance to reload
                        yield return Reload();
                    }

                }
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

    IEnumerator MoveRandom() {
        if (playerController.player.tile_id % playerController.player.grid.width == 0)
            yield return MoveRight();
        else if (playerController.player.tile_id % playerController.player.grid.width == playerController.player.grid.width - 1)
            yield return MoveLeft();
        else {
            if (Random.Range(0f, 1f) < .5f)
                yield return MoveRight();
            else yield return MoveLeft();
        }
    }
    
    IEnumerator Shoot() {
        shootButton.interactable = false;
        shootButtonIcon.localPosition = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(.12f,.2f));
        playerController.ShootProjectile(!direction);
    }

    int DistanceToEnemy() {
        int i = Mathf.Abs(playerController.player.tile_id - enemyController.player.tile_id);
        int alignedDistance = playerController.player.grid.width * (playerController.player.grid.height - 1);
        if (direction) return i - alignedDistance;
        else return alignedDistance - i;
    }
}
