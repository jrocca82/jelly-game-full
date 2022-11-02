using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public static TextManager instance;

    int score;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        score = 0;
        textMesh.text = "Coins: " + score;
    }

    void Update()
    {
    }

    public void IncreaseScore()
    {
        score++;
        instance.textMesh.text = "Coins: " + score;
    }
}
