using UnityEngine;
using System.Collections.Generic;


public class LinesDrawer : MonoBehaviour {

    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    public LayerMask tiles;
    int cantDrawOverLayerIndex;

    List<string> connectedTags = new List<string>();

    // How many tiles are connected
    int solvedTiles = 0;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    public WirefixPuzzleManager manager;

    Line currentLine;

    Camera cam;


    void Start() {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");

        print($"linePrefab: {linePrefab}");
        print($"cantDrawOverLayer: {cantDrawOverLayer}");
        print($"cantDrawOverLayerIndex: {cantDrawOverLayerIndex}");
        print($"lineColor: {lineColor}");
        print($"linePointsMinDistance: {linePointsMinDistance}");
        print($"lineWidth: {lineWidth}");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw() {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        //Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }
    // Draw ----------------------------------------------------
    void Draw() {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit) {
            Destroy(currentLine.gameObject);
            //print("Here 1");
        } else {
            currentLine.AddPoint(mousePosition);
            //print("Here 2");
        }
    }
    // End Draw ------------------------------------------------
    void EndDraw() {
        if (currentLine != null) {
            if (currentLine.pointsCount < 2) {
                //print("Here 3");
                //If line has one point
                Destroy(currentLine.gameObject);
            } else {
                //print("Here 4");
                //Add the line to "CantDrawOver" layer
                currentLine.gameObject.layer = cantDrawOverLayerIndex;

                //Activate Physics on the line
                currentLine.UsePhysics(false);

                if(CheckConnection(currentLine.GetFirstPoint(), currentLine.GetLastPoint()))
                {
                    Debug.Log("Aaaaaand a hit!");
                    solvedTiles += 1;
                    if (solvedTiles >= manager.numberOfTiles)
                    {
                        //manager.UpdatePuzzleState(puzzleState.solved);
                        Debug.Log("Aaaaaand a win!");
                    }
                }
                else
                {
                    Destroy(currentLine.gameObject);
                }
                currentLine = null;
            }
        }
    }

    bool CheckConnection(Vector2 first, Vector2 last)
    {
        // Checking for hits at the first and last point of the line
        RaycastHit2D beginning = Physics2D.CircleCast(first, lineWidth / 3f, Vector2.zero, 1f, tiles);
        RaycastHit2D end = Physics2D.CircleCast(last, lineWidth / 3f, Vector2.zero, 1f, tiles);

        // Checking if they where hit & they are of the same colour (using tags to define said colours)
        if(beginning && end)
        {
            if(beginning.transform != end.transform && !connectedTags.Contains(beginning.transform.gameObject.tag) && beginning.transform.gameObject.tag == end.transform.gameObject.tag)
            {
                connectedTags.Add(beginning.transform.gameObject.tag);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
