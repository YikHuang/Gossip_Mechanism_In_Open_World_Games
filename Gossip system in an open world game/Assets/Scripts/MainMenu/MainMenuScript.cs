using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string gameScene;
    public static string characterName;
    public TMP_InputField nameInput;
    public GameObject CharacterCreationCanvas;
    public GameObject StartMenuCanvas;

    public void StoreName() {
        characterName = nameInput.text;
        //Debug.Log(characterName);
    }

    public void SwitchScene() {
        SceneManager.LoadScene(gameScene);
    }

    public void EnterCharaterCreationMenu() {
        StartMenuCanvas.SetActive(false);
        CharacterCreationCanvas.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
