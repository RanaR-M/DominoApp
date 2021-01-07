using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

[RequireComponent(typeof(InputField))]
public class playerNameInputField : MonoBehaviour
{

    const string playerNamePrefsKey = "PlayerName";
    // Start is called before the first frame update
    void Start() {
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();

        if (_inputField != null) {
            if (PlayerPrefs.HasKey(playerNamePrefsKey)) {
                defaultName = PlayerPrefs.GetString(playerNamePrefsKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    // Update is called once per frame
    public void SetPlayerName(string value) {
        if (string.IsNullOrEmpty(value)) {
            Debug.LogError("Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefsKey, value);
    }
}
