using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Transform m_Transform;
    private Transform m_gem;
    void Start()
    {
        m_Transform =GetComponent<Transform>();
        m_gem =m_Transform.GetChild(0);
        m_gem.rotation = Quaternion.Euler(new Vector3(-33, 0, -49));
    }

    // Update is called once per frame
    void Update()
    {
        m_gem.rotation*=Quaternion.AngleAxis(1f,Vector3.up);
        m_gem.rotation *= Quaternion.AngleAxis(1f, Vector3.forward);
    }
}
