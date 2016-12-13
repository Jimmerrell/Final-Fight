using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
public class TestLoadDeck : MonoBehaviour 
{
    //This is used to test reading in XML. This will probably be adapted into a deck manager or
    //put into the game manager or something along those lines in the future

    public Deck deck = new Deck();

    //These are temporary values used for testing and showing the
    //functionality of the deck
    public SpriteRenderer cardBack = null;
    public TextMesh cardNameDisplay = null;
    public int currCard = 0;
    bool buffer = false;

	// Use this for initialization
	void Awake () 
    {
        cardBack = GetComponentInChildren<SpriteRenderer>();
        cardNameDisplay = GetComponentInChildren<TextMesh>();
        
        //LoadDeck();

       //cardNameDisplay.text = "Name: " + deck.deck[currCard].cardName + "\n\n\n\n\n\n\n" + deck.deck[currCard].cardType;
       //switch (deck.deck[currCard].cardType)
       //{
       //    case Card.CardType.MOVEMENT_CARD:
       //        {
       //            cardBack.color = Color.blue;
       //            break;
       //        }
       //    case Card.CardType.ATTACK_CARD:
       //        {
       //            cardBack.color = Color.red;
       //            break;
       //        }
       //    case Card.CardType.RESOURCE_CARD:
       //        {
       //            cardBack.color = Color.green;
       //            break;
       //        }
       //    case Card.CardType.SCENARIO_CARD:
       //        {
       //            cardBack.color = Color.yellow;
       //            break;
       //        }
       //    case Card.CardType.MERCENARY_CARD:
       //        {
       //            cardBack.color = Color.cyan;
       //            break;
       //        }
       //}
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckInput();
	}

    void LoadDeck()
    {
        //Will probably have to change this file path depending on computer
        string path = "Assets\\Deck\\FinalFight_Decks.xml";

        //Loads in all of the xml data
        var serializer = new XmlSerializer(typeof(Deck));
        var stream = new FileStream(path, FileMode.Open);
        deck = serializer.Deserialize(stream) as Deck;
        stream.Close();

        //Fills and shuffles the deck
        deck.FillDeck();
        Debug.Log(deck.deck.Count);
    }

    //A temporary function (used for testing/debugging) that allows you
    //to flip through the cards of the deck w/ the left/right arrow keys
    //TODO: Allow you to flip through with buttons
    void CheckInput()
    {
        if (!buffer)
        {
            bool change = false;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currCard--;
                if (currCard == -1)
                    currCard = deck.deck.Count - 1;
                change = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currCard++;
                if (currCard == deck.deck.Count)
                    currCard = 0;
                change = true;
            }

            if (change)
            {
                buffer = true;
                cardNameDisplay.text = "Name: " + deck.deck[currCard].cardName + "\n\nCard Description:\n" + deck.deck[currCard].cardDescription + "\n\n\n" + deck.deck[currCard].cardType;
                switch (deck.deck[currCard].cardType)
                {
                    case Card.CardType.MOVEMENT_CARD:
                        {
                            cardBack.color = Color.blue;
                            break;
                        }
                    case Card.CardType.ATTACK_CARD:
                        {
                            cardBack.color = Color.red;
                            break;
                        }
                    case Card.CardType.RESOURCE_CARD:
                        {
                            cardBack.color = Color.green;
                            break;
                        }
                    case Card.CardType.SCENARIO_CARD:
                        {
                            cardBack.color = Color.yellow;
                            break;
                        }
                    case Card.CardType.MERCENARY_CARD:
                        {
                            cardBack.color = Color.cyan;
                            break;
                        }
                }
            }
        }
        else
        {
            if (!Input.anyKey)
                buffer = false;
        }
    }
}
