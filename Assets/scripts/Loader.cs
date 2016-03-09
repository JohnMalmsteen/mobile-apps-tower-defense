using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loader : MonoBehaviour {

    
    public static int GoldCount;
    public SaveState saveState;

    public static Loader instance = null;

    private Loader() { }

    public static Loader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Loader();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveCurrentState()
    {
        print("Saving");

        saveState = new SaveState();

        saveState.GoldCount = GlobalVars.GoldCount;

        Application.LoadLevel("testScene");
    }

    public void LoadCurrentState()
    {

    }

}
