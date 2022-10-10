using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject panelLogin;
    [SerializeField] GameObject panelLobby;

    public Text lobbyStartTime;
    public InputField playerNameInputField;

    public string playerName;
    public Text connectionStatusText;

    // Start is called before the first frame update
    void Start()
    {
        lobbyStartTime.gameObject.SetActive(false);
        connectionStatusText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PanelLobbyActive()
    {
        panelLobby.SetActive(true);
        panelLogin.SetActive(false);
    }

    public void PanelLoginActive()
    {
        panelLobby.SetActive(false);
        panelLogin.SetActive(true);
    }
}
