using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerColor
{
    GREEN,
    RED,
    YELLOW,
    BLUE,
    ORANGE
}

public class ColorManager : MonoBehaviour {

    public static Color[] colors = new Color[] {
        new Color(.116f, .875f, .215f),
        new Color(.875f, .118f, .118f),
        new Color(.833f, .875f, .118f),
        new Color(.206f, .118f, .875f),
        new Color(.875f, .666f, .118f)
    };

    public static PlayerColor bottomPlayerColor, upperPlayerColor;
    public Button[] bottomPlayerButtons, upperPlayerButtons;

	// Use this for initialization
	void Start () {
        bottomPlayerColor = (PlayerColor) Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length);
        upperPlayerColor = (PlayerColor) Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length);
        SetBottomPlayerColor(Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length));
        SetUpperPlayerColor(Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length));
    }

    public void SetBottomPlayerColor(int i)
    {
        foreach (Button b in bottomPlayerButtons)
        {
            b.interactable = true;
        }
        bottomPlayerButtons[i].interactable = false;
        bottomPlayerColor = (PlayerColor)i;

    }

    public void SetUpperPlayerColor(int i)
    {
        foreach (Button b in upperPlayerButtons)
        {
            b.interactable = true;
        }
        upperPlayerButtons[i].interactable = false;
        upperPlayerColor = (PlayerColor)i;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
