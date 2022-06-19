using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÉãÏñ»ú¸úËæ½ÇÉ«ÒÆ¶¯
/// </summary>
public class CameraFollow : MonoBehaviour
{
    private Transform m_Transform;
    private Transform m_Player_Transform;
    public bool startFollow = false;
    private Vector3 normalPos;
    void Start()
    {  
        m_Transform =GetComponent<Transform>();
        m_Player_Transform =GameObject.Find("cube_books").GetComponent<Transform>();
        normalPos = m_Transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CamerMove();
    }
    void CamerMove()
    {
        if (startFollow)
        {
            Vector3 nextPos = new Vector3(m_Transform.position.x, m_Player_Transform.position.y+ 3.033f, m_Player_Transform.position.z);
            m_Transform.position = Vector3.Lerp(m_Transform.position, nextPos, Time.deltaTime);
        }
    }
    public void ResetCamera()
    {
        m_Transform.position = normalPos;
        m_Player_Transform = GameObject.Find("cube_books").GetComponent<Transform>();
    }
}
