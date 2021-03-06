﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player2 info;

    private Command jumpCmd;
    private Command shootCmd;
    private Command moveCmd;

    [SerializeField]
    private Transform groundDetector;
    private Weapon weapon;

    private bool allowMovement;
    public float bounceCooldown;

    void Awake(){
        groundDetector = transform.GetChild(0).GetChild(0);
        weapon = GetComponentInChildren<Weapon>();

        info = GetComponent<Player2>();
        allowMovement = true;
        jumpCmd = new JumpCmd();
        shootCmd = new ShootCmd();
        moveCmd = new MoveCmd();
    }
    // Update is called once per frame
    void Update()
    {
        if(info.animator.GetInteger("LoadState") == 1){
            if(Input.GetButtonDown("MeleeAtk")){
                weapon.MeleeAtk();
            }        
            if (Input.GetButtonDown("Fire1") && weapon.Shoot()) {
                shootCmd.execute(transform,info);
            }
            if(allowMovement){
                info.moveSpeed = Input.GetAxis("Horizontal") * info.MAX_WALK_SPEED;
                if(Mathf.Abs(info.moveSpeed) > 0.01) info.rb2d.velocity = new Vector2(0,info.rb2d.velocity.y);
                moveCmd.execute(transform, info);
                if (Input.GetButtonDown("Jump") && ((IJumpable) info).canJump) {
                    jumpCmd.execute(transform,info);
                    ((IJumpable) info).jumpNum++;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if(hitInfo.gameObject.CompareTag(StrConstant.enemyTag)){
            IMelee atkInfo = hitInfo.gameObject.GetComponent<IMelee>();
            info.remainHealth -= atkInfo.meleeDmg;
            Vector2 dir = hitInfo.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;
            info.rb2d.velocity = Vector2.zero;
            info.rb2d.AddForce(dir*atkInfo.meleeKnockback,ForceMode2D.Impulse);
            allowMovement = false;
            Invoke("resetCoolDown", bounceCooldown);
        }
    }

    private void resetCoolDown() => allowMovement = true;
}