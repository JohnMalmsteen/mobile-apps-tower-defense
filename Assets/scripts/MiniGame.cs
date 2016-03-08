using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniGame : MonoBehaviour {

    public GameObject[] Archers;

    public List<GameObject> dead;
    public List<GameObject> notDead;

    void Start()
    {
        dead = new List<GameObject>();
        notDead = new List<GameObject>();

        foreach (GameObject go in Archers)
        {
            dead.Add(go);
        }
    }

    public void ImDead(GameObject go)
    {
        notDead.Remove(go);
    }

    void FixedUpdate()
    {
        if (AmountDead() > (Archers.Length / 2))
            Respawn();
    }

    int AmountDead()
    {
        return dead.Count;
    }

    void Respawn()
    {
        GameObject temp = dead[0];

        notDead.Add(temp);

        dead.Remove(temp);

        StartCoroutine(UnHide(temp)); // notDead.Peek().gameObject.transform;
    }

    IEnumerator UnHide(GameObject unit)
    {
        float elapsedTime = 0;
        float time = 20.0f;

        Vector3 hiddenPosition = unit.transform.position;
        Vector3 finishPosition = new Vector3(hiddenPosition.x, hiddenPosition.y + 1.5f, hiddenPosition.z);

        //print("Unhiding: " + unit.gameObject.name);

        while (elapsedTime < time)
        {
            unit.transform.position = Vector3.Lerp(unit.transform.position, finishPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

}
