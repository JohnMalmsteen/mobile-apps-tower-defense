using UnityEngine;
using System.Collections;
using System;

public class MouseTile : MonoBehaviour
{
    public GameObject SelectedTile;
    public Boolean ShowTile = false;

    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

        //Debug.Log(position);

        if (position.x >= 0.5f && position.x <= GlobalVars.GridSize + 0.5f && position.z >= 0.5f && position.z <= GlobalVars.GridSize + 0.5f) // Mouse Cursor withing Grid
        {
            SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = true;

            float x, y, z;

            if (position.x < 1)
            {
                x = 1;
            }
            else
            {
                x = (float)Math.Floor(position.x);
            }

            if (position.z < 1)
            {
                z = 1;
            }
            else
            {
                z = (float)Math.Floor(position.z);
            }

            y = (float)Math.Floor(position.y);

            SelectedTile.transform.position = new Vector3(x,y,z);

            SelectedTile.GetComponent<MeshRenderer>().material.color = Grid.CheckAvailabilityOnGridColor(new GridVector((int)SelectedTile.transform.position.x, (int)SelectedTile.transform.position.z));

            //Debug.Log(SelectedTile.transform.position);
        }
        else
            SelectedTile.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

}// MouseTile
