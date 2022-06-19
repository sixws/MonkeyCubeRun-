using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 显而易见这是一个角色控制器脚本
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform m_Transform;
    private MapManager m_MapManager;
    public  int z = 5;
    public  int x = 4;
    private Color colorOne = new Color(122 / 255f, 85 / 255f, 179 / 255f);
    private Color colorTwo = new Color(126 / 255f, 93 / 255f, 183 / 255f);
    int y;
    private bool life = true;
    private CameraFollow m_CameraFollow;
    private int num;
    public AudioSource BkMusic;
    public AudioSource BkMusic0;
    public AudioSource sound;
    public AudioSource sound0;
    public bool make = false;
    private void AddGemCount()=>GameDataMgr.Instance.data.gemCount++;
    private void AddGsoucreCount() => num++;
    void Start()
    {
        m_Transform =GetComponent<Transform>();
        m_MapManager = GameObject.Find("MapManager").gameObject.GetComponent<MapManager>();
        m_CameraFollow = GameObject.Find("Main Camera").gameObject.GetComponent<CameraFollow>();
    }                                                                                   
    void Update()
    {
            if (make)
            {
                make=false;
                Time.timeScale = 1;
                m_CameraFollow.startFollow = true;
                SetPlayerPos();
                m_MapManager.StartTileDown();
            }
      if(life)
            PlayerContorl();
        CalcPosition();
    }
    private void SetPlayerPos()
    {
        Transform playerPos = m_MapManager.listMap[z][x].transform;
        m_Transform.position = playerPos.position + new Vector3(0, 0.254f / 2, 0);
        m_Transform.rotation = playerPos.transform.rotation;
        if(playerPos.tag == "tile"||playerPos.tag == "GroundSpikes"|| playerPos.tag == "SkySpikes")
        {
            MeshRenderer normal_a2 = playerPos.GetChild(0).GetComponent<MeshRenderer>();
            normal_a2.material.color = z % 2 == 0 ? colorOne : colorTwo;
        }
        else
        {
            gameObject.AddComponent<Rigidbody>();
            StartCoroutine(GameOver(true));
        }
    }
    /// <summary>
    /// 角色移动控制函数
    /// </summary>
    private void PlayerContorl()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
           Right();
        }
      
    }
    public void Left()
    {
        z++;
        x -= z % 2 == 0 ? 0 : 1;
        if (!(z % 2 == 0 && x == 0))
        {
            SetPlayerPos();
            y = z;
        }
        else
        {
            z = y;
            x = 0;
        }
        sound.Play();
        AddGsoucreCount();
        UIManager.Instance.GameUIUPdate(num, GameDataMgr.Instance.data.gemCount);
    }
    public void Right()
    {
        z++;
        x += z % 2 == 0 ? 1 : 0;
        if (!(z % 2 == 0 && x == 6))
        {
            SetPlayerPos();
            y = z;
        }
        else
        {
            z = y;
            x = 5;
        }
        sound.Play();
        AddGsoucreCount();
        UIManager.Instance.GameUIUPdate(num, GameDataMgr.Instance.data.gemCount);
    }
    /// <summary>
    /// 位置计算
    /// </summary>
    private void CalcPosition()
    {
        if (m_MapManager.listMap.Count - z <= 17)
        {
            m_MapManager.AddPR();
            float offsetZ = m_MapManager.listMap[m_MapManager.listMap.Count-1][0].GetComponent<Transform>().position.z+m_MapManager.bootomLength/2f;
            m_MapManager.CreateMapItem(offsetZ);
        }
    }
    public IEnumerator GameOver(bool b=false)
    { 
        if (life)
        {
            Debug.Log("Game Over");
            life = false;
            m_CameraFollow.startFollow = false;
            if(num> GameDataMgr.Instance.data.gsoucreCount)
                GameDataMgr.Instance.data.gsoucreCount=num;
            XmlDataMgr.Instance.SavaData("Data", GameDataMgr.Instance.data);
            StartCoroutine(ResetGame());
            UIManager.Instance.Init();     
        }
        if (b)
            yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Spikes_Attack")
        StartCoroutine(GameOver());
        if(other.tag == "Gem")
        {
            Destroy(other.gameObject.GetComponent<Transform>().parent.gameObject);
            AddGemCount();
            sound0.Play();
            UIManager.Instance.GameUIUPdate(num, GameDataMgr.Instance.data.gemCount);
        }
    }
    private IEnumerator ResetGame()
    {
        yield return new WaitForSecondsRealtime(2);
        BkMusic.Play();
        BkMusic0.Stop();
        ResetPlayer();
        m_MapManager.ResetGameMap();
        m_CameraFollow.ResetCamera();
        UIManager.Instance.ResetUI();
    }
    private void ResetPlayer()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        z = 6;
        x = 4;
        num = 0;
        life = true;

    }
}
