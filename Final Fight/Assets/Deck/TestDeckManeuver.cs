using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestDeckManeuver : MonoBehaviour
{
    //All temporary to maneuver through deck
    //This script is just for testing purposes
    public int move;
    bool buffer = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !buffer)
        {
            TestLoadDeck loadedDeck = GetComponentInParent<TestLoadDeck>();
            loadedDeck.currCard += move;
            if (loadedDeck.currCard == loadedDeck.deck.deck.Count)
                loadedDeck.currCard = 0;
            else if (loadedDeck.currCard < 0)
                loadedDeck.currCard = loadedDeck.deck.deck.Count - 1;

            loadedDeck.cardNameDisplay.text = "Name: " + loadedDeck.deck.deck[loadedDeck.currCard].cardName + "\n\n\n\n\n\n\n" + loadedDeck.deck.deck[loadedDeck.currCard].cardType;
            switch (loadedDeck.deck.deck[loadedDeck.currCard].cardType)
            {
                case Card.CardType.MOVEMENT_CARD:
                    {
                        loadedDeck.cardBack.color = Color.blue;
                        break;
                    }
                case Card.CardType.ATTACK_CARD:
                    {
                        loadedDeck.cardBack.color = Color.red;
                        break;
                    }
                case Card.CardType.RESOURCE_CARD:
                    {
                        loadedDeck.cardBack.color = Color.green;
                        break;
                    }
            }
            buffer = true;
        }
        else if (buffer)
            buffer = !buffer;
    }
}
