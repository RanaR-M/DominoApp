using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardControl : MonoBehaviour
{
    public List<Sprite> cardSprites = new List<Sprite>();
    //public List<GameObject> cardList = new List<GameObject>();
    public List<Sprite> opponentCards = new List<Sprite>();
    //public List<Sprite> playerCards = new List<Sprite>();
    //public List<GameObject> opponentCards = new List<GameObject>();
    public List<GameObject> playerCards = new List<GameObject>();
    public List<Text> cardText = new List<Text>();
    //public List<GameObject> bankCards = new List<GameObject>();
    public GameObject objectToCopy;
    public Transform objectParent;
    public Transform objectNewParent;
    public Transform newRow;

    // show cards 
    // make false in inspector to work with text

    public string TranslateMethod(Sprite Card) {
        // used to build the text ex: 1/5 using the name of the sprites 
        // can be used for arabic and english numbers 
        char[] nameArray = Card.name.ToCharArray();
        return nameArray[0] + "/" + nameArray[1];
    }

    public int TranslateObjectName(GameObject Card) {
        return int.Parse(Card.name.Split('_')[1]) - 1;
    }

    public void BankCardsOnclick(GameObject CardClicked) {
        Debug.Log(TranslateMethod(cardSprites[TranslateObjectName(CardClicked)]));
        CardClicked.GetComponent<Image>().sprite = cardSprites[TranslateObjectName(CardClicked)];
        CardClicked.GetComponent<Button>().onClick = null;
        playerCards.Add(CardClicked);
        if (playerCards.Count <= 9) {
            CardClicked.transform.SetParent(objectNewParent);
            CardClicked.transform.parent.localScale -= new Vector3(.1f, .1f, 0);
            CardClicked.transform.parent.localPosition -= new Vector3(11, 0, 0);
            if (CardClicked.transform.localScale != playerCards[0].transform.localScale) {
                CardClicked.transform.localScale = playerCards[0].transform.localScale;
            }
            Vector3 cardZeroPosition = playerCards[0].transform.localPosition;
            CardClicked.transform.localPosition = cardZeroPosition + new Vector3(90 * (playerCards.Count - 1), 60, 0);
        }
        else {
            CardClicked.transform.SetParent(newRow);
            
        }

        cardSprites.RemoveAt(TranslateObjectName(CardClicked));
    }

    void Start() {
        bool cardSwitch = PlayerPrefs.GetInt("Cards") == 0 ? true : false;
        // not mine so i don't know how it works.
        cardSprites.ShuffleMethod();
        // switch values
        if (cardSwitch) {
            foreach (GameObject card in playerCards) {
                card.SetActive(true);
            }
        }
        else {
            foreach (Text textCard in cardText) {
                // because text isn't a game object
                textCard.gameObject.SetActive(true);
            }
        }

        //cardText[0].text = "اهلا";

        // loops to assign the cards for both the player and the opponent
        // i don't check if the random number has been done before because i remove the sprite when chosen just like a real game
        for (int i = 0; i < playerCards.Count; i++) {
            int randomNumber = Random.Range(1, cardSprites.Count);
            playerCards[i].GetComponent<Image>().sprite = cardSprites[randomNumber];
            cardText[i].text = TranslateMethod(playerCards[i].GetComponent<Image>().sprite);
            cardSprites.RemoveAt(randomNumber);
        }
        int opponentCount = 7;
        for (int i = 0; i < opponentCount; i++) {
            int randomNumber = Random.Range(1, cardSprites.Count);
            opponentCards.Add(cardSprites[randomNumber]);
            cardSprites.RemoveAt(randomNumber);
        }

        // changing the sprites and texts of the cards
        // you access the text with just .text as it's already a text component so you don't need to use GetComponet<Text>()

        // this code is to see the opponentCards:

        //for (int i = 0, j = 0; i < 14; i++, j++) {
        //    if (j == 7) {
        //        j = 0;
        //    }
        //    if (i < 7) {
        //        // Image not SpriteRenderer as it's an Image not an object with an image.
        //        cardList[i].GetComponent<Image>().sprite = playerCards[i];
        //        cardText[i].text = TranslateMethod(playerCards[i]);
        //    } else {
        //        cardList[i].GetComponent<Image>().sprite = opponentCards[j];
        //        cardText[i].text = TranslateMethod(opponentCards[j]);
        //    }
        //}


        cardSprites.ShuffleMethod();
        int xZero = -220;
        int yZero = 230;
        for (int i = 0, x = xZero, y = yZero; i < cardSprites.Count; i++, x -= xZero / 2) {
            GameObject bankCard = Instantiate(objectToCopy, objectParent) as GameObject;
            bankCard.name = "BankCard_" + (i + 1);

            if (i == 0) {
                bankCard.transform.localPosition = new Vector2(x, y);
            }
            else {
                if (i % 5 == 0) {
                    y -= 160;
                    x = xZero;
                }
                bankCard.transform.localPosition = new Vector2(x, y);
            }
            bankCard.AddComponent<Button>().onClick.AddListener(() => BankCardsOnclick(bankCard));
            //bankCard.GetComponent<Image>().sprite = cardSprites[i];
        }




    }
}

