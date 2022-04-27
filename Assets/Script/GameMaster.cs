using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class GameMaster : MonoBehaviour
{
    public RectTransform Spawn_Area;
    public RectTransform Canvas_Area;
    public GameObject Canvas;
    public float Conversion_Rate;
    public System.Collections.Generic.List<string> Mole_Positions;
    public int Time_Left;
    public bool Time_Is_Out;
    public GameObject Mole_Original;
    public GameObject Mole_Clone;
    public int Array_Input_Count;
    public UnityEngine.UI.Text Title;
    public UnityEngine.UI.Text Score_Current;
    public UnityEngine.UI.Text Score_High;
    public UnityEngine.UI.Text Time_Left_Text;
    public GameObject Button_Restart;
    public GameObject Button_Quit;
    public virtual void Start()
    {

        {
            float _1 = this.Canvas_Area.sizeDelta.y * this.Conversion_Rate;
            Vector2 _2 = this.Spawn_Area.sizeDelta;
            _2.y = _1;
            this.Spawn_Area.sizeDelta = _2;
        }

        {
            float _3 = (this.Canvas_Area.sizeDelta.y / -2) + (this.Spawn_Area.sizeDelta.y / 2);
            Vector3 _4 = this.Spawn_Area.localPosition;
            _4.y = _3;
            this.Spawn_Area.localPosition = _4;
        }
        PlayerPrefs.SetInt("MoleTotal", 0);
        PlayerPrefs.SetInt("Score_Current", 0);
        PlayerPrefs.SetString("Remove", null);
        this.Button_Restart.SetActive(false);
        this.Button_Quit.SetActive(false);
        this.StartCoroutine(this.CountDown());
        this.StartCoroutine(this.Spawn_The_Moles());
    }

    public virtual void Update()
    {
        if ((PlayerPrefs.GetString("Remove") != null) && (PlayerPrefs.GetString("Remove") != "GAMEOVER"))
        {
            this.Mole_Positions.Remove(PlayerPrefs.GetString("Remove"));
            PlayerPrefs.SetString("Remove", null);
        }
        if (PlayerPrefs.GetInt("Score_Current") > PlayerPrefs.GetInt("Score_High"))
        {
            PlayerPrefs.SetInt("Score_High", PlayerPrefs.GetInt("Score_Current"));
        }
        this.Score_Current.text = "Moles Killed: " + PlayerPrefs.GetInt("Score_Current");
        this.Score_High.text = "Most Moles Killed: " + PlayerPrefs.GetInt("Score_High");
        this.Time_Left_Text.text = "Time Left: " + this.Time_Left;
    }

    public virtual IEnumerator Spawn_The_Moles()
    {
        int Row_Total = 3;
        int Column_Total = 7;
        int Row_Current = 1;
        int Column_Current = 1;
        float Spawn_Area_Y = this.Spawn_Area.sizeDelta.y;
        while (this.Time_Is_Out != true)
        {
            int Moles_To_Spawn = Random.Range(1, 4);
            while ((Moles_To_Spawn > 0) && (PlayerPrefs.GetInt("MoleTotal") != (Row_Total * Column_Total)))
            {
                Row_Current = Random.Range(1, Row_Total + 1);
                Column_Current = Random.Range(1, Column_Total + 1);
                while (this.Mole_Positions.Contains((Row_Current + ":") + Column_Current))
                {
                    Row_Current = Random.Range(1, Row_Total + 1);
                    Column_Current = Random.Range(1, Column_Total + 1);
                }
                float Clone_X = ((this.Canvas_Area.sizeDelta.x / Column_Total) * (Column_Current - 0.5f)) - (this.Canvas_Area.sizeDelta.x / 2);
                float Clone_Y = (this.Canvas_Area.sizeDelta.y / -2) + ((Spawn_Area_Y / Row_Total) * ((Row_Total - Row_Current) + 0.5f));
                this.Mole_Clone = UnityEngine.Object.Instantiate(this.Mole_Original);
                this.Mole_Clone.transform.SetParent(this.Canvas.transform);
                this.Mole_Clone.transform.localPosition = new Vector3(Clone_X, Clone_Y, 0);
                this.Mole_Clone.transform.localScale = new Vector3(1, 1, 1);
                this.Mole_Clone.name = (Row_Current + ":") + Column_Current;
                this.Mole_Positions.Add(this.Mole_Clone.name);
                this.Array_Input_Count++;
                PlayerPrefs.SetInt("MoleTotal", PlayerPrefs.GetInt("MoleTotal") + 1);
                Moles_To_Spawn--;
            }
            yield return new WaitForSeconds(2);
        } //Time Intervals for Mole Spawns (Random?)
    }

    public virtual IEnumerator CountDown()
    {
        while (this.Time_Left > 0)
        {
            this.Time_Left--;
            yield return new WaitForSeconds(1);
        }
        this.Time_Is_Out = true;
        PlayerPrefs.SetString("Remove", "GAMEOVER");
        this.Button_Restart.SetActive(true);
        this.Button_Quit.SetActive(true);
        this.Title.text = "Game Over";
    }

    public virtual void RestartGame()
    {
        Application.LoadLevel("Game");
    }

    public virtual void ExitGame()
    {
        Application.Quit();
    }

    public GameMaster()
    {
        this.Conversion_Rate = 0.38437502402f;
        this.Time_Left = 60;
        this.Array_Input_Count = 1;
    }

}