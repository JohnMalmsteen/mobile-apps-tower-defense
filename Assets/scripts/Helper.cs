using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour {

    public void Save()
    {
        try
        {
            GameObject.Find("LOADER").GetComponent<Loader>().SaveCurrentState();
        }
        catch { }
    }

    public void Load()
    {
        try
        {
            GameObject.Find("LOADER").GetComponent<Loader>().LoadCurrentState();
        }
        catch { }
    }
}
