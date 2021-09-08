using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance { get; private set; }

    public TMP_InputField hostInput;
    public TMP_InputField joinInput;

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
        SceneManager.LoadScene("Lobby");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(hostInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(hostInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}