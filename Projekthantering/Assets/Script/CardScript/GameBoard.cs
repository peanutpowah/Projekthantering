﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public List<GameObject> cardsOnTable = new List<GameObject>();

    [SerializeField] GameObject handGameobject;
    [SerializeField] float cardOffset;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        cardOffset = 10;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ArrangeCards();
    }
    private void OnTriggerStay(Collider other)
    {
        bool cardPlaced = false;
        Vector3 newPosition = new Vector3();
        List<GameObject> sortList = new List<GameObject>();

        if (other.tag == "PlayerOneCard" && other.GetComponent<Card>().GetState() == Card.CardState.PickedUp)
        {
            if (cardsOnTable.Count == 0)
            {
                cardsOnTable.Add(other.gameObject);
                other.transform.SetParent(gameObject.transform);
            }
            else
            {
                for (int i = 0; i < cardsOnTable.Count; i++)
                {
                    print("Sortlist " + sortList.Count);
                    if (other.transform.position.x < cardsOnTable[i].transform.position.x && !cardPlaced)
                    {
                        sortList.Add(other.gameObject);
                        other.transform.SetParent(gameObject.transform);
                        i -= 1;
                        cardPlaced = true;
                    }
                    else
                    {
                        sortList.Add(cardsOnTable[i]);
                    }
                }

                for (int i = 0; i < sortList.Count; i++)
                {
                    sortList[i].transform.position = new Vector3(cardOffset * i, transform.position.y, transform.position.z);
                    newPosition += sortList[i].transform.position;
                }
                if (sortList.Count != 0)
                {
                    newPosition /= sortList.Count;

                    newPosition = newPosition - (newPosition - startPos);

                    transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            if (other.GetComponent<Card>().GetState() == Card.CardState.Released)
            {
                handGameobject.GetComponent<CardHand>().RemoveCardFromHand(other.gameObject);
                cardsOnTable = sortList;
                other.GetComponent<Card>().SetState(Card.CardState.Played);
                other.gameObject.tag = "Untagged";
            }
        }
    }

    void ArrangeCards()
    {
        float newPos;
        for (int i = 0; i < cardsOnTable.Count; i++)
        {
            newPos = (cardsOnTable.Count / 2) + i;
            if (i - 1 == (int)(cardsOnTable.Count / 2))
            {
                newPos += 2;
            }
            cardsOnTable[i].transform.position = new Vector3(startPos.x + newPos * cardOffset, startPos.y, startPos.z);
        }
    }
}
