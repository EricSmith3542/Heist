using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    TextMeshProUGUI playerDeaths;
    // Start is called before the first frame update
    void Start()
    {
        playerDeaths = GetComponent<TextMeshProUGUI>();
        ArtifactStolen.playerDeaths++;
    }

    // Update is called once per frame
    void Update()
    {
        playerDeaths.text = "Deaths: " + ArtifactStolen.playerDeaths;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Application.Quit();
        }
    }
}
