using UnityEngine;
using System.Collections.Generic;
using Vectrosity;

public class DrawGrid : MonoBehaviour
{
    private int GridSize;

    private List<Vector3> LinePoints;

    private float GridHeight = 0.075f;
    private float LineWidth = 0.2f;
    private string LineName = "Vectrosity Grid Line";
    private string VectrosityCamera = "VectrosityCamera";

    public GameObject clickableTile;
    public GameObject clickableTileParent;

    public Material MarbleBlack;
    public Material MarbleWhite;

    void Start()
    {
        GridSize = GlobalVars.GridSize;

        VectorLine.SetCamera3D(GameObject.Find(VectrosityCamera));
        LinePoints = new List<Vector3>();

        // Init Stuff

        //CreateGridLines();

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
        int count = 0;

        for (int i = 1; i <= GridSize; i++)
        {
            for (int j = 1; j <= GridSize; j++)
            {
                GameObject temp = Instantiate(clickableTile, new Vector3(j, 0.06f, i), Quaternion.identity) as GameObject;

                temp.gameObject.name = j + " : " + i;

                temp.GetComponent<PlaceorMove>().setGrid(j, i);

                temp.transform.SetParent(clickableTileParent.transform);

                if(count % 2 == 0)
                    temp.GetComponent<MeshRenderer>().material = MarbleBlack;
                else
                    temp.GetComponent<MeshRenderer>().material = MarbleWhite;

                ++count;
            }
        }
    }

}// DrawGrid
