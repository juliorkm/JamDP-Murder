using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour {
    
    public TextMeshProUGUI bottomScore, upperScore;
    public Vector2 bottomStop, upperStop;
    public Vector2 bottomEnd, upperEnd;

    private int playerOneScore, playerTwoScore;
    
    void Start () {
        playerOneScore = PlayerPrefs.GetInt("player1");
        playerTwoScore = PlayerPrefs.GetInt("player2");
        bottomScore.text = playerOneScore + " x " + playerTwoScore;
        upperScore.text = playerTwoScore + " x " + playerOneScore;
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
        while (bottomRect.anchoredPosition.y > bottomEnd.y + 10)
        {
            bottomRect.anchoredPosition =
                Vector2.Lerp(bottomRect.anchoredPosition, bottomEnd, .4f);
            upperRect.anchoredPosition =
                Vector2.Lerp(upperRect.anchoredPosition, upperEnd, .4f);

            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Gameplay");
    }
}
