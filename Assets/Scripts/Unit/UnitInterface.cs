﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDashable
{
    float dashDist {set;get;}
    float dashSpeed {set;get;}
}

interface IShootable
{
    float shootDist {set;get;}
    float bulletLimit {set;get;}
    float bulletCount {set;get;}
    int bulletDmg {set;get;}

    Transform shootPos{set;get;}
    GameObject bullet {set;get;}
}

interface IJumpable
{
    float jumpPow {set;get;}
    int jumpNum {set;get;}
    int maxJumpNum {set;get;}
    bool canJump{get;}
}

interface IMelee
{
    int meleeDmg {get;set;}
    float meleeRange {get;set;}
    float meleeKnockback {get;set;}
    Transform hitPos{set;get;}

    LayerMask enemyLayers{set;get;}
    
}