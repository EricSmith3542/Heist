using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextMeshPro : MonoBehaviour
{
    TextMeshProUGUI tutorialText;
    bool firstText = false; //Used for the first weapon switch text
    string[] tutText = {"Use numbers [1], [2], [3], [4] to swap between your weapons",
                        "Your mission is to steal as many artifacts as possible",
                        "Watch out for guards and dogs, they will kill you if they spot you",
                        "Killing guards will cost you money",
                        "Cameras are placed throughout the house, staying in their sight too long will notify guards of your location",
                        "Once you have collected all that you want, get back to your escape vehicle",
                        "Good Luck",
                        ""
                        };
    int textIterator = 0;
    public float tutorialTimer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GetComponent<TextMeshProUGUI>();
        tutorialText.text = tutText[textIterator];
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("" + 1) || Input.GetKeyDown("" + 2) || Input.GetKeyDown("" + 3) || Input.GetKeyDown("" + 4) || Input.GetKeyDown("" + 5)) && !firstText)
        {
            firstText = true;
            tutorialText.text = "";
        }

        //Start timer after first text
        if(firstText)
            tutorialTimer -= Time.deltaTime;

        if (tutorialTimer <= 0f && firstText)
        {
            textIterator++;
            if (textIterator < tutText.Length)
            {
                tutorialText.text = tutText[textIterator];
            }
            tutorialTimer = 4f;
        }
    }
}
