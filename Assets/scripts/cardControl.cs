using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardControl : MonoBehaviour {
    public List<Sprite> cardSprites = new List<Sprite>();
    public List<GameObject> cardList = new List<GameObject>();
    public List<Sprite> opponentCards = new List<Sprite>();
    public List<Sprite> playerCards = new List<Sprite>();

    // Start is called before the first frame update
    void Start() {
        cardSprites.ShuffleMethod();
        for (int i = 0; i < 7; i++) {
            int randomNumber = Random.Range(1, cardSprites.Count);
            playerCards.Add(cardSprites[randomNumber]);
            cardSprites.RemoveAt(randomNumber);
        }
        for (int i = 0; i < 7; i++) {
            int randomNumber = Random.Range(1, cardSprites.Count);
            opponentCards.Add(cardSprites[randomNumber]);
            cardSprites.RemoveAt(randomNumber);
        }

        for (int i = 0, j = 0; i < 14; i++, j++) {
            if (j == 7) {
                j = 0;
            }
            if (i < 7) {
                cardList[i].GetComponent<Image>().sprite = playerCards[i];
            } else {
                cardList[i].GetComponent<Image>().sprite = opponentCards[j];
            }
        }

    }

    void Update() {

    }
}
