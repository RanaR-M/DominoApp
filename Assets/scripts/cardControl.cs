using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cardControl : MonoBehaviour
{
    public List<Sprite> cardSprites = new List<Sprite>();
    public List<Sprite> opponentCards = new List<Sprite>();
    public List<GameObject> playerCards = new List<GameObject>();
    public List<GameObject> testCards = new List<GameObject>();
    public List<Text> cardText = new List<Text>();
    public List<GameObject> bankCards = new List<GameObject>();
    public List<GameObject> playingCards = new List<GameObject>();
    public List<GameObject> rightCards = new List<GameObject>();
    public List<GameObject> leftCards = new List<GameObject>();
    public GameObject objectToCopy;
    public GameObject middleCard;
    public GameObject lastCardInbank;
    public Transform objectParent;
    public Transform objectNewParent;
    public Transform newRow;
    public Transform playingGround;
    public Transform testParent;
    int currentCards;
    int playerCardsCount;
    int opponentCardsCount;
    static Vector3 zeroPositionParent;
    static Vector3 zeroScaleParent;
    static Vector3 zeroPositonPlayingGround;
    static Vector3 opponentCardsZero;
    // show cards 
    // make false in inspector to work with text

    string TranslateMethod(Sprite Card) {
        // used to build the text ex: 1/5 using the name of the sprites 
        // can be used for arabic and english numbers 
        char[] nameArray = Card.name.ToCharArray();
        return nameArray[0] + "/" + nameArray[1];
    }

    char[] GetValue(GameObject playingCard) {
        char[] valueOfCard = TranslateMethod(playingCard.GetComponent<Image>().sprite).ToCharArray();
        char[] valueToReturn = new char[2];
        valueToReturn[0] = valueOfCard[0];
        valueToReturn[1] = valueOfCard[2];
        return valueToReturn;
    }

    int TranslateObjectName(GameObject Card) {
        return int.Parse(Card.name.Split('_')[1]) - 1;
    }

    void SelectCard(GameObject selectedCard) {
        if (selectedCard.transform.parent.name.Equals(testParent.name)) {
            selectedCard.transform.SetParent(playingGround);
            selectedCard.transform.localScale = new Vector3(1, 1, 1);
            playingCards.Add(selectedCard);
            testCards.RemoveAt(TranslateObjectName(selectedCard));
            opponentCards.RemoveAt(TranslateObjectName(selectedCard));
            selectedCard.name = "card" + "_" + playingCards.Count;
            SetPositon(selectedCard);
        }
        else {
            selectedCard.transform.SetParent(playingGround);
            selectedCard.transform.localScale = new Vector3(1, 1, 1);
            playingCards.Add(selectedCard);
            playerCards.RemoveAt(TranslateObjectName(selectedCard));
            selectedCard.name = "card" + "_" + playingCards.Count;
            SetPositon(selectedCard);
        }


    }

    void SetPositon(GameObject card) {
        card.GetComponent<Button>().onClick.RemoveAllListeners();
        int count = playingCards.Count;
        int numberInSides = (int)((count - 1) / 2);
        if (count == 1) {
            card.transform.localPosition = new Vector2(0, 0);
        }
        else {
            // need to edit but only for testing

            if (count < 6) {
                card.transform.localRotation = Quaternion.Euler(0, 0, 90);
                card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                zeroPositonPlayingGround = playingGround.localPosition;
                if (count % 2 == 0) {
                    //left
                    if (numberInSides == 0) {
                        card.transform.localPosition = playingCards[0].transform.localPosition + new Vector3(92, 0, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[1].transform.localPosition + new Vector3(120 * numberInSides, 0, 0);
                    }

                }
                else {
                    if (numberInSides == 1) {
                        card.transform.localPosition = playingCards[0].transform.localPosition - new Vector3(92, 0, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[2].transform.localPosition - new Vector3(120 * (numberInSides - 1), 0, 0);
                    }

                }
            }
            else if (count < 10) {
                playingGround.transform.localPosition = zeroPositonPlayingGround + new Vector3(0, 200, 0);
                if (count % 2 == 0) {
                    if (count == 6) {
                        card.transform.localPosition = playingCards[3].transform.localPosition + new Vector3(30, -90, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(0, -120, 0);
                    }

                }
                else {
                    if (count == 7) {
                        card.transform.localPosition = playingCards[4].transform.localPosition + new Vector3(-30, -90, 0);

                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(0, -120, 0);
                    }

                }
            }
            else if (count < 12) {
                card.transform.localRotation = Quaternion.Euler(0, 0, 90);
                card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                if (count % 2 == 0) {
                    card.transform.localPosition = playingCards[3].transform.localPosition + new Vector3(0, -120 * (numberInSides - 2) - 60, 0);
                }
                else {
                    card.transform.localPosition = playingCards[4].transform.localPosition + new Vector3(0, -120 * (numberInSides - 3) - 60, 0);
                }
            }
            else if (count < 18) {
                if (count % 2 == 0) {
                    if (count == 12) {
                        card.transform.parent.localPosition += new Vector3(0, 50, 0);
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(-90, -30, 0);
                        card.transform.parent.localScale = new Vector3(.9f, .9f, 1);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3 ].transform.localPosition + new Vector3(0, -120, 0);
                    }

                }
                else {
                    if (count == 13) {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(90, -30, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(0, -120, 0);
                    }

                }
            }
            else if (count < 22) {
                card.transform.localRotation = Quaternion.Euler(0, 0, 90);
                if (count % 2 == 0) {
                    if (count == 18) {
                        card.transform.parent.localScale = new Vector3(.8f, .8f, 0);
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(30, -90, 0);

                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(120, 0, 0);
                    }
                }
                else {
                    if (count == 19) {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(-30, -90, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition - new Vector3(120, 0, 0);
                    }
                }
            }
            else if (count < 24) {
                if(count % 2 == 0) {
                    if (count == 22) {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(70, -90, 0);
                    } 
                    //else {
                    //    card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(0, -120, 0);
                    //}
                }
                else {
                    if (count == 23) {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(-70, -90, 0);

                    }
                    //else {
                    //    card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(0, -120, 0);
                    //}
                }
                
            }
            else if (count >= 24) {
                card.transform.localRotation = Quaternion.Euler(0, 0, 90);
                card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                int c = 1;
                if (count % 2 == 0) {
                    if (count == 24) {
                        card.transform.parent.localScale = new Vector3(.7f, .7f, 1);
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(-95, -30, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(-125 * c, 0, 0);
                    }


                }
                else {
                    if (count == 25) {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(95, -30, 0);
                    }
                    else {
                        card.transform.localPosition = playingCards[count - 3].transform.localPosition + new Vector3(125 * c++, 0, 0);
                    }



                }
                if(count == 28) {
                    card.transform.localPosition += new Vector3(3.25f, 0, 0); 
                }
            }

            if ((count - 1) % 2 == 0 && count != 0 && count != 1) {
                //card.transform.parent.localScale -= new Vector3(.1f, .1f, 0);
            }
        }
    }

    DirectionEnum.direction DirectionOfCard(GameObject Card) {
        char[] cardValue = GetValue(Card);
        if (rightCards.Count == 0 && leftCards.Count == 0) {
            return DirectionEnum.direction.middle;
        }
        else if (rightCards.Count == 0 && leftCards.Count > 0) {
            char[] middleCardValue = GetValue(middleCard);
            char[] leftCardValue = GetValue(leftCards[leftCards.Count - 1]);
            bool left = false;
            bool right = false;
            if (cardValue.Contains(middleCardValue[0]) || cardValue.Contains(middleCardValue[1])) {
                right = true;
                // left
            }
            else if (cardValue.Contains(leftCardValue[0]) || cardValue.Contains(leftCardValue[1])) {
                left = true;
                // right
            }
            if (right || left) {
                if (right) {
                    return DirectionEnum.direction.right;
                }
                else if (left) {
                    return DirectionEnum.direction.left;
                }
                else if (left && right) {
                    return DirectionEnum.direction.rightLeft;
                }

            }
            else {
                return DirectionEnum.direction.none;
            }
            // left, middle
        }
        else if (rightCards.Count > 0 && leftCards.Count == 0) {
            char[] middleCardValue = GetValue(middleCard);
            char[] rightCardValue = GetValue(leftCards[rightCards.Count - 1]);
            bool left = false;
            bool right = false;
            if (cardValue.Contains(middleCardValue[0]) || cardValue.Contains(middleCardValue[1])) {
                left = true;
                // left
            }
            else if (cardValue.Contains(rightCardValue[0]) || cardValue.Contains(rightCardValue[1])) {
                right = true;
                // right
            }
            if (right || left) {
                if (right) {
                    return DirectionEnum.direction.right;
                }
                else if (left) {
                    return DirectionEnum.direction.left;
                }
                else if (left && right) {
                    return DirectionEnum.direction.rightLeft;
                }

            }
            else {
                return DirectionEnum.direction.none;
            }
        }


        // right, middle

        // check right/ left if not check middle
        else {
            char[] rightCardValue = GetValue(rightCards[rightCards.Count - 1]);
            char[] leftCardValue = GetValue(leftCards[leftCards.Count - 1]);
            bool left = false;
            bool right = false;
            if (cardValue.Contains(rightCardValue[0]) || cardValue.Contains(rightCardValue[1])) {
                right = true;
                // left
            }
            else if (cardValue.Contains(leftCardValue[0]) || cardValue.Contains(leftCardValue[1])) {
                left = true;
                // right
            }
            if (right || left) {
                if (right) {
                    return DirectionEnum.direction.right;
                }
                else if (left) {
                    return DirectionEnum.direction.left;
                }
                else if (left && right) {
                    return DirectionEnum.direction.rightLeft;
                }

            }
            else {
                return DirectionEnum.direction.none;
            }

            // check left and right
        }

        return DirectionEnum.direction.none;
    }

    GameObject playerCard_1;

    void setOpponentCards() {
        bool rebuild = false;
        if (testCards.Count == 7 && cardSprites.Count == 21) {
            int opponentCount = 7;
            for (int i = 0; i < opponentCount; i++) {
                opponentCardsCount = testCards.Count;
                int randomNumber = Random.Range(1, cardSprites.Count);
                opponentCards.Add(cardSprites[randomNumber]);
                cardSprites.RemoveAt(randomNumber);
                testCards[i].GetComponent<Image>().sprite = opponentCards[i];
                testCards[i].name = "test_" + (i + 1);
                var temp = i;
                testCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(testCards[temp]));
                //
            }
        }
        else {
            for (int i = 0; i < testCards.Count; i++) {
                if (!testCards[i].name.Equals("test_" + (i + 1))) {
                    rebuild = true;
                    break;
                }
            }
            if (rebuild) {
                for (int i = 0; i < testCards.Count; i++) {
                    opponentCardsCount = testCards.Count;
                    testCards[i].transform.localPosition = new Vector3(90 * i, 0, 0);
                    testCards[i].name = "test_" + (i + 1);
                    testCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    var temp = i;
                    testCards[i].GetComponent<Button>().onClick.AddListener(() => SelectCard(testCards[temp]));
                }
            }
        }

    }

    void setPlayerCards() {
        bool rebuild = false;
        // IMPORTANT:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // prevent it from constantly updating like the bank

        if (playerCards.Count == 7 && cardSprites.Count == 28) {
            // need to check if they are the first 7 that i added manually
            for (int i = 0; i < playerCards.Count; i++) {
                playerCardsCount = playerCards.Count;
                int randomNumber = Random.Range(1, cardSprites.Count);
                playerCards[i].GetComponent<Image>().sprite = cardSprites[randomNumber];
                cardText[i].text = TranslateMethod(playerCards[i].GetComponent<Image>().sprite);
                cardSprites.RemoveAt(randomNumber);
                zeroPositionParent = objectNewParent.localPosition;
                zeroScaleParent = objectNewParent.localScale;
                var temp = i;
                playerCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
            }
        }
        else {
            if (playerCards.Count < 10) {
                playerCardsCount = playerCards.Count;
                for (int i = 0; i < playerCards.Count; i++) {
                    GameObject bankCard = bankCards[i];
                    if (!bankCard.name.Equals("card_" + (i + 1))) {
                        rebuild = true;
                        break;
                    }
                }
                if (rebuild) {
                    int count = 1;
                    for (int i = 0; i < playerCards.Count; i++) {
                        playerCards[i].transform.SetParent(objectNewParent);
                        if (playerCards[i].GetComponent<RectTransform>().pivot != new Vector2(.5f, .5f)) {
                            playerCards[i].GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                        }
                        playerCards[i].transform.localPosition = new Vector3(90 * i, 0, 0);
                        playerCards[i].name = "card_" + (i + 1);
                        if (i > 6) {
                            objectNewParent.localScale = zeroScaleParent - new Vector3(.1f * count, .1f * count++, 0);
                            if (playerCards.Count > 7) {
                                // -25
                                objectNewParent.localPosition = zeroPositionParent - new Vector3(15, 0, 0);
                            }
                        }
                        if (playerCards[i].transform.localScale != playerCards[0].transform.localScale) {
                            playerCards[i].transform.localScale = playerCards[0].transform.localScale;
                        }

                        var temp = i;
                        if (playerCards[i].GetComponent<Button>() == null) {
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }



                    }
                }

            }
            else {
                playerCardsCount = playerCards.Count;
                for (int i = 0; i < playerCards.Count; i++) {
                    GameObject bankCard = bankCards[i];
                    if (!bankCard.name.Equals("card_" + (i + 1))) {
                        rebuild = true;
                        break;
                    }
                }
                if (rebuild) {
                    int count = 1;
                    for (int i = 0; i < 9; i++) {
                        playerCards[i].transform.SetParent(objectNewParent);
                        if (playerCards[i].GetComponent<RectTransform>().pivot != new Vector2(.5f, .5f)) {
                            playerCards[i].GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                        }
                        playerCards[i].transform.localPosition = new Vector3(90 * i, 0, 0);
                        playerCards[i].name = "card_" + (i + 1);
                        if (i > 6) {
                            objectNewParent.localScale = zeroScaleParent - new Vector3(.1f * count, .1f * count++, 0);
                            if (playerCards.Count > 7) {
                                // -25
                                objectNewParent.localPosition = zeroPositionParent - new Vector3(15, 0, 0);
                            }
                        }
                        if (playerCards[i].transform.localScale != playerCards[0].transform.localScale) {
                            playerCards[i].transform.localScale = playerCards[0].transform.localScale;
                        }

                        var temp = i;
                        if (playerCards[i].GetComponent<Button>() == null) {
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }

                    }
                    for (int i = 9; i < playerCards.Count; i++) {
                        playerCards[i].transform.SetParent(newRow);
                        playerCards[i].name = "card_" + (i + 1);
                        if (playerCards[i].GetComponent<RectTransform>().pivot != new Vector2(.5f, .5f)) {
                            playerCards[i].GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                        }
                        if (playerCards.Count > 10) {
                            if (playerCards[i].transform.localScale != playerCards[8].transform.localScale) {
                                playerCards[i].transform.localScale = playerCards[8].transform.localScale;
                            }
                        }
                        else {
                            playerCards[i].transform.parent.localPosition += new Vector3(45, 0, 0);
                            playerCards[i].transform.parent.localScale = objectNewParent.transform.localScale;
                        }
                        playerCards[i].transform.localPosition = new Vector3(90 * (i - 9), 0, 0);
                        var temp = i;
                        if (playerCards[i].GetComponent<Button>() == null) {
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => SelectCard(playerCards[temp]));
                        }
                    }
                }

            }
        }
    }



    // need to edit test on chosing a card then adding a card

    void BankCardsOnclick(GameObject CardClicked) {
        currentCards = cardSprites.Count;
        lastCardInbank = bankCards[bankCards.Count - 1];
        int objectName = TranslateObjectName(CardClicked);
        Debug.Log(TranslateMethod(cardSprites[objectName]));
        playerCards.Add(CardClicked);
        CardClicked.GetComponent<Image>().sprite = cardSprites[objectName];
        setPlayerCards();
        cardSprites.RemoveAt(objectName);
        bankCards.RemoveAt(objectName);

        //
    }


    void BankCardsShow() {
        int xZero = -220;
        int yZero = 230;
        bool rebuild = false;
        for (int i = 0; i < bankCards.Count; i++) {
            GameObject bankCard = bankCards[i];
            if (!bankCard.name.Equals("BankCard_" + (i + 1))) {
                rebuild = true;
                break;
            }
        }
        if (rebuild) {

            for (int i = 0, x = xZero, y = yZero; i < bankCards.Count; i++, x -= xZero / 2) {
                GameObject bankCard = bankCards[i];
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
            }
            return;
        }


        else if (bankCards.Count == 0) {
            cardSprites.ShuffleMethod();
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
                bankCards.Add(bankCard);
                bankCard.AddComponent<Button>().onClick.AddListener(() => BankCardsOnclick(bankCard));
                bankCard.GetComponent<Button>().onClick.AddListener(() => objectParent.gameObject.SetActive(false));


            }

        }



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

        setPlayerCards();
        setOpponentCards();


        //int opponentCount = 7;
        //for (int i = 0; i < opponentCount; i++) {
        //    int randomNumber = Random.Range(1, cardSprites.Count);
        //    opponentCards.Add(cardSprites[randomNumber]);
        //    cardSprites.RemoveAt(randomNumber);
        //}
        //for (int i = 0; i < 7; i++) {
        //    testCards[i].GetComponent<Image>().sprite = opponentCards[i];
        //    testCards[i].name = "test_" + (i + 1);
        //    var temp = i;
        //    testCards[i].AddComponent<Button>().onClick.AddListener(() => SelectCard(testCards[temp]));
        //}



        BankCardsShow();


    }


    void Update() {
        int newCards = cardSprites.Count;
        int newPlayerCards = playerCards.Count;
        int newOpponentCards = testCards.Count;

        //Debug.Log("new:" + newCards + " old:" + currentCards);
        if (newCards < 14) {
            if (currentCards != newCards) {
                BankCardsShow();

            }
        }
        if (newPlayerCards != playerCardsCount) {
            setPlayerCards();
        }
        if (newOpponentCards != opponentCardsCount) {
            setOpponentCards();
        }

        // old count new couny
    }



}