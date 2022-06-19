using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioClip m_Clip;
    public  AudioSource m_Sound;
    private GameObject GameObject;
    private AudioSource m_BKMusic;
    private AudioSource m_BGMusic;
    private float music;
    private void Start()
    {
        music = GameDataMgr.Instance.soundData.music;
        GameObject =gameObject;
        GameObject.AddComponent<AudioSource>();
        m_Sound = GameObject.GetComponent<AudioSource>();
        m_Clip = Resources.Load<AudioClip>("warn");
        m_Sound.clip  = m_Clip;
        m_Sound.playOnAwake = false;
        m_Sound.volume = GameDataMgr.Instance.soundData.sound;
        m_Sound.loop = false;
        m_BKMusic =GameObject.transform.GetChild(0).GetComponent<AudioSource>();
        m_BGMusic = GameObject.transform.GetChild(0).GetComponent<AudioSource>();
        m_BKMusic.volume = music;
        m_BGMusic.volume = music;
    }
}
