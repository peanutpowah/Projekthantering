﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {
    [SerializeField] List<GameObject> myCards = new List<GameObject>();
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float rotationZOffset;

    GameObject myPlayer;
    RaycastHit hit;
    Ray ray;
    GameObject myDeck;
    GameObject inspectedCard;
    GameObject gameController;


    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        myPlayer = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (gameController.GetComponent<GameController>().playedTurns == 0 && myCards.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                AddCardFromDeck();
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddCardFromDeck();
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("PlayerOneCard"))
            {
                if(inspectedCard != hit.collider.gameObject)
                {
                    SortCards();
                }
                inspectedCard = hit.collider.gameObject;
                InspectCard(hit.collider.gameObject);
            }
        }
        else
        {
            SortCards();
        }
    }


    public void AddCardFromDeck()
    {
        
        myDeck = GameObject.Find("CardDeckPlayerOne");

        if (myDeck.transform.childCount > 0)
        {
            AddCardToHand(myDeck.transform.GetChild(0).gameObject);
        }
        else
        {
            print("out of cards");
            myPlayer.GetComponent<PlayerScript>().RemoveHealth(1);
        }
    }

    public void AddCardToHand(GameObject newCard)
    {
        newCard.transform.SetParent(transform);
        myCards.Add(newCard);
        SortCards();
    }

    public void SortCards()
    {
        if (myCards.Count != 0)
        {
            rotationZOffset = -100 / (myCards.Count);

            for (int i = 0; i < myCards.Count; i++)
            {
                myCards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                myCards[i].transform.position = new Vector3(transform.position.x + xOffset * i, transform.position.y + (0.1f * i), transform.position.z + zOffset * i);
                myCards[i].transform.localRotation = Quaternion.Euler(90, 0, rotationZOffset * i);
            }
            transform.rotation = Quaternion.Euler(0, (rotationZOffset / 2) * (myCards.Count - 1), 0);
        }
    }
    void InspectCard(GameObject Card)
    {
            Card.GetComponent<SpriteRenderer>().sortingOrder = (myCards.Count + 1);
            Card.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
}
