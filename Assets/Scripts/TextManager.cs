using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI scoreMesh;

    public TextMeshProUGUI userAddress;

    public TextMeshProUGUI tokenBalance;

    public static TextManager instance;

    public GameObject coinMintButton;

    public int score;

    void Start()
    {
        // Display wallet address
        string userAddressText = PlayerPrefs.GetString("Account");
        string startAddress = userAddressText.Substring(0, 5);
        string endAddress = userAddressText.Substring(userAddressText.Length - 5, 5);
        string shortenedAddress = startAddress + "..." + endAddress;
        userAddress.text = "Connected to: " + shortenedAddress;
        if (instance == null)
        {
            instance = this;
        }
        score = 0;
        scoreMesh.text = "Coins: " + score;
    }

    void Update()
    {
        DisplayERC20Balance();
        if (score < 100)
        {
            coinMintButton.SetActive(false);
        }
        else
        {
            coinMintButton.SetActive(true);
        }
    }

    public void IncreaseScore()
    {
        score++;
        instance.scoreMesh.text = "Coins: " + score;
    }

    public void ResetScore()
    {
        score = score % 100;
        instance.scoreMesh.text = "Coins: " + score;
    }

    public async void DisplayERC20Balance()
    {
        //Display token balances
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0x562240e1228b7905208FAE11d313F03c99371110";
        string account = PlayerPrefs.GetString("Account");
        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);

        BigInteger weiValue = 1000000000000000000;
        int balance = (int)(balanceOf / weiValue);
        tokenBalance.text = "Gold Token Balance: " + balance.ToString();
    }

    public void OpenMarketplace()
    {
        Application.OpenURL("http://unity3d.com/");
    }
}
