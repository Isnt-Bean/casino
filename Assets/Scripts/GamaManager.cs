using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    public Sprite[] cards;
    private int[] cardValue = new int[52];
    private int playerValue;
    private int dealerValue;
    private int randPlayerCard;
    private int randDealerCard;
    public int bet;
    public int money = 1000;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI dealerText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI betText;
    private bool startable = true;
    
    void Start()
    {
        betText.text = 0.ToString();
    }

    void Update()
    {
        moneyText.text = money.ToString();
        betText.text = bet.ToString();
        playerText.text = playerValue.ToString();
        dealerText.text = dealerValue.ToString();
        
    }
    public void Bet(int amount)
    {
        if (amount > 0 && amount <= money)
        {
            bet = amount;
            startable = true;
        }
    }
    public void StartGame()
    {
        if (startable)
        {
            playerValue = 0;
            dealerValue = 0;
            
            for (int i = 0; i < 2; i++)
            {
                randPlayerCard = Random.Range(1, 53);
                randPlayerCard %= 13;
                if (randPlayerCard > 10)
                {
                    randPlayerCard = 10;
                }
    
                if (randPlayerCard == 0)
                {
                    randPlayerCard = 11;
                }
                //print("Player: " + randPlayerCard);
                
                
                randDealerCard = Random.Range(1, 53);
                randDealerCard %= 13;
                if (randDealerCard > 10)
                {
                    randDealerCard = 10;
                }
    
                if (randDealerCard == 0)
                {
                    randDealerCard = 11;
                }
                
                CalculateValue( randPlayerCard, randDealerCard);
                //print("Dealer: " + randDealerCard);
            }
        }
    }

    void CalculateValue(int amount, int amount2)
    {
        playerValue += amount;
        //print("Player total: " + playerValue);
        playerText.text = playerValue.ToString();
        
        dealerValue += amount2;
        //print("Dealer total: " + dealerValue);
        dealerText.text = dealerValue.ToString();
    }

    public void Hit()
    {
        //give the player or dealer a card
        randPlayerCard = Random.Range(1, 53);
        randPlayerCard %= 13;
        if (randPlayerCard > 10)
        {
            randPlayerCard = 10;
        }

        if (randPlayerCard == 0)
        {
            randPlayerCard = 11;
        }
        CalculateValue(randPlayerCard, 0);
    }

    public void Stay()
    {
        //end action for the player or dealer
        startable = false;
        DealerAction();
    }


    void DealerAction()
    {
        if (dealerValue < 17)
        {
            randDealerCard = Random.Range(1, 53);
            randDealerCard %= 13;
            if (randDealerCard > 10)
            {
                randDealerCard = 10;
    
                if (randDealerCard == 0)
                {
                    randDealerCard = 11;
                }
            }
        }
        CalculateValue(0, randDealerCard);
        WhoWins();
    }

    void WhoWins()
    {
        //print("Who wins!");
        if (playerValue > dealerValue && playerValue <= 21)
        {
            money += bet * 2;
            print("Player wins!");
        }

        if (playerValue < dealerValue && dealerValue <= 21)
        {
            money -= bet;
            print("Dealer wins!");
        }

        if (playerValue == dealerValue && dealerValue <= 21 && playerValue > 21)
        {
            money += bet;
            print("Tie!");
        }

        StartCoroutine(EndHand());
    }

    IEnumerator EndHand()
    {
        yield return new WaitForSeconds(1.5f);
        playerValue = 0;
        dealerValue = 0;
        startable = true;
    }
}

