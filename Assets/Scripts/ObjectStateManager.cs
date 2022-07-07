using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectStateManager : MonoBehaviour
{
    public int State = 1;//1=falling(Dynamic); 2=solid(Static); 0=movable;
    public static int height;
    public int fallAmount=1;
    AudioSource audioData;
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        height = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z;
    }
    public bool CheckAround(int posokaX,int posokaZ)
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
        if (Matrixgridcontroller.grid[cx, cy - 1, cz] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckAbove()
    {
        int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
        if (Matrixgridcontroller.grid[cx, cy + 1, cz] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void playStopSound()
    {
        audioData.Play(0);
    }
    List<int> movedLine = new List<int>();
    void Update()
    {
        if (State==2 )
        {
            int cx = (int)transform.position.x; int cy = (int)transform.position.y; int cz = (int)transform.position.z;
            Matrixgridcontroller.grid[cx, cy, cz] = 1;
            //Destroy if segment is part of a full line
            if (Matrixgridcontroller.LineToClear)
            {
                movedLine = Matrixgridcontroller.LineToClearHeights;
                foreach (int lineHeight in movedLine)
                {
                    if (cy == lineHeight)
                    {
                        Matrixgridcontroller.grid[cx, cy, cz] = 0;
                        Destroy(this.gameObject);
                    }
                }
            }
            //move other segments on field down
            else
            {
                if (movedLine.Count>0)
                {
                    movedLine=movedLine.OrderByDescending(line=>line).ToList();
                    foreach (int lineHeight in movedLine)
                    {
                        cy= (int)transform.position.y;
                        if (cy > lineHeight)
                        {
                            Matrixgridcontroller.grid[cx, cy, cz] = 0;
                            Matrixgridcontroller.grid[cx, cy - 1, cz] = 1;
                            transform.position = new Vector3(cx, cy - 1, cz);
                        }
                    }
                    movedLine.Clear();
                    Matrixgridcontroller.CurrentPiece.GetComponent<ControlPiece>().UpdateGhostPiece();
                }
            }
            if (Matrixgridcontroller.gameEnded)
            {
                transform.GetComponent<Renderer>().material.SetColor("_Color",Color.gray);
                Matrixgridcontroller.grid[cx, cy, cz] = 0;
            }
        }
    }
}
