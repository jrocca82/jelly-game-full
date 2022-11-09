using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class JellyContract : MonoBehaviour
{
    public class Response
    {
        public string image;
    }

    public GameObject jellySprite;

    public Sprite[] spriteArray;

    string contract = "0xB799B360F537Fe5e397Fa1bBcE1529AD6031544d";

    string chain = "ethereum";

    string network = "goerli";

    string tokenId = "3";

    public async void FetchJelly()
    {
        string userAddress = PlayerPrefs.GetString("Account");
        string ownerOf =
            await ERC721.OwnerOf(chain, network, contract, tokenId);

        if (ownerOf.ToLower() == userAddress.ToLower())
        {
            string uri = await ERC721.URI(chain, network, contract, tokenId);
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            await webRequest.SendWebRequest();
            Response data =
                JsonUtility
                    .FromJson<Response>(System
                        .Text
                        .Encoding
                        .UTF8
                        .GetString(webRequest.downloadHandler.data));
            string imageUri = data.image;
            print (imageUri);
            string pngString = (imageUri.Substring(imageUri.Length - 5)).Substring(0, 1);
            print (pngString);
            int spriteIndex = Convert.ToInt32(pngString);
            print (spriteIndex);
            SpriteRenderer spriteRenderer = jellySprite.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteArray[spriteIndex - 1];
            TextManager.instance.ExitModal();
        }
        else
        {
            print("Error: this is not your token");
        }
    }
}
