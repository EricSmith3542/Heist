using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextMeshPro : MonoBehaviour
{
    TextMeshProUGUI tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GetComponent<TextMeshProUGUI>();
        tutorialText.text = "Use numbers [1], [2], [3], [4], [5] to swap between your weapons";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("" + 1) || Input.GetKeyDown("" + 2) || Input.GetKeyDown("" + 3) || Input.GetKeyDown("" + 4) || Input.GetKeyDown("" + 5))
        {
            //Debug.Log("Text");
            tutorialText.text = "";
        }
    }
}
