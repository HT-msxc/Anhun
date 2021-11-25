using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System;
public class GameManager : Singleton<GameManager>
{
    public bool PlayerDead = false;
    public static int DeadCounter;
    public GameObject CurrentPlayer;
    public static SaveData GameData = new SaveData();
    public GameObject Pawn;
    public Transform StartTransformOfMainScene;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    Player player;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        CurrentPlayer = (GameObject)Instantiate(Resources.Load("Prefab/Player"));
        CurrentPlayer.name = "Player";
        CurrentPlayer.SetActive(false);
        DontDestroyOnLoad(CurrentPlayer);
        try
        {
            StreamReader  reader = new StreamReader(Application.dataPath + "/SaveData.txt");
        }
        catch (FileNotFoundException)
        {
            SaveGameData();
        }
        player = CurrentPlayer.GetComponent<Player>();
        UIManager.Instence.PushUI(new MainButtonUI(), "Canvas");
    }

    //FIXME: 不应该在这判断
    private void Update()
    {

    }

    /// <summary>
    /// 每次进入场景必须调用这个 以及setPlayer;
    /// </summary>
    public void SaveGameData()
    {
        if(CurrentPlayer == null)
        {
            //FIXME: 玩家名字可能不是player
            CurrentPlayer = GameObject.Find("Player");
        }
        GameData.PlayerStateData = NatureState.Normal;
        GameData.PlayerSizeData  = PlayerSize.Middle;
        GameData.PositionData = CurrentPlayer.transform.position;
        GameData.RotationData = CurrentPlayer.transform.rotation;
        GameData.DeathCounter = DeadCounter;
        GameData.GameLevel = SceneManager.GetActiveScene().name;
        string saveData = JsonUtility.ToJson(GameData);
        FileStream file = new FileStream(Application.dataPath + "/SaveData.txt",FileMode.Create);
        Debug.Log(saveData);
        Debug.Log(Application.dataPath);
        byte[] bt = new UTF8Encoding().GetBytes(saveData);
        file.Write(bt,0,bt.Length);
        file.Close();
    }

    public SaveData GetGameData()
    {
        StreamReader  reader = new StreamReader(Application.dataPath + "/SaveData.txt");
        string data = reader.ReadToEnd();
        GameData = JsonUtility.FromJson<SaveData>(data);
        Debug.Log(data);
        reader.Close();
        return GameData;
    }

    public void SetPlayer(SaveData data)
    {
        Destroy(GameManager.Instence.CurrentPlayer);
        GameManager.Instence.CurrentPlayer = (GameObject)Instantiate(Resources.Load("Prefab/Player"));
        CurrentPlayer.transform.position = data.PositionData;
        CurrentPlayer.transform.rotation = data.RotationData;
        //FIXME: null ---->
        //CurrentPlayer.GetComponent<Player>().SetNatureState(data.PlayerStateData);
        CurrentPlayer.GetComponent<Player>().SetNatureState(NatureState.Normal);
        CurrentPlayer.GetComponent<Player>().SetPlayerSize(data.PlayerSizeData);
        DeadCounter = data.DeathCounter;
        GameObject.Find("PawnPoint").GetComponent<PawnPoint>().virtualCamera.Follow = GameManager.Instence.CurrentPlayer.transform;
    }
}
