using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridItem : MonoBehaviour 
{
    public GridMaster gridmaster;
    public bool clicked;
    public Sprite xIcon;
    public Sprite nonX;
    public int x;
    public int y;

    public List<GameObject> matches = new List<GameObject>();
    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
    void OnMouseDown()
    {
        this.clicked = !clicked;

    }

    // Start is called before the first frame update
    void Start()
    {
        gridmaster = this.transform.parent.GetComponent<GridMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeighboor();

        if (clicked)
        {
            GetComponent<SpriteRenderer>().sprite = xIcon;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = nonX;
        }
        
    }
    public void CheckNeighboor()
    {
            //Checking for neighboor X'es and adding them to matches list.
            if (x < gridmaster.n - 1 && gridmaster.gridItems[x + 1, y].GetComponent<GridItem>().clicked)
            {
                right = gridmaster.gridItems[x + 1, y];
                if (!matches.Contains(right))
                {
                    matches.Add(right);
                }
            }
            if (x > 0 && gridmaster.gridItems[x - 1, y].GetComponent<GridItem>().clicked)
            {
                left = gridmaster.gridItems[x - 1, y];
                if (!matches.Contains(left))
                {
                    matches.Add(left);
                }
            }
            if (y > 0 && gridmaster.gridItems[x, y - 1].GetComponent<GridItem>().clicked)
            {
                up = gridmaster.gridItems[x, y - 1];
                if (!matches.Contains(up))
                {
                    matches.Add(up);
                }
            }
            if (y < gridmaster.n - 1 && gridmaster.gridItems[x, y + 1].GetComponent<GridItem>().clicked)
            {
                down = gridmaster.gridItems[x, y + 1];
                if (!matches.Contains(down))
                {
                    matches.Add(down);
                }
            }

        //removing gameobjects in matches that is not a X
        for (int i = 0; i < matches.Count; i++)
        {
            if (!matches[i].GetComponent<GridItem>().clicked)
            {
                matches.RemoveAt(i);
            }
        }
    }

}
