﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public void Select(GameObject button)
    {
        GameObject newObj = (GameObject)Instantiate(button.transform.GetChild(1).gameObject, player.transform.GetChild(0).position, player.transform.GetChild(0).rotation);
        newObj.transform.localScale = new Vector3(1, 1, 1);

        Destroy(player.transform.GetChild(0).gameObject);

        newObj.transform.parent = player.transform;
        newObj.transform.SetSiblingIndex(0);

        getAnimator();
    }

    public void StartGame()
    {
        Debug.Log("start!");

        getAnimator();

        player.GetComponent<InputController>().enabled = true;
        gameObject.SetActive(false);
    }

    private void getAnimator() => player.GetComponent<Player2>().animator = player.GetComponentInChildren<Animator>();
}
