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
    ORANGE,
    CPU
}

public class ColorManager : MonoBehaviour {

    public static Color[] colors = new Color[] {
        new Color(.116f, .875f, .215f),
        new Color(.875f, .118f, .118f),
        new Color(.833f, .875f, .118f),
        new Color(.206f, .118f, .875f),
        new Color(.875f, .666f, .118f),
        new Color(.706f, .706f, .706f)
    };
    public static bool colorHasBeenAssigned = false;

    public static PlayerColor bottomPlayerColor, upperPlayerColor;
    public Button[] bottomPlayerButtons, upperPlayerButtons;

    private RectTransform bottomCpuIcon;
    private ButtonIconMovement bottomCpuIconMovement;
    private RectTransform upperCpuIcon;
    private ButtonIconMovement upperCpuIconMovement;

    // Use this for initialization
    void Start () {
        bottomCpuIconMovement = bottomPlayerButtons[bottomPlayerButtons.Length-1].GetComponentInChildren<ButtonIconMovement>();
        bottomCpuIcon = bottomCpuIconMovement.gameObject.GetComponent<RectTransform>();
        upperCpuIconMovement = upperPlayerButtons[upperPlayerButtons.Length-1].GetComponentInChildren<ButtonIconMovement>();
        upperCpuIcon = upperCpuIconMovement.gameObject.GetComponent<RectTransform>();

        //bottomPlayerColor = (PlayerColor) Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length-1);
        //upperPlayerColor = (PlayerColor) Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length-1);

        if (!colorHasBeenAssigned) {
            colorHasBeenAssigned = true;
            SetBottomPlayerColor(Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length - 1));
            SetUpperPlayerColor(Random.Range(0, System.Enum.GetNames(typeof(PlayerColor)).Length - 1));
        }
        else {
            SetBottomPlayerColor((int) bottomPlayerColor);
            SetUpperPlayerColor((int) upperPlayerColor);
        }

        if (upperPlayerColor == PlayerColor.CPU) upperCpuIcon.localPosition = Vector2.zero;
        else upperCpuIcon.localPosition = upperCpuIconMovement.nonPressedPosition;

        if (bottomPlayerColor == PlayerColor.CPU) bottomCpuIcon.localPosition = Vector2.zero;
        else bottomCpuIcon.localPosition = bottomCpuIconMovement.nonPressedPosition;
    }

    public void SetBottomPlayerColor(int i)
    {
        foreach (Button b in bottomPlayerButtons)
        {
            b.interactable = true;
        }
        bottomPlayerButtons[i].interactable = false;
        bottomPlayerColor = (PlayerColor)i;

        if (i == (int) PlayerColor.CPU)
            bottomCpuIcon.localPosition = Vector2.zero;
        else
            bottomCpuIcon.localPosition = bottomCpuIconMovement.nonPressedPosition;
    }

    public void SetUpperPlayerColor(int i)
    {
        foreach (Button b in upperPlayerButtons)
        {
            b.interactable = true;
        }
        upperPlayerButtons[i].interactable = false;
        upperPlayerColor = (PlayerColor)i;

        if (i == (int)PlayerColor.CPU)
            upperCpuIcon.localPosition = Vector2.zero;
        else
            upperCpuIcon.localPosition = upperCpuIconMovement.nonPressedPosition;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
