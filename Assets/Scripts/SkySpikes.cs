using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkySpikes : SpikesBase
{
    // Start is called before the first frame update
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_son_Transform = m_Transform.GetChild(1);
        normalPos = m_son_Transform.position;
        targetPos = m_son_Transform.position + new Vector3(0, 0.6f, 0);
        StartCoroutine(UpAddDown()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
