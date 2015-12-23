using UnityEngine;
using System.Collections;

public class MusicDontDie : MonoBehaviour
{

    private static MusicDontDie instance = null;

    public static MusicDontDie Instance
    {
        get { return instance; }
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
}
