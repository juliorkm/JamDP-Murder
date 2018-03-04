using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

	public static bool firstEnter = true;
	public Animator introAnimator;

	void Start () {
		resetPlayerPrefs();
		
		if (!firstEnter) {
			introAnimator.GetComponentInChildren<GenericAnimationFunctions>().PlayIntro();
			introAnimator.gameObject.SetActive(false);
		}

		firstEnter = false;
	}

	void resetPlayerPrefs() {
        PlayerPrefs.SetInt("playerDown", 0);
        PlayerPrefs.SetInt("playerUp", 0);
    }	
}
