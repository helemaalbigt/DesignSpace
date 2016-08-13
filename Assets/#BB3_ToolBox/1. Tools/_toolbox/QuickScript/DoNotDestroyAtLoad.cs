using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoNotDestroyAtLoad : MonoBehaviour {



    public string[] _deathIn;
    public string[] _liveOnlyIn;

    public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
    }
    void OnLevelWasLoaded(int level)
    {
        string levelName = SceneManager.GetActiveScene().name;

        if (_deathIn != null && _deathIn.Length > 0)
        {
            for (int i = 0; i < _deathIn.Length; i++)
            {
                if (levelName.Equals(_deathIn[i]))
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }

        if (_liveOnlyIn != null && _liveOnlyIn.Length > 0)
        {
            bool isLivingInAtLeastOne = false;
            for (int i = 0; i < _liveOnlyIn.Length; i++)
            {
                if (levelName.Equals(_liveOnlyIn[i]))
                {
                    isLivingInAtLeastOne = true;
                    break;
                }
            }
            if (!isLivingInAtLeastOne)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        

    }

}
