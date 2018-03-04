using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIconMovement : MonoBehaviour {

    private Button fatherButton;
    private RectTransform rt;
    private Vector3 nonPressedPosition;
    private bool isHoldingDown = false;

	void Start () {
        fatherButton = GetComponentInParent<Button>();
        rt = GetComponent<RectTransform>();
        nonPressedPosition = new Vector3(0, 15, 0);
        rt.localPosition = nonPressedPosition;
    }

    public void MoveIconDown() {
        rt.localPosition = Vector3.zero;
        isHoldingDown = true;
    }

    public void MoveIconDownEnter() {
        if (isHoldingDown)
            rt.localPosition = Vector3.zero;
    }

    public void MoveIconUp() {
        rt.localPosition = nonPressedPosition;
        isHoldingDown = false;
    }

    public void MoveIconUpExit() {
        rt.localPosition = nonPressedPosition;
    }
}
