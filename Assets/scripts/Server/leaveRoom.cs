using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class leaveRoom : MonoBehaviourPunCallbacks
{
    public navigation navObject ;
    public override void OnLeftRoom() {
        navObject.loadScene("Lobby");
    }


    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.LogFormat("Player {0} entered", newPlayer.NickName);
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("player is connected to master");
            LoadArena();
        }
    }
    public override void OnPlayerLeftRoom(Player newPlayer) {
        Debug.LogFormat("Player {0} left", newPlayer.NickName);
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("player is connected to master");
            LoadArena();
        }
    }

    void LoadArena() {
        if (!PhotonNetwork.IsMasterClient) {
            Debug.LogError("Not connected to master");
        }
        Debug.Log("connecting to Arena");
        PhotonNetwork.LoadLevel("GamePage");

    }

}
