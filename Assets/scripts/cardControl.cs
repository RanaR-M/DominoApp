
using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject directionControlObject;
    public Transform objectParent;
    public Transform objectNewParent;
    public Transform newRow;
    public Transform playingGround;
    public Transform testParent;
    public Button bankButton;
    int currentCards;
    int playerCardsCount;
    int opponentCardsCount;
    public int usedCardsCount;
    static Vector3 zeroPositionParent;
    static Vector3 zeroScaleParent;
    static Vector3 zeroPositonPlayingGround;
    DirectionEnum.direction directionChosen;

    string TranslateMethod(Sprite Card) {
        // used to build the text ex: 1/5 using the name of the sprites 
        // can be used for arabic and english numbers 
        char[] nameArray = Card.name.ToCharArray();
        return nameArray[0] + "/" + nameArray[1];
    }

    int TranslateObjectName(GameObject Card) {
        return int.Parse(Card.name.Split('_')[1]) - 1;
    }


    int[] GetValue(GameObject playingCard, char printValue) {
        char[] valueOfCard = TranslateMethod(playingCard.GetComponent<Image>().sprite).ToCharArray();
        int[] valueToReturn;
        bool switchValue = false;
        // tail of right side is different that the left one imagine it 


        if (Quaternion.Angle(playingCard.transform.localRotation, (Quaternion.Euler(0, 0, 180) * Quaternion.Euler(0, 0, 90))) == 0) {
            // for bottom cards that isnt no 5,9 or anybigger than 11
            if (leftCards.Contains(playingCard) || rightCards.Contains(playingCard)) {

                switchValue = true;
                if ((((leftCards.Count == 5 || leftCards.Count == 23) || (leftCards.Count > 11 && leftCards.Count < 17)) && leftCards.Contains(playingCard))
                    || (((rightCards.Count == 5 || rightCards.Count == 23) || (rightCards.Count > 11 && rightCards.Count < 17)) && rightCards.Contains(playingCard))) {

                    switchValue = false;
                }
            }

        }
        else if (Quaternion.Angle(playingCard.transform.localRotation, Quaternion.Euler(0, 0, 180)) == 0 && rightCards.Contains(playingCard)) {
            if (rightCards.Count < 17) {
                switchValue = true;
            }
            // a rightBottom straight card

        }
        else if (Quaternion.Angle(playingCard.transform.localRotation, Quaternion.Euler(0, 0, 0)) == 0 && leftCards.Contains(playingCard)) {

            if (leftCards.Count < 17) {
                switchValue = true;
            }
            // a lefttop straight card

        }
        else if (Quaternion.Angle(playingCard.transform.localRotation, Quaternion.Euler(0, 0, 180)) == 0 && leftCards.Contains(playingCard) && leftCards.Count > 16) {

            switchValue = true;
        }
        else if (Quaternion.Angle(playingCard.transform.localRotation, Quaternion.Euler(0, 0, 0)) == 0 && rightCards.Contains(playingCard) && rightCards.Count > 16) {

            switchValue = true;
        }
        else if ((((leftCards.Count == 5 || leftCards.Count == 23) || (leftCards.Count > 11 && leftCards.Count < 17)) && leftCards.Contains(playingCard))
           || (((rightCards.Count == 5 || rightCards.Count == 23) || (rightCards.Count > 11 && rightCards.Count < 17)) && rightCards.Contains(playingCard))) {

            switchValue = true;
        }
        else {
            switchValue = false;
        }

        if (switchValue) {
            valueToReturn = new int[] { (int)System.Char.GetNumericValue(valueOfCard[2])
                , (int)System.Char.GetNumericValue(valueOfCard[0]) };
        }
        else {
            valueToReturn = new int[] { (int)System.Char.GetNumericValue(valueOfCard[0])
                , (int)System.Char.GetNumericValue(valueOfCard[2]) };
        }
        if (printValue.Equals('y')) {
            Debug.Log(valueToReturn[0] + " " + valueToReturn[1]);
        }

        return valueToReturn;
    }

    IEnumerator waitUntilPressed(GameObject Card) {
        DirectionControl controlObject = directionControlObject.gameObject.GetComponent<DirectionControl>();

        int[] cardValue = GetValue(Card, 'y');
        int[] rightCardValue;
        int[] leftCardValue;
        int[] middleCardValue;

        bool leftTop = false;
        bool leftBottom = false;
        bool rightTop = false;
        bool rightBottom = false;
        bool check = true;
        int count = playingCards.Count;
        if (middleCard == null) {
            if (cardValue[0].Equals(cardValue[1])) {
                middleCard = Card;
                directionChosen = DirectionEnum.direction.middle;
                setActualPosition(Card, directionChosen);
                check = false;
            }
            else {
                directionChosen = DirectionEnum.direction.none;
                setActualPosition(Card, directionChosen);
                check = false;
            }
        }
        else if (rightCards.Count == 0 && leftCards.Count == 0) {
            middleCardValue = GetValue(middleCard, 'y');
            if (middleCardValue[0].Equals(cardValue[0])) {
                leftTop = true;
                rightTop = true;
            }
            else if (middleCardValue[0].Equals(cardValue[1])) {
                leftBottom = true;
                rightBottom = true;
            }
        }
        else if (rightCards.Count == 0 && leftCards.Count > 0) {
            middleCardValue = GetValue(middleCard, 'y');
            leftCardValue = GetValue(leftCards[leftCards.Count - 1],'y');


            if (middleCardValue[0].Equals(cardValue[0])) {
                rightTop = true;
            }
            else if (middleCardValue[0].Equals(cardValue[1])) {
                rightBottom = true;
            }
            if (leftCardValue[0].Equals(cardValue[0])) {
                leftTop = true;
            }
            else if (leftCardValue[0].Equals(cardValue[1])) {
                leftBottom = true;
            }
            // left, middle
        }
        else if (rightCards.Count > 0 && leftCards.Count == 0) {
            middleCardValue = GetValue(middleCard, 'y');
            rightCardValue = GetValue(rightCards[rightCards.Count - 1], 'y');

            if (middleCardValue[0].Equals(cardValue[0])) {
                leftTop = true;
            }
            else if (middleCardValue[0].Equals(cardValue[1])) {
                leftBottom = true;
            }
            if (rightCardValue[1].Equals(cardValue[0])) {
                rightTop = true;
            }
            else if (rightCardValue[1].Equals(cardValue[1])) {
                rightBottom = true;
            }
        }  // right, middle
        else {
            rightCardValue = GetValue(rightCards[rightCards.Count - 1], 'y');
            leftCardValue = GetValue(leftCards[leftCards.Count - 1], 'y');
            if (rightCardValue[1].Equals(cardValue[0])) {
                rightTop = true;
            }
            else if (rightCardValue[1].Equals(cardValue[1])) {
                rightBottom = true;
            }
            if (leftCardValue[0].Equals(cardValue[0])) {
                leftTop = true;
            }
            else if (leftCardValue[0].Equals(cardValue[1])) {
                leftBottom = true;
            }

        }

        if (check) {
            if (rightBottom && leftBottom) {
                controlObject.QuitMenuDisplay();
                yield return new WaitUntil(() => controlObject.buttonPressed);
                if (controlObject.direction == 1) {
                    directionChosen = DirectionEnum.direction.rightBottom;
                }
                else {
                    directionChosen = DirectionEnum.direction.leftBottom;
                }

            }
            else if (rightTop && leftTop) {
                controlObject.QuitMenuDisplay();
                yield return new WaitUntil(() => controlObject.buttonPressed);
                if (controlObject.direction == 1) {
                    directionChosen = DirectionEnum.direction.rightTop;
                }
                else {
                    directionChosen = DirectionEnum.direction.leftTop;
                }
            }
            else if (rightTop && leftBottom) {
                controlObject.QuitMenuDisplay();
                yield return new WaitUntil(() => controlObject.buttonPressed);
                if (controlObject.direction == 1) {
                    directionChosen = DirectionEnum.direction.rightTop;
                }
                else {
                    directionChosen = DirectionEnum.direction.leftBottom;
                }

            }
            else if (rightBottom && leftTop) {
                controlObject.QuitMenuDisplay();
                yield return new WaitUntil(() => controlObject.buttonPressed);
                if (controlObject.direction == 1) {
                    directionChosen = DirectionEnum.direction.rightBottom;
                }
                else {
                    directionChosen = DirectionEnum.direction.leftTop;
                }
            }
            else if (rightTop) {
                directionChosen = DirectionEnum.direction.rightTop;
            }
            else if (rightBottom) {
                directionChosen = DirectionEnum.direction.rightBottom;
            }
            else if (leftTop) {
                directionChosen = DirectionEnum.direction.leftTop;
            }
            else if (leftBottom) {
                directionChosen = DirectionEnum.direction.leftBottom;
            }
            else {
                directionChosen = DirectionEnum.direction.none;
            }

            if (directionChosen != DirectionEnum.direction.middle) {
                setActualPosition(Card, directionChosen);
            }
            controlObject.buttonPressed = false;
        }


    }

    // call waitUntilPressed first other than setActuallPositon
    void setActualPosition(GameObject Card, DirectionEnum.direction directionToPut) {
        Card.GetComponent<Button>().onClick.RemoveAllListeners();
        DirectionEnum.direction placeOfCardVar = directionToPut;
        int name = TranslateObjectName(Card);
        if (placeOfCardVar != DirectionEnum.direction.none) {
            if (Card.transform.parent.name.Equals(testParent.name)) {
                testCards.RemoveAt(name);
                opponentCards.RemoveAt(name);
            }
            else {
                playerCards.RemoveAt(name);
            }
            Card.transform.SetParent(playingGround);
            Card.transform.localScale = new Vector3(1, 1, 1);

            if (placeOfCardVar == DirectionEnum.direction.middle) {
                Debug.Log("middle");
            }
            else if (placeOfCardVar == DirectionEnum.direction.rightTop) {
                Debug.Log("rightTop");
                if (rightCards.Count == 4 || (rightCards.Count > 10 && rightCards.Count < 17) || (rightCards.Count > 18 && rightCards.Count < 25)) {
                    Card.transform.localRotation = Quaternion.Euler(0, 0, 180);

                }
            }
            else if (placeOfCardVar == DirectionEnum.direction.leftTop) {
                Debug.Log("leftTop");
                if (leftCards.Count < 2 || (leftCards.Count > 7 && leftCards.Count < 10) || (leftCards.Count > 15)) {
                    Card.transform.localRotation = Quaternion.Euler(0, 0, 180);

                }


            }
            else if (placeOfCardVar == DirectionEnum.direction.rightBottom) {
                Debug.Log("rightBottom");
                if ((rightCards.Count != 4 && rightCards.Count < 11) || (rightCards.Count > 16 && rightCards.Count < 19) || rightCards.Count > 24) {
                    Card.transform.localRotation = Quaternion.Euler(0, 0, 180);

                }


            }
            else if (placeOfCardVar == DirectionEnum.direction.leftBottom) {
                Debug.Log("leftBottom");
                if ((leftCards.Count < 8 && leftCards.Count > 1) || (leftCards.Count >= 10 && leftCards.Count < 16) || leftCards.Count == 22) {
                    Card.transform.localRotation = Quaternion.Euler(0, 0, 180);
                }
            }

            if (placeOfCardVar == DirectionEnum.direction.rightTop
                || placeOfCardVar == DirectionEnum.direction.rightBottom) {
                rightCards.Add(Card);
                Card.name = "rightCard_" + rightCards.Count;
                setCard(Card, "right");
            }
            else if (placeOfCardVar == DirectionEnum.direction.leftTop || placeOfCardVar == DirectionEnum.direction.leftBottom) {
                leftCards.Add(Card);
                Card.name = "leftCard_" + leftCards.Count;
                setCard(Card, "left");
            }
            else if (placeOfCardVar == DirectionEnum.direction.middle) {
                Card.transform.localPosition = new Vector2(0, 0);
                Card.name = "middleCard";
                usedCardsCount++;
                zeroPositonPlayingGround = playingGround.localPosition;
            }
        }
        else {
            if (placeOfCardVar == DirectionEnum.direction.none) {
                Debug.Log("Can't place card");
            }
        }

    }

    void setCard(GameObject Card, string direction) {
        int count;
        GameObject previousCard = null;
        int xSign;
        if (direction.Equals("right")) {
            count = rightCards.Count;
            if (count > 1) {
                previousCard = rightCards[count - 2];
            }

            xSign = 1;

        }
        else {
            count = leftCards.Count;
            if (count > 1) {
                previousCard = leftCards[count - 2];
            }
            xSign = -1;
        }

        if (count < 3) {
            usedCardsCount++;
            Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
            Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
            if (count == 1) {
                Card.transform.localPosition = new Vector3(92 * xSign, 0, 0);
            }
            else {
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(120 * xSign, 0, 0);
            }
        }
        else if (count < 5) {
            usedCardsCount++;
            playingGround.transform.localPosition = zeroPositonPlayingGround + new Vector3(0, 200, 0);
            if (count == 3) {
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(30 * xSign, -90, 0);
            }
            else {
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(0, -120, 0);
            }

        }
        else if (count < 6) {
            usedCardsCount++;
            Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
            Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
            Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(30 * (xSign * -1), -90, 0);
        }
        else if (count < 9) {
            usedCardsCount++;
            if (count == 6) {
                Card.transform.parent.localPosition += new Vector3(0, 25, 0);
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(90 * (xSign * -1), -30, 0);
                Card.transform.parent.localScale -= new Vector3(.05f, .05f, 0);
            }
            else {
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(0, -120, 0);
            }
        }
        else if (count < 11) {
            Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
            usedCardsCount++;
            if (count == 9) {
                Card.transform.parent.localScale -= new Vector3(.05f, .05f, 0);
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(30 * xSign, -90, 0);

            }
            else {
                Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(120 * xSign, 0, 0);
            }

        }
        else if (count < 12) {
            usedCardsCount++;
            Card.transform.parent.localScale = new Vector3(.8f, .8f, 1);
            Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(70 * xSign, -90, 0);

        }
        else if (usedCardsCount < 28) {
            usedCardsCount++;
            if (rightCards.Count > 16 || leftCards.Count > 16) {
                Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                int index;
                float xComp;
                float yComp;
                List<List<GameObject>> listOfArrays = new List<List<GameObject>>
                {
                    rightCards,
                    leftCards
                };
                if (rightCards.Count > 16) {
                    index = 0;
                }
                else {
                    index = 1;
                }

                if (count == 17) {
                    yComp = listOfArrays[index][10].transform.localPosition.y;
                    xComp = previousCard.transform.localPosition.x + 95 * (xSign * -1);
                    Card.transform.localPosition = new Vector3(xComp, yComp);
                }
                else if (count < 20) {
                    Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
                    Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                    if (count == 18) {
                        yComp = listOfArrays[index][9].transform.localPosition.y;
                        xComp = previousCard.transform.localPosition.x + 70 * xSign;
                        Card.transform.localPosition = new Vector3(xComp, yComp);
                    }
                    else {
                        Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(120 * xSign, 0, 0);
                    }

                }
                else if (count < 23) {
                    if (count == 20) {
                        yComp = listOfArrays[index][7].transform.localPosition.y;
                        xComp = previousCard.transform.localPosition.x + 30 * xSign;
                        Card.transform.localPosition = new Vector3(xComp, yComp);
                    }
                    else {
                        Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(0, 120, 0);
                    }

                }
                else if (count < 24) {
                    Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
                    Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                    yComp = listOfArrays[index][4].transform.localPosition.y;
                    xComp = previousCard.transform.localPosition.x + 90 * (xSign * -1);
                    Card.transform.localPosition = new Vector3(xComp, yComp);
                }
                else if (count < 26) {
                    if (count == 24) {
                        yComp = listOfArrays[index][3].transform.localPosition.y;
                        xComp = previousCard.transform.localPosition.x + 30 * (xSign * -1);
                        Card.transform.localPosition = new Vector3(xComp, yComp);
                    }
                    else {
                        Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(0, 120, 0);
                    }
                }
                else if (count < 28) {
                    Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
                    Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                    if (count == 26) {
                        yComp = listOfArrays[index][1].transform.localPosition.y;
                        xComp = previousCard.transform.localPosition.x + 30 * xSign;
                        Card.transform.localPosition = new Vector3(xComp, yComp);
                    }
                    else {
                        Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(120 * xSign, 0, 0);
                    }
                }
            }
            else {
                Card.transform.localRotation = Card.transform.localRotation * Quaternion.Euler(0, 0, 90);
                Card.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                if (count == 12) {

                    Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(95 * (xSign * -1), -30, 0);
                }
                else {
                    Card.transform.localPosition = previousCard.transform.localPosition + new Vector3(125 * (xSign * -1), 0, 0);
                }
                if (usedCardsCount == 28) {
                    Card.transform.localPosition += new Vector3(3.25f, 0, 0);
                }
            }

        }
    }

    void setOpponentCards() {
        bool rebuild = false;
        if (testCards.Count == 7 && cardSprites.Count == 21) {
            int opponentCount = 7;
            for (int i = 0; i < opponentCount; i++) {
                opponentCardsCount = testCards.Count;
                int randomNumber = UnityEngine.Random.Range(1, cardSprites.Count);
                opponentCards.Add(cardSprites[randomNumber]);
                cardSprites.RemoveAt(randomNumber);
                testCards[i].GetComponent<Image>().sprite = opponentCards[i];
                testCards[i].name = "test_" + (i + 1);
                var temp = i;
                testCards[i].AddComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(testCards[temp])));

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
                    testCards[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(testCards[temp])));
                }
            }
        }

    }

    void setPlayerCards() {
        bool rebuild = false;

        if (playerCards.Count == 7 && cardSprites.Count == 28) {
            for (int i = 0; i < playerCards.Count; i++) {
                playerCardsCount = playerCards.Count;
                int randomNumber = UnityEngine.Random.Range(1, cardSprites.Count);
                playerCards[i].GetComponent<Image>().sprite = cardSprites[randomNumber];
                cardText[i].text = TranslateMethod(playerCards[i].GetComponent<Image>().sprite);
                cardSprites.RemoveAt(randomNumber);
                zeroPositionParent = objectNewParent.localPosition;
                zeroScaleParent = objectNewParent.localScale;
                var temp = i;
                playerCards[i].AddComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
            }
        }
        else {
            if (playerCards.Count < 10) {
                playerCardsCount = playerCards.Count;
                for (int i = 0; i < playerCards.Count; i++) {
                    GameObject playerCard = playerCards[i];
                    if (!playerCard.name.Equals("card_" + (i + 1))) {
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
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
                        }



                    }
                }

            }
            else {
                playerCardsCount = playerCards.Count;
                for (int i = 0; i < playerCards.Count; i++) {
                    GameObject playerCard = playerCards[i];
                    if (!playerCard.name.Equals("card_" + (i + 1))) {
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
                                objectNewParent.localPosition = zeroPositionParent - new Vector3(15, 0, 0);
                            }
                        }
                        if (playerCards[i].transform.localScale != playerCards[0].transform.localScale) {
                            playerCards[i].transform.localScale = playerCards[0].transform.localScale;
                        }

                        var temp = i;
                        if (playerCards[i].GetComponent<Button>() == null) {
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
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
                            playerCards[i].AddComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
                        }
                        else {
                            playerCards[i].GetComponent<Button>().onClick.RemoveAllListeners();
                            playerCards[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitUntilPressed(playerCards[temp])));
                        }
                    }
                }

            }
        }
    }

    void ActivateBankButton() {
        bool activate = true;
        GameObject rightCardToCheck;
        GameObject leftCardToCheck;
        int[] middleCardValue = { 0 };
        int[] leftCardValue = { 0 };
        int[] rightCardValue = { 0 };
        if (middleCard != null) {
            middleCardValue = GetValue(middleCard,'n');
        }
        if (rightCards.Count != 0) {
            rightCardToCheck = rightCards[rightCards.Count - 1];
            rightCardValue = GetValue(rightCardToCheck,'n');
        }
        if (leftCards.Count != 0) {
            leftCardToCheck = leftCards[leftCards.Count - 1];
            leftCardValue = GetValue(leftCardToCheck,'n');
        }

        if (middleCard == null) {
            foreach (GameObject card in playerCards) {
                int[] valueOfCard = GetValue(card,'n');
                if (valueOfCard[0].Equals(valueOfCard[1])) {
                    activate = false;
                    break;
                }
            }
        }
        else if (rightCards.Count == 0 && leftCards.Count == 0 && middleCard != null) {
            foreach (GameObject card in playerCards) {
                int[] valueOfCard = GetValue(card, 'n');
                if (valueOfCard[0].Equals(middleCardValue[0]) || valueOfCard[1].Equals(middleCardValue[0])) {
                    activate = false;
                    break;
                }
            }
        }
        else if (rightCards.Count > 0 && leftCards.Count == 0 && middleCard != null) {
            foreach (GameObject card in playerCards) {
                int[] valueOfCard = GetValue(card, 'n');
                if (valueOfCard[0].Equals(middleCardValue[0]) || valueOfCard[1].Equals(middleCardValue[0])) {
                    activate = false;
                    break;
                }
                else if (valueOfCard[0].Equals(rightCardValue[1]) || valueOfCard[1].Equals(rightCardValue[1])) {
                    activate = false;
                    break;
                }
            }
        }
        else if (rightCards.Count == 0 && leftCards.Count > 0 && middleCard != null) {
            foreach (GameObject card in playerCards) {
                int[] valueOfCard = GetValue(card,'n');
                if (valueOfCard[0].Equals(middleCardValue[0]) || valueOfCard[1].Equals(middleCardValue[0])) {
                    activate = false;
                    break;
                }
                else if (valueOfCard[0].Equals(leftCardValue[0]) || valueOfCard[1].Equals(leftCardValue[0])) {
                    activate = false;
                    break;
                }
            }
        }
        else if (rightCards.Count > 0 && leftCards.Count > 0) {
            foreach (GameObject card in playerCards) {
                int[] valueOfCard = GetValue(card,'n');
                if (valueOfCard[0].Equals(leftCardValue[0]) || valueOfCard[1].Equals(leftCardValue[0])) {
                    activate = false;
                    break;
                }
                else if (valueOfCard[0].Equals(rightCardValue[1]) || valueOfCard[1].Equals(rightCardValue[1])) {
                    activate = false;
                    break;
                }
            }
        }



        if (activate) {
            bankButton.gameObject.SetActive(true);
        }
        else {
            bankButton.gameObject.SetActive(false);
        }
    }



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
        if (cardSprites.Count == 0) {
            bankButton.gameObject.SetActive(false);
            return;
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

        object[] temp = Resources.LoadAll("cards", typeof(Sprite));
        for (int i = 0; i < temp.Length; i++) {
            cardSprites.Add(temp[i] as Sprite);
        }

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

        setPlayerCards();
        setOpponentCards();
        BankCardsShow();


    }


    void Update() {
        int newCards = cardSprites.Count;
        int newPlayerCards = playerCards.Count;
        int newOpponentCards = testCards.Count;
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
        ActivateBankButton();

    }



}