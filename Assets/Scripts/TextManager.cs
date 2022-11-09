using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI scoreMesh;

    public bool modalOpen;

    public GameObject modalWindow;

    public TextMeshProUGUI userAddress;

    public TextMeshProUGUI tokenBalance;

    public static TextManager instance;

    public GameObject coinMintButton;

    public GameObject changeJellyButton;

    private static BigInteger weiValue = 1000000000000000000;

    public int score;

    private int jellyBalance;

    void Start()
    {
        modalWindow.SetActive(false);
        // Display wallet address
        string userAddressText = PlayerPrefs.GetString("Account");
        string startAddress = userAddressText.Substring(0, 5);
        string endAddress =
            userAddressText.Substring(userAddressText.Length - 5, 5);
        string shortenedAddress = startAddress + "..." + endAddress;
        userAddress.text = "Connected to: " + shortenedAddress;

        // Set initial NFT balances and hide buttons
        jellyBalance = 0;
        changeJellyButton.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        score = 0;
        scoreMesh.text = "Coins: " + score;
    }

    void Update()
    {
        CheckAllNFTs();
        DisplayERC20Balance();

        if (jellyBalance > 0)
        {
            changeJellyButton.SetActive(true);
        }

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
        BigInteger balanceOf =
            await ERC20.BalanceOf(chain, network, contract, account);

        int formattedBalance = (int)(balanceOf / weiValue);

        int balance = formattedBalance;
        tokenBalance.text = "Gold Token Balance: " + balance.ToString();
    }

    public async void CheckAllNFTs()
    {
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0xB799B360F537Fe5e397Fa1bBcE1529AD6031544d";
        string account = PlayerPrefs.GetString("Account");
        BigInteger balanceOf =
            await ERC721.BalanceOf(chain, network, contract, account);

        jellyBalance = (int) balanceOf;
    }

    public void OpenMarketplace()
    {
        Application.OpenURL("https://jelly-marketplace.vercel.app/");
    }

    public void OpenModal()
    {
        modalOpen = true;
        modalWindow.SetActive(true);
    }

    public void ExitModal()
    {
        modalOpen = false;
        modalWindow.SetActive(false);
    }
}
