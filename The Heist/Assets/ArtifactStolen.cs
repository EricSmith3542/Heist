using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArtifactStolen : MonoBehaviour
{
    public static float playerMoney = 0f;
    public static bool artifactStolen = false;


   public string[] artifacts = { "GreekVase", "Vase2",  "Vase3", "Painting1", "Painting2", "QueenVictoria", "DragonStatue",
        "FigurineStatue", "Discobolus", "NefertitiBust", "EgyptianCat", "JapaneseMask", "JuliusCaesar"}; //13 Artifacts

    public float[] artifactValue = {
            18870f, //GreekVase
            6990f, //Vase2
            4765f, //Vase3
            12540f, //Painting1
            9715f, //Painting2
            23345f, //QueenVictoria
            22650f, //DragonStatue
            4000f, //FigurineStatue
            55990f, //Discobolus
            90000f, //NefertitiBust
            12000f, //EgyptianCat
            6095f, //JapaneseMask
            95725f //JuluisCeasar
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public float getPlayerMoney() {
        return playerMoney;
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "GreekVase")
        {
            playerMoney += artifactValue[0];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Vase2")
        {
            playerMoney += artifactValue[1];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Vase3")
        {
            playerMoney += artifactValue[2];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Painting1")
        {
            playerMoney += artifactValue[3];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Painting2")
        {
            playerMoney += artifactValue[4];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "QueenVictoria")
        {
            playerMoney += artifactValue[5];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Dragonstatue")
        {
            playerMoney += artifactValue[6];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "FigurineStatue")
        {
            playerMoney += artifactValue[7];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Discobulus")
        {
            playerMoney += artifactValue[8];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "NefertitiBust")
        {
            playerMoney += artifactValue[9];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "EgyptianCat")
        {
            playerMoney += artifactValue[10];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "JapaneseMask")
        {
            playerMoney += artifactValue[11];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "JuluisCeasar")
        {
            playerMoney += artifactValue[12];
            artifactStolen = true;
            Destroy(other.gameObject);
        }
    }
}
