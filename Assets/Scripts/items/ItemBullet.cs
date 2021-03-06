﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullet : Items
{   
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(StrConstant.playerTag)) {
            Player2 player = other.gameObject.GetComponent<Player2>();
            player.bulletCount += value;
            
            Destroy(gameObject);
        }
    }
}
