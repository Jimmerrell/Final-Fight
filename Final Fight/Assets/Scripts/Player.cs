using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int HitPoints;

    protected ArrayList hand = new ArrayList();
    protected ArrayList CardsPlayed = new ArrayList();

    public ArrayList GetCardsPlayed() { return CardsPlayed; }

    public GameObject UI_Card;

    protected Vector2 handPosition;
    public GameObject handPanelObject;
    public GameObject playPanelObject;
    public GameObject stagingPanelObject;

    public GameObject Player_Attack_Deck;
    public GameObject Player_Movement_Deck;

    protected int m_nHighestAttackValuePlayed = 0;
    public int GetHighestAttackValuePlayed() { return m_nHighestAttackValuePlayed; }

    // Use this for initialization
    void Start()
    {
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

            TurnDownHand();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCards(bool bRevealed)
    {
        if (gameObject.name == "Player One")
            DrawKarateDeckCards();
        else if (gameObject.name == "Player Two")
            DrawBoxingDeckCards();

        if (bRevealed)
            RevealHand();
        else
            TurnDownHand();

    }

    public void UpdateCards()
    {
        handPosition.x = handPanelObject.transform.position.x + hand.Count * 15;

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i] as GameObject;
            card.GetComponent<UI_CardBehavior>().SetIndex(i);
            card.GetComponent<UI_CardBehavior>().UpdateCard(handPosition, hand.Count);
        }
    }

    public void DrawBoxingDeckCards()
    {
        DrawBoxingAttackCard();
        DrawBoxingMovementCard();
    }

    public void DrawKarateDeckCards()
    {
        DrawKarateAttackCard();
        DrawKarateMovementCard();
    }

    public void DrawKarateAttackCard()
    {
        // Draw One Attack Card
        if (hand.Count < 7)
        {
            int randomAttackCard = Random.Range(1, 9);
            string UI_CardName = "";

            switch (randomAttackCard)
            {
                case 1:
                    UI_CardName = "Straight";
                    break;
                case 2:
                    UI_CardName = "Power_Straight";
                    break;
                case 3:
                    UI_CardName = "Front_Kick";
                    break;
                case 4:
                    UI_CardName = "Power_Front_Kick";
                    break;
                case 5:
                    UI_CardName = "Back_Hand";
                    break;
                case 6:
                    UI_CardName = "Jumping_Kick";
                    break;
                case 7:
                    UI_CardName = "ComboX2";
                    break;
                case 8:
                    UI_CardName = "ComboX3";
                    break;
                case 9:
                    UI_CardName = "ComboX4";
                    break;
            }

            UI_Card = GameObject.Find(UI_CardName);

            GameObject card = Instantiate(UI_Card) as GameObject;
            card.transform.SetParent(handPanelObject.transform, false);
            card.transform.position = Player_Attack_Deck.transform.position;
            card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
            hand.Add(card);
            UpdateCards();
        }
    }

    public void DrawKarateMovementCard()
    {
        // Draw One Movement Card
        if (hand.Count < 7)
        {
            int randomMoveCard = Random.Range(1, 4);
            string UI_CardName = "";

            switch (randomMoveCard)
            {
                case 1:
                    UI_CardName = "Block";
                    break;

                case 2:
                    UI_CardName = "Lean";
                    break;

                case 3:
                    UI_CardName = "Plant";
                    break;

                case 4:
                    UI_CardName = "Momentum";
                    break;
            }

            UI_Card = GameObject.Find(UI_CardName);

            GameObject card = Instantiate(UI_Card) as GameObject;
            card.transform.SetParent(handPanelObject.transform, false);
            card.transform.position = Player_Movement_Deck.transform.position;
            card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
            hand.Add(card);
            UpdateCards();
        }
    }

    public void DrawBoxingAttackCard()
    {
        // Draw One Attack Card
        if (hand.Count < 7)
        {
            int randomAttackCard = Random.Range(1, 9);
            string UI_CardName = "";

            switch (randomAttackCard)
            {
                case 1:
                    UI_CardName = "Jab";
                    break;
                case 2:
                    UI_CardName = "Power_Jab";
                    break;
                case 3:
                    UI_CardName = "Hook";
                    break;
                case 4:
                    UI_CardName = "Power_Hook";
                    break;
                case 5:
                    UI_CardName = "UpperCut";
                    break;
                case 6:
                    UI_CardName = "Haymaker";
                    break;
                case 7:
                    UI_CardName = "Boxing_ComboX2";
                    break;
                case 8:
                    UI_CardName = "Boxing_ComboX3";
                    break;
                case 9:
                    UI_CardName = "Boxing_ComboX4";
                    break;
            }

            UI_Card = GameObject.Find(UI_CardName);

            GameObject card = Instantiate(UI_Card) as GameObject;
            card.transform.SetParent(handPanelObject.transform, false);
            card.transform.position = Player_Attack_Deck.transform.position;
            card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
            hand.Add(card);
            UpdateCards();
        }
    }

    public void DrawBoxingMovementCard()
    {
        // Draw One Movement Card
        if (hand.Count < 7)
        {
            int randomMoveCard = Random.Range(1, 4);
            string UI_CardName = "";

            switch (randomMoveCard)
            {
                case 1:
                    UI_CardName = "Boxing_Block";
                    break;

                case 2:
                    UI_CardName = "Crouch";
                    break;

                case 3:
                    UI_CardName = "Rotate";
                    break;

                case 4:
                    UI_CardName = "Boxing_Momentum";
                    break;
            }

            UI_Card = GameObject.Find(UI_CardName);

            GameObject card = Instantiate(UI_Card) as GameObject;
            card.transform.SetParent(handPanelObject.transform, false);
            card.transform.position = Player_Movement_Deck.transform.position;
            card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
            hand.Add(card);
            UpdateCards();
        }
    }

    public void RevealHand()
    {
        int i = 0;
        foreach (GameObject card in hand)
        {
            if (i >= hand.Count)
            {
                Debug.Log("hand.count greater than hand size");
                return;
            }

            if (card != null)
                card.GetComponent<CardDetails>().RevealCard();

            i++;
        }
    }

    public void TurnDownHand()
    {
        int i = 0;
        foreach (GameObject card in hand)
        {
            if (i >= hand.Count)
            {
                Debug.Log("hand.count greater than hand size");
                return;
            }

            if (card != null)
                card.GetComponent<CardDetails>().TurnDownCard();

            i++;
        }
    }

    public void DiscardPlayedCards()
    {
        foreach(GameObject card in CardsPlayed)
        {
            Destroy(card);
        }

        CardsPlayed.Clear();
    }

    protected void SetHighestAttackValue(int n_attack_value)
    {
        if (n_attack_value > m_nHighestAttackValuePlayed)
            m_nHighestAttackValuePlayed = n_attack_value;
    }
}
