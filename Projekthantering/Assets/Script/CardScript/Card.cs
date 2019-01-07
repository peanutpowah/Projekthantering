﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{   //This is a prefab for all the cards in the game. Includes variables that effects card in play.
    //Written by Tapani Kronvkist

    public enum CardState { InDeck, InHand, PickedUp, Released, Played, InGraveyard };

    [SerializeField] CardState myState;

    public string cardName, cardType, cardText;
    public int hp, attack, manaCost;
    public Sprite frame, portrait;
    Text showHp, showAttack, showCardName, showCardText, showManaCost, showCardType;
    Image showFrame, showPortrait;
    GameObject gameController;
    public GameObject myHand;
    Collider myCollider;

    bool isColliding;
    public bool isMouseDown;

    private Vector3 mOffset;

    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        //health
        hp = gameController.GetComponent<ReadCardData>().GetCardData(cardName).health;
        showHp = GameObject.Find($"{gameObject.name}/Canvas/DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;

        //attack
        attack = gameController.GetComponent<ReadCardData>().GetCardData(cardName).attack;
        showAttack = GameObject.Find($"{gameObject.name}/Canvas/DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + attack;

        //name
        showCardName = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardName").GetComponent<Text>();
        showCardName.text = cardName;

        //Description text
        cardText = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardText;
        showCardText = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardText").GetComponent<Text>();
        showCardText.text = cardText;

        //manacost
        manaCost = gameController.GetComponent<ReadCardData>().GetCardData(cardName).manacost;
        showManaCost = GameObject.Find($"{gameObject.name}/Canvas/DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + manaCost;

        //cardtype
        cardType = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardType;
        showCardType = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardType").GetComponent<Text>();
        showCardType.text = cardType;

        //card frame
        frame = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardFrame;
        showFrame = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardFrame").GetComponent<Image>();
        showFrame.sprite = frame;

        //card portrait
        portrait = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardPortrait;
        showPortrait = GameObject.Find($"{gameObject.name}/Canvas/DisplayPortrait").GetComponent<Image>();
        showPortrait.sprite = portrait;

        myCollider = GetComponent<Collider>();
        myState = CardState.InDeck;
    }

    // Update is called once per frame
    void Update()
    {
        
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
        if(!isColliding && myState == CardState.Released)
        {
            SetState(CardState.InHand);
        }

        if (isMouseDown)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "GameBoard")
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameBoard")
        {
            transform.SetParent(myHand.transform);
            isColliding = false;
        }
    }

    public void SetState(CardState newState)
    {
        myState = newState;
    }

    public CardState GetState()
    {
        return myState;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        SetState(CardState.PickedUp);
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        isMouseDown = true;
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
           
            SetState(CardState.Released);
            isMouseDown = false;
            if (myState == CardState.InHand)
            {
                transform.parent.gameObject.GetComponent<CardHand>().SortCards();
            }
        }
    }


    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
