using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDetails : MonoBehaviour
{
    public enum Card_Type { DEFAULT, ATTACK, MOVEMENT, COMBO };
    public enum Card_Name
    {
        DEFAULT, STRAIGHT, POWER_STRAIGHT, FRONT_KICK, POWER_FRONT_KICK, BACK_HAND,
        JUMPING_KICK, COMBOx2, COMBOx3, COMBOx4, BLOCK, LEAN, PLANT, MOMENTUM, 
        JAB, POWER_JAB, HOOK, POWER_HOOK, UPPERCUT, HAYMAKER, BOXING_COMBOx2, BOXING_COMBOx3,
        BOXING_COMBOx4, CROUCH, ROTATE, BOXING_MOMENTUM, BOXING_BLOCK
    };

    public int nAttackValue;
    public int nComboAmount;
    public Card_Name CardName;
    public Card_Type CardType;
    public bool bRequiresMovement;
    public Card_Name MovementRequired;

    public int nTurnsLasted = 0;

    private bool bHasBeenUsed = false;
    public bool GetHasBeenUsed() { return bHasBeenUsed; }
    public void SetHasBeenUsed(bool bUsed) { bHasBeenUsed = bUsed; }

    bool m_bSetToBeUsed = false;

    Image CardRenderImage;
    public Sprite Card_Front;
    public Sprite Card_Back;


    // Use this for initialization
    void Start()
    {
        CardRenderImage = GetComponent<Image>();
        //Card_Front = CardRenderImage.sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp(int attackvalue, int comboamount, Card_Name cardname, Card_Type cardtype, bool requiesmovement, Card_Name movementrequired)
    {
        nAttackValue = attackvalue;
        nComboAmount = comboamount;
        CardName = cardname;
        CardType = cardtype;
        bRequiresMovement = requiesmovement;
        MovementRequired = movementrequired;
    }

    public bool IsSetToBeUsed()
    {
        return m_bSetToBeUsed;
    }

    public void SetCardToBeUsed()
    {
        m_bSetToBeUsed = true;
    }

    public void RevealCard()
    {
        CardRenderImage = GetComponent<Image>();
        CardRenderImage.sprite = Card_Front;
    }

    public void TurnDownCard()
    {
        CardRenderImage = GetComponent<Image>();
        CardRenderImage.sprite = Card_Back;
    }
}
