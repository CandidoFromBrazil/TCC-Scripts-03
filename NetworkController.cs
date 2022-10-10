using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] LobbyManager lobbySystem;
    [SerializeField] byte playerRoomMax = 2;

    // Start is called before the first frame update
    void Start()
    {

    }


    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        lobbySystem.PanelLobbyActive();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        string roomName = "Room" + Random.Range(1000, 10000);

        //Propriedades da sala
        RoomOptions roomOptions = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = playerRoomMax
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        Debug.Log(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }


    void StartGame()
    {
        PhotonNetwork.LoadLevel("SceneGame");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");

        //Verificar se a sala já está cheia
        if (PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                //O Master Client será o responsável por carregar a cena de jogo
                if (player.IsMasterClient)
                {
                    StartGame();
                    //Chama o Countdown
                    Hashtable props = new Hashtable
                            {
                                {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
                            };

                    PhotonNetwork.CurrentRoom.SetCustomProperties(props);
                }
            }
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(CountdownTimer.CountdownStartTime))
        {
            //Aparecer contador para todos os players quandos as propriedades da sala forem atualizadas
            lobbySystem.lobbyStartTime.gameObject.SetActive(true);
        }
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnConnectedToMaster: " + cause.ToString());
        lobbySystem.PanelLoginActive();
    }

    public void CancelMatch()
    {
        PhotonNetwork.Disconnect(); //print: DisconnectByClientLogic

        lobbySystem.connectionStatusText.gameObject.SetActive(false);
    }

    public void SearchMatch()
    {
        PhotonNetwork.NickName = lobbySystem.playerNameInputField.text;

        lobbySystem.connectionStatusText.gameObject.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();
    }

    void OnCountdownTimeIsExpired()
    {
        StartGame();
    }


    //Countdown
    public override void OnEnable()
    {
        base.OnEnable();

        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimeIsExpired;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimeIsExpired;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
