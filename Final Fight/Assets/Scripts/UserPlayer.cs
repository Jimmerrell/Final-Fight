
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UserPlayer : Player
{
    GameObject StagedMovementCard = null;
    //ArrayList StagingAreaCards = new ArrayList();

    public GameObject player_HP_Text;
    public Text HPtoText;


    int AmountOfCardsPlayable = 1;

    bool m_bBlockCardPlayed = false;

    // Use this for initialization
    void Start()
    {
        HitPoints = 30;
        HPtoText = player_HP_Text.GetComponent<Text>();
        HPtoText.text = HitPoints.ToString();

        handPosition = handPanelObject.transform.position;

        if (gameObject.name == "Player One")
        {
            DrawKarateAttackCard();
            DrawKarateAttackCard();
            DrawKarateAttackCard();
            DrawKarateMovementCard();
            DrawKarateMovementCard();
        }
        else
        {
            DrawBoxingAttackCard();
            DrawBoxingAttackCard();
            DrawBoxingAttackCard();
            DrawBoxingMovementCard();
            DrawBoxingMovementCard();
        }

    }

    // Update is called once per frame
    void Update()
    {

        HPtoText.text = HitPoints.ToString();
        //Debug.Log(HPtoText.text);

        if (HitPoints < 0)
            HitPoints = 0;
        if (HitPoints > 30)
            HitPoints = 30;

    }

    public int PlayCards()
    {

        //TODO: Find the cards that are outside of the hand panel
        //      if they aren't in the play panel return them to the hand
        //      if they are in the play panel reveal them
        int nCardsPlayed = 0;
        bool bMomentumPlayed = false;
        m_bBlockCardPlayed = false;

        int nAttackValue = 0;
        m_nHighestAttackValuePlayed = 0;

        //Debug.Log(hand.Count.ToString());

        //Set the amount of cards playable based on a combo card being played or momentum card played
        for (int i = hand.Count - 1; i > -1; i--)
        {
            GameObject card = hand[i] as GameObject;
            if (CheckCollisionWithPlayPanel(card))
            {
                nCardsPlayed++;

                if (card.GetComponent<CardDetails>().CardName == CardDetails.Card_Name.MOMENTUM)
                {
                    bMomentumPlayed = true;
                }
                else if (card.GetComponent<CardDetails>().CardType == CardDetails.Card_Type.COMBO)
                {
                    AmountOfCardsPlayable += card.GetComponent<CardDetails>().nComboAmount;
                }
            }
        }

        //Debug.Log("Cards played = " + nCardsPlayed.ToString());
        //Debug.Log("Amount of playable cards = " + AmountOfCardsPlayable.ToString());


        // Check to see if amount of cards played is greater than nAmountOfCardsPlayable
        if (nCardsPlayed > AmountOfCardsPlayable)
        {
            //intantiate a warning to player that they are not playing a valid amount of cards
            return -1; // this will play the warning sound
        }

        // Resolve play of any additional cards based on how many cards are playable

        //Seperate out the cards into the PlayedCards array
        for (int i = hand.Count - 1; i > -1; i--)
        {
            GameObject card = hand[i] as GameObject;
            if (CheckCollisionWithPlayPanel(card))
            {
                CardsPlayed.Add(card);
                //Destroy(card);
                hand.RemoveAt(i);
            }
        }

        // Debug.Log("Hand size = " + hand.Count.ToString());
        //Debug.Log("Play Amount = " + CardsPlayed.Count.ToString());

        // Play the cards; starting with the attack cards
        // If there is a movement card that cannot be used, move it to the sstaging area for the next turn
        for (int i = CardsPlayed.Count - 1; i > -1; i--)
        {
            GameObject card = CardsPlayed[i] as GameObject;

            if (card.GetComponent<CardDetails>().CardType == CardDetails.Card_Type.ATTACK)
            {
                //Does the attack card require a movement
                if (card.GetComponent<CardDetails>().bRequiresMovement == true)
                {
                    //If yes, do we have the movement card (look through CardsPlayed and 
                    if (HaveRequiredMovementCard(card.GetComponent<CardDetails>().MovementRequired))
                    {
                        //if yes, nAttackValue += the card's attack value
                        nAttackValue += card.GetComponent<CardDetails>().nAttackValue;
                        SetHighestAttackValue(card.GetComponent<CardDetails>().nAttackValue);

                        Debug.Log("Movement card found and attack applied");
                    }
                    else //If no, set the card to destroy
                    {
                        //discard is happening automatically for all cards played unless it is the last movement card
                    }

                }
                else //If no, nAttackValue += the card's attack value
                {
                    nAttackValue += card.GetComponent<CardDetails>().nAttackValue;
                    SetHighestAttackValue(card.GetComponent<CardDetails>().nAttackValue);
                }

            }
        }

        int card_tobe_removed = 0;
        int currentcard = 0;
        bool bremovecard = false;
        foreach (GameObject card in CardsPlayed)
        {
            if (card.GetComponent<CardDetails>().CardType == CardDetails.Card_Type.MOVEMENT)
            {
                if (card.GetComponent<CardDetails>().CardName == CardDetails.Card_Name.BLOCK ||
                    card.GetComponent<CardDetails>().CardName == CardDetails.Card_Name.BOXING_BLOCK)
                {
                    m_bBlockCardPlayed = true;
                }
                else
                {
                    if (card.GetComponent<CardDetails>().IsSetToBeUsed() == true)
                    {
                        continue;
                    }
                    else if (StagedMovementCard == null)
                    {
                        StagedMovementCard = Instantiate(card);//new GameObject(card);
                        StagedMovementCard.transform.SetParent(stagingPanelObject.transform);
                        StagedMovementCard.transform.position = stagingPanelObject.transform.position; //card.transform.position = stagingPanelObject.transform.position;
                        StagedMovementCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        //CardsPlayed.Remove(card);
                        card_tobe_removed = currentcard;
                        bremovecard = true;

                    }
                }
            }

            currentcard++;
        }

        if (bremovecard == true)
        {
            GameObject tempCard = CardsPlayed[card_tobe_removed] as GameObject;
            CardsPlayed.RemoveAt(card_tobe_removed);
            Destroy(tempCard);
        }


        if (StagedMovementCard != null)
        {
            if (StagedMovementCard.GetComponent<CardDetails>().nTurnsLasted >= 1)
            {
                Destroy(StagedMovementCard);
                StagedMovementCard = null;
            }
            if (StagedMovementCard != null)
                StagedMovementCard.GetComponent<CardDetails>().nTurnsLasted += 1;
        }


        UpdateCards();

        //reset the amount of cards playable
        AmountOfCardsPlayable = 1;

        return nAttackValue;
    }

    public void HP_ADD(int num)
    {
        HitPoints = HitPoints + num;
    }

    public void HP_SUBTRACT(int num)
    {
        HitPoints = HitPoints - num;
    }

    bool CheckCollisionWithPlayPanel(GameObject card)
    {
        if (card != null)
            if (card.GetComponent<CardCollision>().bIsInPlayPanel)
            {
                return true;
            }

        return false;
    }

    bool PointToRectCollision(Vector2 point, RectTransform rect)
    {
        if (point.y > rect.rect.yMax
            && point.y < rect.rect.yMin
            && point.x > rect.rect.xMin
            && point.x < rect.rect.xMax)
        {
            return true;
        }

        return false;
    }

    public bool bBlockCardPlayed()
    {
        return m_bBlockCardPlayed;
    }

    public GameObject GetPlayerHandPanel()
    {
        return handPanelObject;
    }

    bool HaveRequiredMovementCard(CardDetails.Card_Name card_name)
    {
        if (StagedMovementCard != null)
            if (StagedMovementCard.GetComponent<CardDetails>().CardName == card_name)
            {
                //Do something with the Staged Movement Card
                StagedMovementCard.GetComponent<CardDetails>().SetCardToBeUsed();
                return true;
            }

        foreach (GameObject card in CardsPlayed)
        {
            if (card.GetComponent<CardDetails>().CardName == card_name)
            {
                //Do something with the movement card
                card.GetComponent<CardDetails>().SetCardToBeUsed();
                Debug.Log("Movement card required = true");
                return true;
            }
        }


        return false;
    }
}
