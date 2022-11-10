using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTResponse
{
    [Serializable]
    public class Attributes
    {
        public string trait_type;
        public float value;
    }

    [Serializable]
    public class RootGetNFT
    {
        public string description;
        public string image;
        public string name;
        public Attributes[] attributes;
    }
}
