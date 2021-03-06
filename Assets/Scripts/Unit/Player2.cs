﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player2 : Unit2,IJumpable,IShootable
{
    //Jumpable property
    [field: SerializeField]
    public float jumpPow{get;set;}
    [field: SerializeField]
    public int maxJumpNum{get;set;}
    [field: SerializeField]
    public int jumpNum{get;set;}
    public bool canJump
    {
        get => jumpNum < maxJumpNum;
    }

    //Shootable property
    [field: SerializeField]
    public float shootDist{get;set;}
    [field: SerializeField]
    public float bulletLimit{get;set;}
    [field: SerializeField]
    public Transform shootPos{get;set;}
    [field: SerializeField]
    public GameObject bullet {get;set;}
    private float _bulletCount;
    public float bulletCount
    {
        get => _bulletCount;
        set
        {
            if(isInvincible && value <= _bulletCount) return;
            _bulletCount = value;
            if (_bulletCount > bulletLimit) _bulletCount = bulletLimit;
            updateBullet();

        }
    }
    public int bulletDmg{get;set;}

    //text
    [field: SerializeField]
    public TextMeshProUGUI bulletText{get;set;}
    [field: SerializeField]
    public TextMeshProUGUI goldText{get;set;}

    //info
    private int _gold;
    public int gold
    {
        get => _gold;
        set{
            _gold = value;
            updateGold();
        }
    }

    public void Awake(){
        base.Awake();
        changeWeapon();
        setPlayerState(LevelController.playerState);
        isInvincible = LevelController.getSceneIndex() == 1;
    }
    public void Update(){
        base.Update();
        int loadState = animator.GetInteger("LoadState");
        if(loadState == 1){
            restrainWithBg();
        }
        deadZone();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag(StrConstant.platformTag) && hitInfo.transform.position.y < transform.position.y) {
            jumpNum = 0;
        }
    }

    protected override void initSpawn(){
        Transform all = GameObject.Find(StrConstant.playerSpawnAddr).transform;
        int rand = (int) Mathf.Floor(Random.Range(0,all.childCount));
        spawnPoint = all.GetChild(rand);
    }

    private void changeWeapon() {
        Weapon weap = transform.GetChild(1).GetComponent<Weapon>();

        bulletDmg = weap.dmg;
    }
    
    private void restrainWithBg(){
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp(pos.y, -1, 1);
        if(pos.y == 1){
            rb2d.velocity = Vector3.zero;
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public void deadZone() {
        // is below death point
        if (transform.position.y < -10) {
            if(spawnPoint == null) initSpawn();
            rb2d.velocity = new Vector3(0, 0, 0);
            transform.position = spawnPoint.position  + new Vector3(0,0.5f,0);
            remainHealth -= 1;
            animator.SetInteger("LoadState",0);
        }
    }

    public void updateBullet() {
        if (!bulletText) return;

        bulletText.text = bulletCount + " / " + bulletLimit;
    }

    public void updateGold() {
        if (!goldText) return;

        goldText.text = gold.ToString();
    }

    public PlayerState getPlayerState(bool keepPos){
        PlayerState state = new PlayerState(transform.position);
        if(!keepPos) state.position = null;
        state.remainHealth = remainHealth;
        state.totalHealth = totalHealth;
        state.gold = gold;
        state.bulletCount = bulletCount;
        state.bulletLimit = bulletLimit;
        state.facingRight = facingRight;
        state.charIndex = Globals.getCharIndex();
        return state;
    }

    public void setPlayerState(PlayerState state){
        totalHealth = state.totalHealth;
        remainHealth = state.remainHealth;
        gold = state.gold;
        bulletCount = state.bulletCount;
        bulletLimit = state.bulletLimit;
        facingRight = state.facingRight;
        if(!facingRight) transform.Rotate(0f,180f,0f);
        transform.position =  state.position == null ? spawnPoint.position + new Vector3(0,0.5f,0) : state.position.toVector3();
    }
}
