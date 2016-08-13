using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntTextIncreaser : MonoBehaviour {

    public Text textZone;
    public int max = 255;
    public string playerPrefKey = "";

    public void Start() {
        LoadData();
    }

    private void LoadData()
    {
        if (textZone && !string.IsNullOrEmpty(playerPrefKey))
            if(PlayerPrefs.HasKey(playerPrefKey))
            textZone.text = "" + PlayerPrefs.GetInt(playerPrefKey);
    }

    public void OnDestroy()
    {
        SaveData();
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        if (textZone && !string.IsNullOrEmpty(playerPrefKey))
            PlayerPrefs.SetInt(playerPrefKey, int.Parse(textZone.text));
    }

    public void Increase()
    {
        if (textZone != null)
        {
            int num;
            if (int.TryParse(textZone.text, out num))
                textZone.text = "" + ((num + 1) % max);
        }

    }
    public void Decrease()
    {
        if (textZone != null)
        {
            int num;
            if (int.TryParse(textZone.text, out num)) {
                num -= 1;
                if (num < 0) num = max;
                textZone.text = "" + ( num % max );
            }
        }
            

    }
}
