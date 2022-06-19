using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region µ¥Àý
    private static UIManager instance;
    public static UIManager Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    #region ×Ö¶Î
    public  PlayerController m_Playe;
    public GameObject m_Left0;
    public GameObject m_right0;
    public UIButton m_Left;
    public UIButton m_Right;
    public GameObject stop_Button;
    public GameObject x_Button2;
    public GameObject x_Button1;
    private GameObject m_StartUI;
    private GameObject m_GameUI;
    private GameObject m_Shop_UI;
    public GameObject X_Button0;
    public UIButton X_Button1;
    public UIButton X_Button2;
    private UILabel m__ScoreLabel;
    private UILabel m_GemLabel;
    private UILabel m_Game_SCoreLabel;
    private UILabel m_Game_GemLabel;
    private UIButton m__Play_Button;
    public   Sound m_Sound;
    private UIButton[] buttons =new UIButton[8];
    public UISlider m_Music;
    public UISlider m_Sournd;
    public AudioSource BkMusic;
    public AudioSource BkMusic0;
    private UIButton sound_Button;
    public UIButton Stop_Button;
    public AudioSource sound;
    public AudioSource sound0;
    #endregion
    void Start()
    {
        #region ×Ö¶Î³õÊ¼»¯
        sound0.volume = GameDataMgr.Instance.soundData.sound;
        sound.volume = GameDataMgr.Instance.soundData.sound;
        BkMusic.volume = GameDataMgr.Instance.soundData.music;
        BkMusic.Play();
        BkMusic0.Stop();
        m_GameUI = GameObject.Find("Game_UI");
        m_StartUI = GameObject.Find("Start_UI");
        m_StartUI.SetActive(true);
        m_GameUI.SetActive(false);
        m__ScoreLabel = m_StartUI.transform.GetChild(0).GetComponent<UILabel>();
        m_GemLabel = m_StartUI.transform.GetChild(1).GetComponent<UILabel>();
        m_Game_SCoreLabel = m_GameUI.transform.GetChild(0).GetComponentInChildren<UILabel>();
        m_Game_GemLabel = m_GameUI.transform.GetChild(1).GetComponentInChildren<UILabel>();
        m__Play_Button = m_StartUI.transform.GetChild(2).GetComponent<UIButton>();
        sound_Button = m_StartUI.transform.GetChild(11).GetComponent<UIButton>();
        for (int i = 0; i < 8; i++)
        {
            buttons[i]= m_StartUI.transform.GetChild(i+3).GetComponent<UIButton>();
            buttons[i].onClick.Add(new EventDelegate(Button));
        }
        sound_Button.onClick.Add(new EventDelegate(() =>
        {
            m_StartUI.SetActive(false);
            X_Button0.SetActive(true);
        }));
        Stop_Button.onClick.Add(new EventDelegate(() =>
        {
            Time.timeScale = 0;
            m_Left0.SetActive(false);
            m_right0.SetActive(false);
            stop_Button.SetActive(false);
            x_Button1.SetActive(false);
            X_Button0.SetActive(true);

        }));
        X_Button2.onClick.Add(new EventDelegate(() =>
        {
            Time.timeScale = 1;
            m_Left0.SetActive(true);
            m_right0.SetActive(true);
            stop_Button.SetActive(true);
            x_Button1.SetActive(true);
            X_Button0.SetActive(false);
        }));
        m__Play_Button.onClick.Add(new EventDelegate(() =>
        {
            BkMusic.Stop();
            BkMusic0.Play();
            m_StartUI.SetActive(false);
            m_GameUI.SetActive(true);
            m_Playe.make = true;
        }));
        X_Button1.onClick.Add(new EventDelegate(() =>
        {
            m_StartUI.SetActive(true);
            X_Button0.SetActive(false);
        }));
        m_Music.onChange.Add(new EventDelegate(() =>
        {
            BkMusic.volume = m_Music.value;
            BkMusic0.volume = m_Music.value;
            XmlDataMgr.Instance.SavaData("SoundData",new SoundData(m_Music.value,GameDataMgr.Instance.soundData.sound));
        }
        ));
        m_Sournd.onChange.Add(new EventDelegate(() =>
        {
            sound0.volume = m_Sournd.value;
            sound.volume = m_Sournd.value;
            XmlDataMgr.Instance.SavaData("SoundData", new SoundData(GameDataMgr.Instance.soundData.music, m_Sournd.value));
        }));
        m_Left.onClick.Add(new EventDelegate(() =>
        {
            m_Playe.Left();
        }));
        m_Right.onClick.Add(new EventDelegate(() =>
        {
            m_Playe.Right();
        }));
        X_Button0.SetActive(false);
        #endregion
        Init();
    }
    private void Update()
    {
       
    }
    // Update is called once per frame
    public void Init()
   {
      GameData data = XmlDataMgr.Instance.LoadData(typeof(GameData), "Data") as GameData;
      m__ScoreLabel.text =data.gsoucreCount.ToString();
      m_GemLabel.text =data.gemCount.ToString()+"/"+"100";
      m_Game_SCoreLabel.text = 0.ToString();
      m_Game_GemLabel.text = data.gemCount.ToString() + "/" + "100";

    }
    public void GameUIUPdate(int Score,int Gem)
    {
        m_Game_SCoreLabel.text = Score.ToString();
        m_Game_GemLabel.text = Gem + "/" + "100";
    }
    public void ResetUI()
    {
        m_StartUI.SetActive(true);
        m_GameUI.SetActive(false);
    }
    public void Button()
    {
        m_Sound.m_Sound.volume = 1f;
        m_Sound.m_Sound.Play();
    }
}
