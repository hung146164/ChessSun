using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using TMPro;
using UnityEngine;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;



    [Header("UI")]
    [SerializeField] private TMP_Text roomName;

    [SerializeField] private TMP_InputField createRoomNameInput;

    [SerializeField] private TMP_InputField findRoomNameInput;

    [Header("Start Game")]
    [SerializeField] GameObject startGameButton;

    [Header("List Content")]
    [SerializeField] private ListContentString playerListContent;

    [Header("Menu")]
    [SerializeField] private MenuManager parentMenuManager;
    [SerializeField] private MenuManager onlineMenuManager;

    private GameManager gameManager;
    private FirebaseAuthManager firebaseAuthManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameManager=FindObjectOfType<GameManager>();
        firebaseAuthManager=FindObjectOfType<FirebaseAuthManager>();
    }
    #region Base Connection
    public void ConnectToServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            parentMenuManager.OpenMenu("online");
            onlineMenuManager.OpenMenu("loading");
            PhotonNetwork.NickName = $"Player {Random.Range(1000, 9999)}";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            parentMenuManager.OpenMenu("online");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        onlineMenuManager.OpenMenu("main");
    }
    #endregion
    public void CreateRoom()
    {
        string roomName = createRoomNameInput.text;

        onlineMenuManager.OpenMenu("loading");

        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions
            {
                MaxPlayers = 2,
                IsVisible = true,
                IsOpen = true,
                EmptyRoomTtl = 0
            });
        }
        else
        {
            onlineMenuManager.OpenMenu("main");
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create Room Faild");
        onlineMenuManager.OpenMenu("main");
    }

    public void FindRoom()
    {
        PhotonNetwork.JoinRoom(findRoomNameInput.text);
        onlineMenuManager.OpenMenu("loading");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Find Fail!");
        onlineMenuManager.OpenMenu("main");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        parentMenuManager.OpenMenu("room");

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        playerListContent.ClearListContent();
        playerListContent.UpdateListContent(PhotonNetwork.PlayerList.Select(player => player.NickName).ToList());
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerListContent.AddItem(newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerListContent.RemoveItem(otherPlayer.NickName);
    }
    public override void OnLeftRoom()
    {
        parentMenuManager.OpenMenu("online");
        onlineMenuManager.OpenMenu("main");
    }
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
    
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
           
        }
    }
    

}