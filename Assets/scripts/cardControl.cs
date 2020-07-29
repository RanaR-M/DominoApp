﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardControl : MonoBehaviour {
    public List<Sprite> cardSprites = new List<Sprite>();
    public List<GameObject> cardList = new List<GameObject>();
    public List<Sprite> opponentCards = new List<Sprite>();
    public List<Sprite> playerCards = new List<Sprite>();
    public List<Text> cardText = new List<Text>();

    // TODO:
    // make static values that come from the switches so that you can chose either the cards or text.
    // i made the code that activates or deactivate the card / text need to get it from the switch

    public bool cardSwitch = true; // show cards 
    // make false in inspector to work with text

    public string TranslateMethod(Sprite Card) {
        // used to build the text ex: 1/5 using the name of the sprites 
        // can be used for arabic and english numbers 
        char[] nameArray = Card.name.ToCharArray();
        return nameArray[0] + "/" + nameArray[1];
    }
    void Start() {
        // not mine so i don't know how it works.
        cardSprites.ShuffleMethod();

        // switch values
        if (cardSwitch) {
            foreach (GameObject card in cardList) {
                card.SetActive(true);
            }
        } else {
            foreach (Text textCard in cardText) {
                // because text isn't a game object
                textCard.gameObject.SetActive(true);
            }
        }

        //cardText[0].text = "اهلا";

        // loops to assign the cards for both the player and the opponent
        // i don't check if the random number has been done before because i remove the sprite when chosen just like a real game
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

        // changing the sprites and texts of the cards
        // you access the text with just .text as it's already a text component so you don't need to use GetComponet<Text>()

        for (int i = 0, j = 0; i < 14; i++, j++) {
            if (j == 7) {
                j = 0;
            }
            if (i < 7) {
                // Image not SpriteRenderer as it's an Image not an object with an image.
                cardList[i].GetComponent<Image>().sprite = playerCards[i];
                cardText[i].text = TranslateMethod(playerCards[i]);
            } else {
                cardList[i].GetComponent<Image>().sprite = opponentCards[j];
                cardText[i].text = TranslateMethod(opponentCards[j]);
            }
        }
    }

    void Update() {

    }
}
