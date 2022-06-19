using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
   private static GameDataMgr instance =new GameDataMgr();
    public static GameDataMgr Instance => instance;
    private GameDataMgr()
    {
        data = XmlDataMgr.Instance.LoadData(typeof(GameData), "Data") as GameData;
        soundData =XmlDataMgr.Instance.LoadData(typeof(SoundData), "SoundData") as SoundData;
    }
    public GameData data;
    public SoundData soundData;
}
