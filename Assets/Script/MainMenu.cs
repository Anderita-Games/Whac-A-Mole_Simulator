using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Text HighScore;
    public virtual void Update()
    {
        this.HighScore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public virtual void StartGame()
    {
        Application.LoadLevel("Game");
    }

    public virtual void ExitGame()
    {
        Application.Quit();
    }

}