using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBase : MonoBehaviour
{
    protected Transform m_Transform;
    protected Transform m_son_Transform;
    protected Vector3 normalPos;
    protected Vector3 targetPos;
    protected Vector3 startPos;
    protected Vector3 Pos;
    float t = 0;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private IEnumerator Up()
    {
        startPos = normalPos;
        while (true)
        {
            t += 0.02f;
          
            m_son_Transform.position = Vector3.Lerp(startPos, targetPos, t*0.1f);
          
            yield return null;
        }
    }
    private IEnumerator Down()
    {
        startPos = targetPos;
        while (true)
        {
            t += 0.02f;
            m_son_Transform.position = Vector3.Lerp(startPos, normalPos, t*0.1f);
           yield return null;
        }
    }
    protected IEnumerator UpAddDown()
    {
        while (true)
        {
            StartCoroutine(Up());
            t = 0;
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(Down());
            t = 0;
            yield return new WaitForSeconds(2.0f);
        }
    }
}
