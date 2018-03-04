using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

	void Start () {
		resetPlayerPrefs();
	}

	void resetPlayerPrefs() {
        PlayerPrefs.SetInt("playerDown", 0);
        PlayerPrefs.SetInt("playerUp", 0);
    }	
}
