using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UserPlayer Player_1;
    public UserPlayer Player_2;

    public GameObject action_prompt;
    public GameObject playerTurn_Text;
    public GameObject WarningSound;

    public GameObject passTheDeviceOverlay;

    public GameObject winnerText;

    public int nCurrentTurn = 0;
    public bool bAttack = false;
    public bool bRebuttal = false;

    public GameObject GAME_HUD;

    Color colorOrange;
    Color colorYellow;
    Color colorRed;
    Color colorTransparent;

    public Material theMaterial;

    int m_nAttackValue;
    int m_nRebuttalValue;
    bool m_bAttackBlock;
    bool m_bRebuttalBlock;

    // Use this for initialization
    void Start()
    {
        passTheDeviceOverlay.GetComponent<PassTheDeviceOverlay>().Reveal(false);
        winnerText.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.0f);

        bAttack = true;
        ColorUtility.TryParseHtmlString("FFAF03FF", out colorOrange);
        ColorUtility.TryParseHtmlString("F2FF16FF", out colorYellow);
        ColorUtility.TryParseHtmlString("E40000FF", out colorRed);
        ColorUtility.TryParseHtmlString("00000000", out colorTransparent);

        validateHandReveal();
    }

    void Awake()
    {
        instance = this;
        validateHandReveal();
    }

    // Update is called once per frame
    void Update()
    {
        InputHelper();

        validateHandReveal();
    }

    public void NextTurn()
    {
        if (bAttack == true)
        {
            Attack_PlayCards();
            if (m_nAttackValue == -1)
            {
                //Debug.Log("AttackValue = -1");
                WarningSound.GetComponent<AudioSource>().PlayOneShot(WarningSound.GetComponent<AudioSource>().GetComponent<AudioClip>());
                return;
            }
            //add any staged cards to the staging area

            // Switch the turn only after an Attack
            SwitchTurn();

            bAttack = false;
            bRebuttal = true;



            action_prompt.GetComponent<ImageSwap>().SetImageToRebuttal();

        }
        else if (bRebuttal == true)
        {
            Rebuttal_PlayCards();
            if (m_nRebuttalValue == -1)
            {
                //Debug.Log("AttackValue = -1");
                WarningSound.GetComponent<AudioSource>().PlayOneShot(WarningSound.GetComponent<AudioSource>().GetComponent<AudioClip>());
                return;
            }

            bRebuttal = false;
            bAttack = true;

            action_prompt.GetComponent<ImageSwap>().SetImageToAttack();

            //apply damage values
            if (nCurrentTurn == 1)
            {
                Player_1.HitPoints -= m_nAttackValue;
                Player_2.HitPoints -= m_nRebuttalValue;

                if (m_bAttackBlock)
                    Player_2.HitPoints += Player_1.GetHighestAttackValuePlayed();

                if (m_bRebuttalBlock)
                    Player_1.HitPoints += Player_2.GetHighestAttackValuePlayed();
            }
            else
            {
                Player_2.HitPoints -= m_nAttackValue;
                Player_1.HitPoints -= m_nRebuttalValue;

                if (m_bAttackBlock)
                    Player_1.HitPoints += Player_2.GetHighestAttackValuePlayed();

                if (m_bRebuttalBlock)
                    Player_2.HitPoints += Player_1.GetHighestAttackValuePlayed();
            }

            Player_2.DiscardPlayedCards();
            Player_1.DiscardPlayedCards();

            DrawCards();

            CheckWinCondition();
        }

    }

    void InputHelper()
    {
        if (Input.GetKeyDown(KeyCode.D)) // Draw test
        {
            winnerText.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.F)) // Draw test
        {
            winnerText.transform.localPosition = new Vector3(0.0f, 10000.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.Alpha1)) // Subtract hp from player_1
        {
            Player_1.HP_SUBTRACT(1);
        }

        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.Alpha2)) // Subtract hp from player_2
        {
            Player_2.HP_SUBTRACT(1);
        }


        if (Input.GetKeyDown(KeyCode.A)) // Subtract hp
        {
            Player_1.HP_ADD(1);
        }
    }

    void setColor(float r, float g, float b, float a, out Color c)
    {
        c.r = r;
        c.g = g;
        c.b = b;
        c.a = a;
    }

    void SwitchTurn()
    {
        // Throw up turn switch screen overlay
        passTheDeviceOverlay.GetComponent<PassTheDeviceOverlay>().Reveal(true);

        if (nCurrentTurn == 1)
        {
            nCurrentTurn = 2;
            playerTurn_Text.GetComponent<Text>().text = "Player 2";
            Player_1.TurnDownHand();
            Player_2.RevealHand();
        }
        else
        {
            nCurrentTurn = 1;
            playerTurn_Text.GetComponent<Text>().text = "Player 1";
            Player_1.RevealHand();
            Player_2.TurnDownHand();
        }
    }

    void Attack_PlayCards()
    {
        int nAttackValue = 0;

        if (nCurrentTurn == 1)
        {
            nAttackValue = Player_1.PlayCards();
            m_bAttackBlock = Player_1.bBlockCardPlayed();
        }
        else if (nCurrentTurn == 2)
        {
            nAttackValue = Player_2.PlayCards();
            m_bAttackBlock = Player_2.bBlockCardPlayed();
        }

        m_nAttackValue = nAttackValue;
    }

    void Rebuttal_PlayCards()
    {
        int nRebuttalValue = 0;

        if (nCurrentTurn == 1)
        {
            nRebuttalValue = Player_1.PlayCards();
            m_bRebuttalBlock = Player_1.bBlockCardPlayed();
        }
        else if (nCurrentTurn == 2)
        {
            nRebuttalValue = Player_2.PlayCards();
            m_bRebuttalBlock = Player_2.bBlockCardPlayed();
        }

        m_nRebuttalValue = nRebuttalValue;
    }

    static GameManager GetInstance()
    {
        return instance;
    }

    public int GetTurn()
    {
        return nCurrentTurn;
    }

    void validateHandReveal()
    {
        if (nCurrentTurn == 1)
        {
            Player_1.RevealHand();
            Player_2.TurnDownHand();
        }
        else
        {
            Player_2.RevealHand();
            Player_1.TurnDownHand();
        }
    }

    void DrawCards()
    {
        if (nCurrentTurn == 1)
        {
            Player_1.DrawCards(true);
            Player_2.DrawCards(false);
        }
        else
        {
            Player_1.DrawCards(false);
            Player_2.DrawCards(true);
        }
    }

    void CheckWinCondition()
    {
        Text txt = winnerText.GetComponent<Text>();
        bool winner = false;

        if (Player_1.HitPoints == 0 && Player_2.HitPoints == 0)
        {
            txt.text = "TIE";
            txt.color = Color.white;
            winner = true;
        }
        else if (Player_1.HitPoints == 0)
        {
            txt.text = "Player 2 Wins!";
            txt.color = Color.red;
            winner = true;
        }
        else if (Player_2.HitPoints == 0)
        {
            txt.text = "Player 1 Wins!";
            txt.color = Color.blue;
            winner = true;
        }

        if (winner)
        {
            winnerText.transform.localPosition = new Vector2(0.0f, 0.0f);
        }

    }
}
