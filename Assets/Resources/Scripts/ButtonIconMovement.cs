using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIconMovement : MonoBehaviour {

    private Button fatherButton;
    private RectTransform rt;
    private Vector3 nonPressedPosition;

	void Start () {
        fatherButton = GetComponentInParent<Button>();
        rt = GetComponent<RectTransform>();
        nonPressedPosition = new Vector3(0, 15, 0);
        //isTopPlayer = (fatherButton.transform.rotation.eulerAngles.z > 170);
	}

    public void MoveIconDown() {
        rt.localPosition = Vector3.zero;
    }

    public void MoveIconUp() {
        rt.localPosition = nonPressedPosition;
    }
}
