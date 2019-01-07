﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class ReadCardData : MonoBehaviour
{
    StreamReader readData;
    [SerializeField] string textPath;
    public List<CardData> cardlist;

    // Start is called before the first frame update
    void Awake()
    {
        cardlist = new List<CardData>();
        string readLine;
       
        readData = new StreamReader(textPath);
        while ((readLine = readData.ReadLine()) != null)
        {
            ConvertStringToData(readLine);
        }
        readData.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ConvertStringToData(string line)
    {
        //data [0-7]{name, health, manacost, attack, cardtext, taunt, charge, battlecry}
        string[] data = line.Split('\t');
        cardlist.Add(new CardData(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[4], StringToBool(data[5]), StringToBool(data[6]), StringToBool(data[7]), StringToBool(data[8]), data[9]));
    }
    bool StringToBool(string input)
    {
        if (input.ToLower() == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public CardData GetCardData(string name)
    {
        for (int i = 0; i < cardlist.Count; i++)
        {
            if(name == cardlist[i].name)
            {
                return cardlist[i];
            }
        }
        return null;
    }
}

public class CardData
{
   public string name;
    public int health;
    public int manacost;
    public int attack;
    public string cardText;
    public bool taunt;
    public bool charge;
    public bool battlecry;
    public bool drunkRage;
    public string cardType;
    public Sprite cardFrame;
    public Sprite cardPortrait;

    public CardData(string name, int health, int manacost, int attack, string cardText, bool taunt, bool charge, bool battlecry, bool drunkRage, string cardType)
    {
        this.name = name;
        this.health = health;
        this.manacost = manacost;
        this.attack = attack;
        this.cardText = cardText;
        this.taunt = taunt;
        this.charge = charge;
        this.battlecry = battlecry;
        this.drunkRage = drunkRage;
        this.cardType = cardType;
        this.cardFrame =  (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/2D Textures/Cards/Frames/{cardType}.png", typeof(Sprite));
        this.cardPortrait = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/2D Textures/Cards/Portraits/{name}.png", typeof(Sprite));
    }

}