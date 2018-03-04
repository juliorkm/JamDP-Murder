using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

	public static bool entered;
	public Animator introAnimator;

	void Start () {
		resetPlayerPrefs();
		
		introAnimator.gameObject.SetActive(entered);
		entered = true;
	}

	void resetPlayerPrefs() {
        PlayerPrefs.SetInt("playerDown", 0);
        PlayerPrefs.SetInt("playerUp", 0);
    }	
}
