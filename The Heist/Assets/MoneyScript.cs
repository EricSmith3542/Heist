using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Show money player has made on screen
public class MoneyScript : MonoBehaviour
{
    TextMeshProUGUI moneyStolen;
    //ArtifactStolen a;

    // Start is called before the first frame update
    void Start()
    {
        //a = GetComponent<ArtifactStolen>();
        moneyStolen = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyStolen.text = "Money: $" + ArtifactStolen.playerMoney;
    }
}
