//
// PongVR
// ******
// 
// Created by Luis Eudave 10/07/21.
// 
// Based on VRPong by Kevin Thomas
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LeftPaddle leftHandPlayer;
    public RightPaddle rightHandPlayer;

    public TextMesh infoText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame update score and since it
        // is an int we need to cast to a string
        infoText.text
            = leftHandPlayer.score.ToString()
            + "                                    "
            + rightHandPlayer.score.ToString();
    }
}
