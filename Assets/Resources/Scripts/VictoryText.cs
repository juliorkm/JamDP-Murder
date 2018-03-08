using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour {

    public TextMeshProUGUI bottomText, upperText;
    public Image bottomPlayer, upperPlayer;
	public float fadeTime;

    void Start () {
        bottomPlayer.color = ColorManager.colors[(int)ColorManager.bottomPlayerColor];
        upperPlayer.color = ColorManager.colors[(int)ColorManager.upperPlayerColor];
        setWinner();
	}

	void setWinner() {
		string winner = PlayerPrefs.GetString("Winner");		
		if(winner == "Up") {
			upperText.text = "voce ganhou";
			bottomText.text = "perdeu mané";
			StartCoroutine(FadeTextToFullAlpha(fadeTime, upperText));
			StartCoroutine(FadeTextToFullAlpha(fadeTime, bottomText));			
		} else if (winner == "Down") {
			bottomText.text = "voce ganhou";
			upperText.text = "perdeu mané";
			StartCoroutine(FadeTextToFullAlpha(fadeTime, upperText));	
			StartCoroutine(FadeTextToFullAlpha(fadeTime, bottomText));	
		}
	}

	public IEnumerator FadeTextToFullAlpha(float time, TextMeshProUGUI text) {
			text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
			while (text.color.a < 1.0f) {
				text.color = new Color(text.color.r, 
					text.color.g, 
					text.color.b, 
					text.color.a + (Time.deltaTime / time));
				yield return null;
			}
		}

}
