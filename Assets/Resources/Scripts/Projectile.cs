using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private RectTransform rectTransform;
    public float speed;
    public bool direction; //(true = up)

    public void SerializeProjectile (bool direction, Vector2 playerPos) {
        rectTransform = GetComponent<RectTransform>();
        if (direction == false) speed = - speed;
        this.direction = direction;
        rectTransform.anchoredPosition = playerPos;
	}
	
	void Update () {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,
            rectTransform.anchoredPosition.y + speed * Time.deltaTime);

        if (Mathf.Abs(rectTransform.localPosition.y) > 315) Destroy(gameObject);
	}
}
