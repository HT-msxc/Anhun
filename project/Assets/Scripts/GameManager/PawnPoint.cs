using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Video;
public class PawnPoint : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public GameObject panel;
    public GameObject video;
    public VideoPlayer videoPlayer;
    public bool ok;
    AnimatorStateInfo info;
    GameObject pawnUI;
    public GameObject player;
    //控制玩家到达下个场景的位置
    /// <summary>
    /// 一进入场景判断保存的数据是不是和当前关卡一样，一样的话就得Set
    /// 不一样说明是从主入口进来的
    /// </summary>
    private void Awake()
    {
        string level = SceneManager.GetActiveScene().name;
        SaveData data = GameManager.Instence.GetGameData();
        Destroy(GameManager.Instence.CurrentPlayer);
        GameManager.Instence.CurrentPlayer = (GameObject)Instantiate(Resources.Load("Prefab/Player"));
        player = GameManager.Instence.CurrentPlayer;
        player.SetActive(true);
        InputManager.Instence.EscCounter = 0;
        //不等于 -----> 那就是别的关卡来的，不是加载的
        if(level != data.GameLevel)
        {
            if (video != null)
            {
                video.SetActive(true);
                videoPlayer.loopPointReached += EndVideo;
                GameManager.Instence.CurrentPlayer.transform.position = this.transform.position;
                virtualCamera.Follow = GameManager.Instence.CurrentPlayer.transform;
                GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = false;
                GameManager.Instence.SaveGameData();
            }
            else
            {
                jianchu();
            }
        }
        else
        {
            Destroy(panel);
            UIManager.Instence.PushUI(new StartPawn(),"Canvas");
            GameManager.Instence.SetPlayer(data);
            GameManager.Instence.CurrentPlayer.GetComponent<PlayerBattle>().SetRebirth();
            GameManager.Instence.SaveGameData();
            pawnUI = GameObject.Find("Pawn");
            virtualCamera.Follow = GameManager.Instence.CurrentPlayer.transform;
            GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = false;
            InputManager.Instence.EscCounter = 0;
        }
    }

    private void Update()
    {
        if(pawnUI != null)
        {
            info = pawnUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if(info.normalizedTime >= 1.0f && !ok)
            {
                UIManager.Instence.PopUI();
                Player player = GameManager.Instence.CurrentPlayer.GetComponent<Player>();
                player.CanOperate = true;
                AudioManager.Instance.PlayBackGroundMusic();
                GameManager.Instence.CurrentPlayer.GetComponent<PlayerInteractionWithSoulPiece>().RefreshSoulCages();
                player.SetPlayerSize(player.GetPlayerSize());
                SceneLoadManager.Instence.IsLoading = false;
                ok = true;
            }
        }
    }

    public void EndVideo(VideoPlayer videoPlayer)
    {
        video.SetActive(false);
        jianchu();
    }

    void jianchu()
    {
        UIManager.Instence.PushUI(new StartPawn(),"Canvas");
        GameManager.Instence.Pawn = this.gameObject;
        GameManager.Instence.CurrentPlayer.transform.position = this.transform.position;
        //保存
        GameManager.Instence.SaveGameData();
        pawnUI = GameObject.Find("Pawn");
        virtualCamera.Follow = GameManager.Instence.CurrentPlayer.transform;
        GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = false;
        InputManager.Instence.EscCounter = 0;
        Destroy(panel);
    }
}