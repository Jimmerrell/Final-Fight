using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
[XmlRoot("Cards")]
public class Card 
{
    [XmlAttribute("name")]
    public string cardName = "\0";

    [XmlAttribute("description")]
    public string cardDescription = "\0";

    //How many resources it costs
    public int resourceCost = 0;

    //How many ammo tokens can be increased, if 100, max it out
    public int AmmoAmt = 0;
    //Extra Zombie Tokens collected from killing a zombie (if -1, no tokens are collected)
    public int ExtraZombieTokenAmt = 0;
    //How many lives you gain from activating this card
    public int LifeIncAmt = 0;

    public enum CardType { MOVEMENT_CARD, ATTACK_CARD, SCENARIO_CARD, RESOURCE_CARD, MERCENARY_CARD, DEFAULT };
    public CardType cardType = CardType.DEFAULT;
}
