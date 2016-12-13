using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
[XmlRoot("Cards")]
public class Deck
{
    //The different types of cards are initially split up into seperate lists so
    //that they may work with xml. If you try to throw them all directly into
    //one list, you get errors.

    //The Resource Cards of this deck
    [XmlArray("Resources"), XmlArrayItem("Resource", typeof(Resource_Card))]
    public List<Resource_Card> resourceCards = new List<Resource_Card>();

    //The Attack Cards of this deck
    [XmlArray("Attacks"), XmlArrayItem("Attack", typeof(Attack_Card))]
    public List<Attack_Card> attackCards = new List<Attack_Card>();

    //The movement cards of this deck
    [XmlArray("Movements"), XmlArrayItem("Movement", typeof(Movement_Card))]
    public List<Movement_Card> movementCards = new List<Movement_Card>();

    //The scenario cards of this deck
    [XmlArray("Scenarios"), XmlArrayItem("Scenario", typeof(Scenario_Card))]
    public List<Scenario_Card> scenarioCards = new List<Scenario_Card>();

    //The mercenary cards of this deck
    [XmlArray("Mercenaries"), XmlArrayItem("Mercenary", typeof(Mercenary_Card))]
    public List<Mercenary_Card> mercenaryCards = new List<Mercenary_Card>();

    //The actual deck of cards
    public List<Card> deck = new List<Card>();

    //Will be used to randomly fill the deck with all of the 4 types of cards you
    //got from the XML doc
    public void FillDeck()
    {
        //Random.seed = (int)Time.time;

        //Puts all of the 4 different types of cards into the deck
        for (int i = 0; i < resourceCards.Count; i++)
        {
            resourceCards[i].cardType = Card.CardType.RESOURCE_CARD;
            deck.Add(resourceCards[i]);
        }
        for (int i = 0; i < attackCards.Count; i++)
        {
            attackCards[i].cardType = Card.CardType.ATTACK_CARD;
            deck.Add(attackCards[i]);
        }
        for (int i = 0; i < movementCards.Count; i++)
        {
            movementCards[i].cardType = Card.CardType.MOVEMENT_CARD;
            deck.Add(movementCards[i]);
        }
        for (int i = 0; i < scenarioCards.Count; i++)
        {
            scenarioCards[i].cardType = Card.CardType.SCENARIO_CARD;
            deck.Add(scenarioCards[i]);
        }
        for (int i = 0; i < mercenaryCards.Count; i++)
        {
            mercenaryCards[i].cardType = Card.CardType.MERCENARY_CARD;
            deck.Add(mercenaryCards[i]);
        }

        //Shuffles the deck
        for (int k = 0; k < 2; k++)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                Card temp = deck[i];
                int randomIndex = Random.Range(i, deck.Count - 1);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }

        Debug.Log(deck[0].cardName);
        switch (deck[0].cardType)
        {
            case Card.CardType.MOVEMENT_CARD:
                {
                    Movement_Card temp = (Movement_Card)deck[0];
                    Debug.Log("Movement Amt: " + temp.movementAmt);
                    break;
                }
            case Card.CardType.ATTACK_CARD:
                {
                    Attack_Card temp = (Attack_Card)deck[0];
                    Debug.Log("Accuracy: " + temp.accuracy);
                    break;
                }
            case Card.CardType.RESOURCE_CARD:
                {
                    Resource_Card temp = (Resource_Card)deck[0];
                    Debug.Log("Resource Amt: " + temp.resourceAmt);
                    break;
                }
            case Card.CardType.SCENARIO_CARD:
                {
                    Scenario_Card temp = (Scenario_Card)deck[0];
                    Debug.Log("Spawn Zombies: " + temp.spawnZombies);
                    break;
                }
            case Card.CardType.MERCENARY_CARD:
                {
                    Mercenary_Card temp = (Mercenary_Card)deck[0];
                    Debug.Log("Base Attack: " + temp.baseAttack);
                    break;
                }
        }
    }

    public Card Dequeue()
    {
        Card output = deck[0];
        deck.RemoveAt(0);
        return output;
    }
}
