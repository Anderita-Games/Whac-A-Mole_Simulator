using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Unit : MonoBehaviour
{
    public virtual void Start()
    {
        this.StartCoroutine(this.DEATH_AGE());
    }

    public virtual void Update()
    {
        if (PlayerPrefs.GetString("Remove") == "GAMEOVER")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual IEnumerator DEATH_AGE()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public virtual void Death_CLICKED()
    {
        if (PlayerPrefs.GetString("Remove") != "GAMEOVER")
        {
            PlayerPrefs.SetInt("Score_Current", PlayerPrefs.GetInt("Score_Current") + 1);
            PlayerPrefs.SetInt("MoleTotal", PlayerPrefs.GetInt("MoleTotal") - 1);
            PlayerPrefs.SetString("Remove", this.gameObject.name);
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}