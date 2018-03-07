using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private bool upperReady = false, bottomReady = false;
    public Image upperPlay, bottomPlay;
    public GameObject upperBlock, bottomBlock;

    public void ChangeScene(string s) {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(s);
	}

    public void SetUpperReady() {
        upperReady = !upperReady;
        if (upperReady)
            upperPlay.color = ColorManager.colors[(int)ColorManager.upperPlayerColor];
        else
            upperPlay.color = Color.white;
        if (upperReady && bottomReady) ChangeScene("Gameplay");
        upperBlock.SetActive(upperReady);
    }

    public void SetBottomReady() {
        bottomReady = !bottomReady;
        if (bottomReady)
            bottomPlay.color = ColorManager.colors[(int)ColorManager.bottomPlayerColor];
        else
            bottomPlay.color = Color.white;
        if (upperReady && bottomReady) ChangeScene("Gameplay");
        bottomBlock.SetActive(bottomReady);
    }

}
