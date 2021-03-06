﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{   //This is a prefab for all the cards in the game. Includes variables that effects card in play.
    //Written by Tapani Kronvkist

    public string cardName, cardType, cardText;
    public int hp, attack, manaCost;
    public Sprite frame, portrait;
    Text showHp, showAttack, showCardName, showCardText, showManaCost, showCardType;
    Image showFrame, showPortrait;
    
    // Start is called before the first frame update
    void Start()
    {
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
        showAttack = GameObject.Find("DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + attack;
        showCardName = GameObject.Find("DisplayCardName").GetComponent<Text>();
        showCardName.text = cardName;
        showCardText = GameObject.Find("DisplayCardText").GetComponent<Text>();
        showCardText.text = cardText;
        showManaCost = GameObject.Find("DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + manaCost;
        showCardType = GameObject.Find("DisplayCardType").GetComponent<Text>();
        showCardType.text = cardType;
        showFrame = GameObject.Find("DisplayCardFrame").GetComponent<Image>();
        showFrame.sprite = frame;
        showFrame = GameObject.Find("DisplayPortrait").GetComponent<Image>();
        showFrame.sprite = portrait;
    }

    // Update is called once per frame
    void Update()
    {
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
    }
}
