using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public void GoToSeedEntering()
    {
        SceneManager.LoadScene("SeedEnteringScene");
    } 

    public void GoToSeedGenerating()
    {
        SceneManager.LoadScene("GeneratingRandomSeedScene");
    }

    
}
