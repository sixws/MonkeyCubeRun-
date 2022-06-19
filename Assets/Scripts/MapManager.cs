using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地图管理器，显而易见。
/// </summary>

public class MapManager : MonoBehaviour
{
    #region 字段
    public List<GameObject[]> listMap = new List<GameObject[]>();
    private GameObject m_prefab_tile_white;
    private GameObject m_prefab_spikes;
    private Transform m_transform;
    private GameObject m_prefab_wall;
    private GameObject m_prefab_sky_spikes;
    private GameObject m_prefab_gem;
    public float bootomLength = 0.254f * Mathf.Sqrt(2);
    private Color colorOne = new Color(124 / 255f, 155 / 255f, 230 / 255f);
    private Color colorTwo = new Color(125 / 255f, 169 / 255f, 233 / 255f);
    private Color colorWall = new Color(87 / 255f, 93 / 255f, 169 / 255f);
    private int index = 0;
    public float time = 0.5f;
    private PlayerController m_Player;
    private int pr_hole = 0;
    private int pr_spikes = 0;
    private int pr_sky = 0;
    private int pr_gem = 2;
    #endregion
    void Start()
    {
        m_Player = GameObject.Find("cube_books").GetComponent<PlayerController>();
        m_transform =GetComponent<Transform>();
        m_prefab_tile_white = Resources.Load<GameObject>("tile_white");
        m_prefab_wall = Resources.Load<GameObject>("wall2");
        m_prefab_spikes = Resources.Load<GameObject>("moving_spikes");
        m_prefab_sky_spikes = Resources.Load<GameObject>("smashing_spikes");
        m_prefab_gem = Resources.Load<GameObject>("gem 2");
        CreateMapItem(0);  
    }
    void Update()
    {
       
    }
    public void CreateMapItem(float offsetZ)
    {
        Vector3 pos;
        GameObject tile =new GameObject();
        GameObject tile0 =new GameObject();
        for (int i = 0; i < 12; i++)
        {
            GameObject[] item0 = new GameObject[6];
            GameObject[] item =new GameObject[7];
            for (int j = 0; j < 7; j++)
            {
               
                //菱形地图生成算法
                if (j > 0 && j < 6)
                {
                    pos = new Vector3(j * bootomLength, 0, i * bootomLength+ offsetZ);
                    int pr = CalcPR();
                    if (pr == 0)
                    {
                        tile = Instantiate(m_prefab_tile_white, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                        tile.GetComponent<MeshRenderer>().material.color = colorOne;
                        tile.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorOne;  
                        if(CalcGemPR() == 1)
                        {
                          GameObject gem = Instantiate(m_prefab_gem, tile.GetComponent<Transform>().position + new Vector3(0, 0.06f, 0), Quaternion.identity);
                            gem.GetComponent<Transform>().SetParent(tile.transform);
                        }
                        
                    }
                    else if(pr == 1)
                    {
                        tile = new GameObject();
                        tile.name = "hole";
                        tile.GetComponent<Transform>().position = pos;
                        tile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward);
                    }
                    else if (pr == 2)
                    {
                        tile = Instantiate(m_prefab_spikes, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                    }
                    else if(pr == 3)
                    {
                        tile = Instantiate(m_prefab_sky_spikes, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                    }
                    item[j] = tile;
                    tile.transform.SetParent(m_transform);
                }
                else
                {
                    pos = new Vector3(j * bootomLength, 0, i * bootomLength+ offsetZ);
                    tile = Instantiate(m_prefab_wall, pos, Quaternion.AngleAxis(-90, Vector3.right));
                    tile.GetComponent<MeshRenderer>().material.color = colorWall;
                    tile.transform.rotation *= Quaternion.AngleAxis(45, Vector3.forward); 
                    item[j] = tile;
                    tile.transform.SetParent(m_transform);
                }
                if (j < 6)
                {
                    int pr = CalcPR();
                    pos = new Vector3(j * bootomLength + bootomLength / 2, 0, i * bootomLength + bootomLength / 2+ offsetZ);
                    if(pr == 0)
                    {
                        tile0 = Instantiate(m_prefab_tile_white, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                        tile0.GetComponent<MeshRenderer>().material.color = colorTwo;
                        tile0.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorTwo;
                    }
                    else if (pr == 1)
                    {
                        tile0 = new GameObject();
                        tile0.GetComponent<Transform>().position = pos;
                        tile0.GetComponent<Transform>().rotation = Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward);
                    }
                    else if (pr == 2)
                    {
                        tile0 = Instantiate(m_prefab_spikes, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                    }
                    else if (pr == 3)
                    {
                        tile0 = Instantiate(m_prefab_sky_spikes, pos, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(45, Vector3.forward));
                    }
                    item0[j] = tile0;
                    tile0.transform.SetParent(m_transform);
                }   
            }
            listMap.Add(item);
            listMap.Add(item0);
        }
    }
    private IEnumerator TileDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            for (int i = 0; i < listMap[index].Length; i++)
            {
               Rigidbody rb = listMap[index][i].AddComponent<Rigidbody>();
                rb.angularVelocity = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) *Random.Range(1,15);
               Destroy(listMap[index][i],2);
            }
            if (index == m_Player.z)
            {
                m_Player.gameObject.AddComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * Random.Range(1, 15);
                m_Player.StartCoroutine(m_Player.GameOver(true));
                StopileDown();
            }
            index++;  
        }
    }
    public void StartTileDown()
    {
        StartCoroutine(TileDown());
    }
    public void StopileDown()
    {
        StopCoroutine(TileDown());
    }
    /// <summary>
    /// 计算概率
    /// </summary>
    /// <returns></returns>
    private int CalcPR()
    {
        int pr = Random.Range(1, 100);
        if (pr <= pr_hole)
            return 1;
        else if(31<pr &&pr<pr_spikes+30)
            return 2;
        else if(61<pr && pr< pr_sky + 60)
            return 3;
        return 0;
    }
    public void AddPR()
    {
        pr_hole += 2;
        pr_spikes++;
        pr_sky++;
        pr_hole= Mathf.Clamp(pr_hole, 0, 30);
        pr_spikes= Mathf.Clamp(pr_spikes, 0, 20);
        pr_sky= Mathf.Clamp(pr_sky, 0, 10);
    }
    public int CalcGemPR()
    {
        int pr =Random.Range(1, 100);
        return pr <= pr_gem ? 1 : 0;
    }
    public void ResetGameMap()
    {
        Transform [] sonTransform = m_transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < sonTransform.Length; i++)
        {
            Destroy(sonTransform[i].gameObject);
        }
         pr_hole = 0;
         pr_spikes = 0;
         pr_sky = 0;
        index = 0;
        listMap.Clear();
        CreateMapItem(0);
    }
}
