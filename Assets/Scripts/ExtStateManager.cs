/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtStateManager : MonoBehaviour
{
    public int State = 1;//1=falling(Dynamic); 2=solid(Static); 0=movable;
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = GameObject.Find("MG").GetComponent<Matrixgridcontroller>().movementSpeed;

    }
    // Update is called once per frame
    public bool CheckAround(int posokaX, int posokaZ)
    {
        int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
        if (Matrixgridcontroller.grid[cx + posokaX, cy, cz + posokaZ] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckBelow()
    {
        int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
        if (Matrixgridcontroller.grid[cx, cy-1, cz] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool fallnow = true;
    public void FallDown()
    {
        int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
        if (Matrixgridcontroller.grid[cx, cy - 1, cz] == 0)
        {
            //Fall
            transform.position = new Vector3(cx, cy - 1, cz);
        }
        fallnow = true;
        return;
    }
    bool changeColorTrig = false;
    void Update()
    {
        if (State == 1)
        {
            if (fallnow) { Invoke("FallDown", movementSpeed);fallnow = false; }
        }
        if (State == 2)
        {
            if (!changeColorTrig)
            {
                transform.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                changeColorTrig = true;
            }
            CancelInvoke();
            int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
            Matrixgridcontroller.grid[cx, cy, cz] = 1;
            /*if (cy <= Matrixgridcontroller.Howmanylinestoclear)
            {
                Matrixgridcontroller.grid[cx, cy, cz] = 0;
                Object.Destroy(this.gameObject);

            }
            else
            {
                if (Matrixgridcontroller.grid[cx, cy - 1, cz] != 1)
                {
                    //transform.position = new Vector3(cx, cy - 1, cz);
                    Matrixgridcontroller.grid[cx, cy, cz] = 0;
                    Matrixgridcontroller.grid[cx, cy - 1, cz] = 1;
                }
            }
        }
    }
}*/

