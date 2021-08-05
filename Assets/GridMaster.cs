using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMaster : MonoBehaviour
{
    public InputField input;
    public int n;
    public GameObject[,] gridItems;
    public GameObject GridItem;
    public List<GameObject> matches = new List<GameObject>();
    private float screenHeight;
    private float screenWidth;
    HashSet<GameObject> matchingcell = new HashSet<GameObject>();
    private float gridItemSize;
    public void Start()
    {
        //finding the camera size and streching the background sprite according to it.
        screenHeight = Camera.main.orthographicSize * 2.0f;
        screenWidth = screenHeight / Screen.height * Screen.width;
        this.GetComponent<SpriteRenderer>().size = new Vector2(screenWidth, screenHeight);
        
        GenerateGrid();
    }
    public void Update()
    {
        check();
    }

    public void CreateGrid()
    {
        for (int i = 0; i < gridItems.GetLength(0); i++)
        {
            for (int j = 0; j < gridItems.GetLength(1); j++)
            {
                Destroy(gridItems[i, j]);
            }
        }
        matchingcell.Clear();
        this.n = int.Parse(input.text);
        GenerateGrid();
    }

    public List<GameObject> GetNextMatchList(GameObject g)
    {
        if (g.GetComponent<GridItem>().clicked && g.GetComponent<GridItem>().matches.Count > 0)
        {
            return g.GetComponent<GridItem>().matches;
        }
        return null;
    }
    public void check()
    {
        foreach (GameObject g in gridItems)
        {
            //if it is X and have more than one neighboor
            if (g.GetComponent<GridItem>().clicked && g.GetComponent<GridItem>().matches.Count > 1)
            {
                
                g.GetComponent<GridItem>().matches.ForEach(x =>
                {
                    matchingcell.Add(x);
                    //find neighboors of neighboor
                    List<GameObject> newmatch = GetNextMatchList(x);
                    foreach(GameObject nweg in newmatch)
                    {
                        matchingcell.Add(nweg);
                    }

                });
                
                //if total neighbooring X count is larger than or equal to 3 then destroy X'es.
                if (matchingcell.Count > 2)
                {
                    //Debug.Log(matchingcell.Count);
                    foreach (GameObject matchingG in matchingcell)
                    {
                        //Debug.Log(matchingG.name);
                        matchingG.GetComponent<GridItem>().clicked = false;
                    }
                    matchingcell.Clear();
                    g.GetComponent<GridItem>().clicked = false;
                }
               
            }
        }

    }


    public void GenerateGrid()
    {
        //Determine the size of any item on the grid.
        gridItemSize = screenWidth / n;
        //startpositions, upperleftmost corner of the background sprite
        var startposx= (screenWidth / -2) + gridItemSize / 2;
        var startposy = (screenHeight / 2) - gridItemSize / 2;
        gridItems = new GameObject[n,n];

        //spawning the grid cells with appropriate properties.
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var obj = Instantiate(GridItem, this.transform,false);
                obj.transform.position = new Vector3(startposx+i*gridItemSize, startposy-j*gridItemSize);
                obj.gameObject.name = "Item" + " (" + i +"-"+ j+")";
                obj.GetComponent<GridItem>().x = i;
                obj.GetComponent<GridItem>().y = j;
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                obj.GetComponent<SpriteRenderer>().size = new Vector2(gridItemSize, gridItemSize);
                obj.GetComponent<BoxCollider2D>().size = new Vector2(gridItemSize, gridItemSize);
                gridItems[i, j] = obj;
            }
           

        }
    }
}
