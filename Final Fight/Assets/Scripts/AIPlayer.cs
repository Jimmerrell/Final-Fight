using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPlayer : Player
{
    public GameObject Player_Attack_Deck;
    public GameObject Player_Movement_Deck;

    ArrayList hand = new ArrayList();
    Vector2 handPosition;
    GameObject handPanelObject;
    GameObject playPanelObject;
    GameObject stagingPanelObject;
    Vector2 playPanelPosition;
    Vector2 stagingPanelPosition;

    public GameObject UI_Card;

    public GameObject player_HP_Text;
    public Text HPtoText;

    // Use this for initialization
    void Start()
    {
        HitPoints = 30;
        HPtoText = player_HP_Text.GetComponent<Text>();
        HPtoText.text = HitPoints.ToString();

        handPanelObject = GameObject.Find("Hand_Panel_Opponent");
        playPanelObject = GameObject.Find("Play_Panel_Opponent");
        stagingPanelObject = GameObject.Find("Staging_Panel_Opponent");

        handPosition = handPanelObject.transform.position;
        playPanelPosition = playPanelObject.transform.position;
        stagingPanelPosition = stagingPanelObject.transform.position;

        DrawCards(false);
        DrawCards(false);
    }

    int AI_Behavior()
    {
        //stupid/random behavior
        int playAmount = 1;// Random.Range(1, hand.Count);
        // 1) Play the first card
        GameObject card = hand[0] as GameObject;
        Debug.Log(card.transform.position.y.ToString());
        card.transform.position = playPanelPosition;
        Debug.Log(card.transform.position.y.ToString());

        return playAmount;
    }

    // Update is called once per frame
    void Update()
    {

        HPtoText.text = HitPoints.ToString();

        if (HitPoints < 0)
            HitPoints = 0;
        if (HitPoints > 30)
            HitPoints = 30;
    }

    //public void DrawCards()
    //{
    //    //Debug.Log(hand.Count.ToString());

    //    if (hand.Count < 7)
    //    {
    //        GameObject card = Instantiate(UI_Card) as GameObject;
    //        card.transform.SetParent(handPanelObject.transform, false);
    //        card.transform.position = Player_Attack_Deck.transform.position;
    //        card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
    //        hand.Add(card);
    //        UpdateCards();
    //    }
    //    if (hand.Count < 7)
    //    {
    //        GameObject card2 = Instantiate(UI_Card) as GameObject;
    //        card2.transform.SetParent(handPanelObject.transform, false);
    //        card2.transform.position = Player_Attack_Deck.transform.position;
    //        card2.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
    //        hand.Add(card2);
    //        UpdateCards();
    //    }
    //}

    //public void DrawCards()
    //{
    //    DrawAttackCard();
    //    DrawMovementCard();
    //}

    //void DrawAttackCard()
    //{
    //    // Draw One Attack Card
    //    if (hand.Count < 7)
    //    {
    //        int randomAttackCard = Random.Range(1, 9);
    //        string UI_CardName = "";

    //        switch (randomAttackCard)
    //        {
    //            case 1:
    //                UI_CardName = "Jab";
    //                break;
    //            case 2:
    //                UI_CardName = "Power_Jab";
    //                break;
    //            case 3:
    //                UI_CardName = "Hook";
    //                break;
    //            case 4:
    //                UI_CardName = "Power_Hook";
    //                break;
    //            case 5:
    //                UI_CardName = "UpperCut";
    //                break;
    //            case 6:
    //                UI_CardName = "Haymaker";
    //                break;
    //            case 7:
    //                UI_CardName = "Boxing_ComboX2";
    //                break;
    //            case 8:
    //                UI_CardName = "Boxing_ComboX3";
    //                break;
    //            case 9:
    //                UI_CardName = "Boxing_ComboX4";
    //                break;
    //        }

    //        UI_Card = GameObject.Find(UI_CardName);

    //        GameObject card = Instantiate(UI_Card) as GameObject;
    //        card.transform.SetParent(handPanelObject.transform, false);
    //        card.transform.position = Player_Attack_Deck.transform.position;
    //        card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
    //        hand.Add(card);
    //        UpdateCards();
    //    }
    //}

    //void DrawMovementCard()
    //{
    //    // Draw One Movement Card
    //    if (hand.Count < 7)
    //    {
    //        int randomMoveCard = Random.Range(1, 4);
    //        string UI_CardName = "";

    //        switch (randomMoveCard)
    //        {
    //            case 1:
    //                UI_CardName = "Boxing_Block";
    //                break;

    //            case 2:
    //                UI_CardName = "Crouch";
    //                break;

    //            case 3:
    //                UI_CardName = "Rotate";
    //                break;

    //            case 4:
    //                UI_CardName = "Boxing_Momentum";
    //                break;
    //        }

    //        UI_Card = GameObject.Find(UI_CardName);

    //        GameObject card = Instantiate(UI_Card) as GameObject;
    //        card.transform.SetParent(handPanelObject.transform, false);
    //        card.transform.position = Player_Movement_Deck.transform.position;
    //        card.GetComponent<UI_CardBehavior>().SetIndex(hand.Count);
    //        hand.Add(card);
    //        UpdateCards();
    //    }
    //}

    public int PlayCards()
    {
        // Chose the cards to be played

        // Reveal played cards in the play panel

        int playAmount = AI_Behavior();

        // destroy that amount of cards
        //for (int i = playAmount - 1; i > -1; i--)
        //{
        //    GameObject card = hand[i] as GameObject;
        //    Destroy(card);
        //    hand.RemoveAt(i);
        //}

        UpdateCards();

        return playAmount;
    }

    void UpdateCards()
    {
        handPosition.x = handPanelObject.transform.position.x + hand.Count * 15;

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i] as GameObject;
            card.GetComponent<UI_CardBehavior>().SetIndex(i);
            card.GetComponent<UI_CardBehavior>().UpdateCard(handPosition, hand.Count);
        }
    }

    public void HP_ADD(int num)
    {
        HitPoints = HitPoints + num;
    }

    public void HP_SUBTRACT(int num)
    {
        HitPoints = HitPoints - num;
    }

}
