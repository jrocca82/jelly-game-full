using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using NFTResponse;

public class JellyContract : MonoBehaviour
{

    public GameObject jellySprite;
    public TextMeshProUGUI errorMessage;

    public Sprite[] spriteArray;

    string contract = "0xB799B360F537Fe5e397Fa1bBcE1529AD6031544d";

    string chain = "ethereum";

    string network = "goerli";

    private string tokenId;

    public void GetUserInput(string userInput)
    {
        tokenId = userInput;
    }

    public async void FetchJelly()
    {
        string userAddress = PlayerPrefs.GetString("Account");
        string ownerOf =
            await ERC721.OwnerOf(chain, network, contract, tokenId);

        if (ownerOf.ToLower() == userAddress.ToLower())
        {
            string uri = await ERC721.URI(chain, network, contract, tokenId);
            print(uri);
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            await webRequest.SendWebRequest();
           RootGetNFT data =
                    JsonConvert.DeserializeObject<RootGetNFT>(
                        System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data)); 

            print("SPEED " + data.attributes[0].value);
            print("HEIGHT " + data.attributes[1].value);

            string imageUri = data.image;
            string pngString = (imageUri.Substring(imageUri.Length - 5)).Substring(0, 1);
            int spriteIndex = Convert.ToInt32(pngString);
            PlayerController.playerInstance.speed = 3.0f * data.attributes[0].value;
            PlayerController.playerInstance.jumpForce = 4.0f * data.attributes[1].value;
            print("Speed " + PlayerController.playerInstance.speed);
            print("Jump Height: " + PlayerController.playerInstance.jumpForce);
            SpriteRenderer spriteRenderer = jellySprite.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteArray[spriteIndex - 1];
            TextManager.instance.ExitModal();
        }
        else
        {
            errorMessage.text = "Error: this is not your token";
        }
    }
}
