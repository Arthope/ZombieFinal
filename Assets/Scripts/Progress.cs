using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

[System.Serializable]

public class PlayerInfo
{
    public int Coins;
    int _coins;
    public int Level;
}

public class Progress : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public static Progress Instance;


    [SerializeField] TextMeshProUGUI _playerInfoText;

    private void Awake()
    {
       if(Instance == null)
       {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
       }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = PlayerInfo.Coins + "\n" + PlayerInfo.Level;
    }
}
