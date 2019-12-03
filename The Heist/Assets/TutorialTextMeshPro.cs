using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextMeshPro : MonoBehaviour
{
    TextMeshProUGUI tutorialText;
    bool firstText = false; //Used for the first weapon switch text
    string[] tutText = {"Use numbers [1], [2], [3], [4], [5] to swap between your weapons",
                        "Your mission, should you choose to accept it, and you don't have a choice",
                        "Is to enter the mansion, steal some stuff, and get out",
                        "Watch out for security cameras, guards, and take note of your limited ammo",
                        "Once you have collected all that you want, get back to the... ",
                        "*Checks notes",
                        "Get back to the ESCAPE CUBE!",
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
