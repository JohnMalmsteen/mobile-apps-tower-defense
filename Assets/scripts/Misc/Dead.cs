﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Dead : MonoBehaviour, IPointerClickHandler
{
    public MiniGame minigame;
    public bool DEAD = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Clicked: " + gameObject.name);

        transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        
        DEAD = true;

        minigame.ImDead(gameObject);
    }
}
