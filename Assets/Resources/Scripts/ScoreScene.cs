using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour {

    public TextMeshProUGUI bottomScore, upperScore;
    public Vector2 bottomStop, upperStop;
    public Vector2 bottomEnd, upperEnd;
    public int scoreLimit;

    private int playerDownScore, playerUpScore;

    void Start () {
        playerDownScore = PlayerPrefs.GetInt("playerDown");
        playerUpScore = PlayerPrefs.GetInt("playerUp");
        bottomScore.text = playerDownScore + " x " + playerUpScore;
        upperScore.text = playerUpScore + " x " + playerDownScore;
        
        StartCoroutine(ScoreAnimation());
	}

    IEnumerator ScoreAnimation() {
        var bottomRect = bottomScore.GetComponent<RectTransform>();
        var upperRect = upperScore.GetComponent<RectTransform>();
        while (bottomRect.anchoredPosition.y > bottomStop.y + 10) {
            bottomRect.anchoredPosition =
                Vector2.Lerp(bottomRect.anchoredPosition, bottomStop, .15f);
            upperRect.anchoredPosition =
                Vector2.Lerp(upperRect.anchoredPosition, upperStop, .15f);

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.8f);
        while (bottomRect.anchoredPosition.y > bottomEnd.y + 10) {
            bottomRect.anchoredPosition =
                Vector2.Lerp(bottomRect.anchoredPosition, bottomEnd, .4f);
            upperRect.anchoredPosition =
                Vector2.Lerp(upperRect.anchoredPosition, upperEnd, .4f);

            yield return new WaitForEndOfFrame();
        }

        if (playerUpScore >= scoreLimit) {
            loadVictoryScene("Up");
        } else if (playerDownScore >= scoreLimit) {
            loadVictoryScene("Down");
        } else {
            SceneManager.LoadScene("Gameplay");
        }
    }

    void loadVictoryScene(string winner) {
        PlayerPrefs.SetString("Winner", winner);
        SceneManager.LoadScene("Victory");
    }
}
