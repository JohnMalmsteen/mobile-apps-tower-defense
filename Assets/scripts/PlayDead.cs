using UnityEngine;
using System.Collections;

public class PlayDead : MonoBehaviour
{
    public Material playMat;

    void Start()
    {
        transform.Find("Tops").GetComponent<SkinnedMeshRenderer>().material = playMat;
        GetComponent<Animator>().Play("Death");
    }
}
