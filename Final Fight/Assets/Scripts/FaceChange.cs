using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FaceChange : MonoBehaviour 
{
    public Player PlayerObject;
    int m_nPlayerHP = 0;

    Image FaceStage_Image;

    public Sprite _25up;
    public Sprite _20up;
    public Sprite _15up;
    public Sprite _10up;
    public Sprite _5up;
    public Sprite _0up;

	// Use this for initialization
	void Start () 
    {
        FaceStage_Image = GetComponent<Image>();
        m_nPlayerHP = PlayerObject.HitPoints;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_nPlayerHP = PlayerObject.HitPoints;

        if (m_nPlayerHP >= 25)
            FaceStage_Image.sprite = _25up;
        else if (m_nPlayerHP < 25 && m_nPlayerHP >= 20)
            FaceStage_Image.sprite = _20up;
        else if (m_nPlayerHP < 20 && m_nPlayerHP >= 15)
            FaceStage_Image.sprite = _15up;
        else if (m_nPlayerHP < 15 && m_nPlayerHP >= 10)
            FaceStage_Image.sprite = _10up;
        else if (m_nPlayerHP < 10 && m_nPlayerHP >= 5)
            FaceStage_Image.sprite = _5up;
        else if (m_nPlayerHP < 5)
            FaceStage_Image.sprite = _0up;         
	}
}
