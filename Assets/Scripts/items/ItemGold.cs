﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGold : MonoBehaviour
{
    [SerializeField]
    private int value;
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "PlayerAtkIgnore") {
            Player2 player = other.gameObject.GetComponent<Player2>();
            player.incGold(value);
            
            Destroy(gameObject);
        }
    }
}
