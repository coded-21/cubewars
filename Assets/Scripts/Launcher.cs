using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance { get; private set; }

    public TMP_InputField hostInput;
    public TMP_InputField joinInput;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItem;

    private void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connected");
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        MenuManager.Instance.OpenMenu("title");
        //SceneManager.LoadScene("Lobby");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(hostInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(hostInput.text);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform room_t in roomListContent)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItem, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
}