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
            notDead.Add(go);
        }
    }

    public void ImDead(GameObject go)
    {
        notDead.Remove(go);
        dead.Add(go);
    }

    void FixedUpdate()
    {
        if (AmountDead() > 3)
        {
            print(AmountDead());
            Respawn();
        }
        else
            print(AmountDead());
    }

    int AmountDead()
    {
        return dead.Count;
    }

    void Respawn()
    {
        if (dead.Count != 0)
        {
            GameObject temp = dead[0];

            print(temp.name);

            notDead.Add(temp);

            dead.Remove(temp);

            StartCoroutine(UnHide(temp)); // notDead.Peek().gameObject.transform;
        }
    }

    IEnumerator UnHide(GameObject unit)
    {
        if(!unit.GetComponent<Dead>().DEAD)
        {
            float elapsedTime = 0;
            float time = 50.0f;

            Vector3 hiddenPosition = unit.transform.position;
            Vector3 finishPosition = new Vector3(hiddenPosition.x, hiddenPosition.y + 1.5f, hiddenPosition.z);

            //print("Unhiding: " + unit.gameObject.name);

            unit.GetComponent<Dead>().DEAD = false;

            while (elapsedTime < time)
            {
                unit.transform.position = Vector3.Lerp(unit.transform.position, finishPosition, (elapsedTime / time));
                elapsedTime += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
     }

}
