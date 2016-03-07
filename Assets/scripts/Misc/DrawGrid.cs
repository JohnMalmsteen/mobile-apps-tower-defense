using UnityEngine;
using System.Collections.Generic;
using Vectrosity;

public class DrawGrid : MonoBehaviour
{
    private int GridSize;

    private List<Vector3> LinePoints;

    private float GridHeight = 0.01f;
    private float LineWidth = 0.2f;
    private string LineName = "Vectrosity Grid Line";
    private string VectrosityCamera = "VectrosityCamera";

    public GameObject clickableTile;
    public GameObject clickableTileParent;

    void Start()
    {
        GridSize = GlobalVars.GridSize;

        VectorLine.SetCamera3D(GameObject.Find(VectrosityCamera));
        LinePoints = new List<Vector3>();

        // Init Stuff

        CreateGridLines();

        // Creating array of lines

        var myLine = new VectorLine(LineName, LinePoints, null, LineWidth);
        myLine.SetColor(Color.white);

        // Setting up the line

        myLine.Draw3D();
        
        GameObject.Find(LineName).transform.position = new Vector3(0.5f, GridHeight, 0.5f);
        // Position Camera with the Grid

        CreateClickableTile();

    }// Start

    void CreateGridLines()
    {
        for (int i = 0; i <= GridSize; i++)
        {
            LinePoints.Add(new Vector3(i, GridHeight, 0));
            LinePoints.Add(new Vector3(i, GridHeight, GridSize));
        }

        for (int i = 0; i <= GridSize; i++)
        {
            LinePoints.Add(new Vector3(0, GridHeight, i));
            LinePoints.Add(new Vector3(GridSize, GridHeight, i));
        }

    }// CreateGridLines()

    void CreateClickableTile()
    {
        for (int i = 0; i <= GridSize; i++)
        {
            for (int j = 0; j <= GridSize; j++)
            {
                GameObject temp = Instantiate(clickableTile, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

                temp.gameObject.name = i + " : " + j;

                temp.transform.SetParent(clickableTileParent.transform);
            }
        }
    }

}// DrawGrid
