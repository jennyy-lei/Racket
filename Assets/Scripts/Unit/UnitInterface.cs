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
    float meleeDmg {get;set;}
    float meleeRange {get;set;}
    float knockbackForce {get;set;}
}