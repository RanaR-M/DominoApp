using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private GameObject progressLabel;
    bool isConnecting;

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene =  true;
    }

    void Start() {
        progressLabel.SetActive(false);
        GamePanel.SetActive(true);
    }

    // Start is called before the first frame update
    
    public void connect() {
        progressLabel.SetActive(true);
        GamePanel.SetActive(false);

        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom();

        } else {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected to master");
        if (isConnecting) {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.LogError("no room availabe creating one");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined room");
        // load for the first player to enter
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1) {
            Debug.Log("Loading room");
            PhotonNetwork.LoadLevel("GamePage");
        }

    }
    public override void OnDisconnected(DisconnectCause cause) {
        progressLabel.SetActive(false);
        GamePanel.SetActive(true);
        isConnecting = false;
        Debug.LogErrorFormat("Disonnected reason: {0}", cause);
    }
}
