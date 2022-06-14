/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPieceSave : MonoBehaviour
{
    public GameObject Core;
    public GameObject Extend;
    private ObjectStateManager objectStateManager;
    public static int amount=3;
    private ExtStateManager[] extstate= new ExtStateManager[amount];
    public int[] ofsetx = new int[amount];
    public int[] ofsety = new int[amount];
    public int[] ofsetz = new int[amount];
    public static float movementSpeed;
    public static int height;
    GameObject core;
    //Instantiating and giving parent to the prefabs making script instances; 
    private GameObject[] ext=new GameObject[amount];
    void Start()
    {
        core = Instantiate(Core, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform) as GameObject;
        core.name = "Core(Clone)" + Matrixgridcontroller.nextNameNumber;
        objectStateManager = core.GetComponent<ObjectStateManager>();
        for(int i = 0; i < extstate.Length; i++)
        {
            ext[i]= Instantiate(Extend, new Vector3(transform.position.x+ofsetx[i], transform.position.y+ofsety[i], transform.position.z+ofsetz[i]), Quaternion.identity, transform) as GameObject;
            ext[i].name = "t " + i;
            extstate[i] = ext[i].GetComponent<ExtStateManager>();
        }  
        movementSpeed = GameObject.Find("MG").GetComponent<Matrixgridcontroller>().movementSpeed;
        height = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z;
    }
    void CalculateBounds(int[] ofsetx,int[] ofsety, int[] ofsetz) {
        for(int i = ofsety.Min(); i < ofsety.Max()+1; i++)
        {
            for(int j = 0; j < amount; j++)
            {
                if (ofsety[j] == i)
                {
                    curentOfsetsx[i, j] = ofsetx[j];
                    curentOfsetsz[i, j] = ofsetz[j];
                    Debug.Log($"{curentOfsetsx[i,j]} {curentOfsetsz[i,j]}");
                }
            }
        }
    }

    // !!! IMportant pro rotation script trqbva da promenqm i stoinostite na ofsetite !!!

    static int[,] curentOfsetsx = new int[6,6];
    static int[,] curentOfsetsz = new int[6,6];
        //void Rotate kudeto se vika CalculateBounds Einstveno sled povikvaneto i zavurtaneto izchislqva i novata y visochina;
    void Rotate()
    {
        CalculateBounds(ofsetx,ofsety,ofsetz);
    }
    void Movedirection(int tx, int ty, int tz, int mx, int mz, string key, int gridx, int gridy, int gridz)
    {
        if (Input.GetKeyDown(key))
        {
            if (Matrixgridcontroller.grid[gridx + 1 + mx, gridy, gridz + mz] == 0 && Matrixgridcontroller.grid[gridx - 1 + mx, gridy, gridz + mz] == 0)//some arguments acording to rotation
            {
                CalculateBounds(ofsetx, ofsety, ofsetz);
                transform.position = new Vector3(tx + mx, ty, tz + mz);
                //--Imp--Matrixgridcontroller.grid[x, y, z] = 0;
                //Debug.Log(x + " " + y + " " + z + " grid= " + Matrixgridcontroller.grid[x, y, z]);
                //--Imp--Matrixgridcontroller.grid[x + mx, y, z + mz] = 1;
            }

        }
    }
    private Transform[] Child = new Transform[amount];
    void Update()
    {

        if (objectStateManager.State == 1)
        {
            int cx = (int)core.transform.position.x; int cy = (int)core.transform.position.y; int cz = (int)core.transform.position.z;
            if (objectStateManager.frame == movementSpeed - 1)
            {
                //Doing the stopping if the bottom pieces should stop
                if (Matrixgridcontroller.grid[cx, cy - 1, cz] == 1 || Matrixgridcontroller.grid[cx - 1, cy - 1, cz] == 1 || Matrixgridcontroller.grid[cx + 1, cy - 1, cz] == 1)
                {
                    objectStateManager.State = 2;
                    for(int i = 0; i < extstate.Length; i++) { extstate[i].State = 2; }
                }
            }
            if (objectStateManager.frame % movementSpeed != 0)
            {
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, -1, "a", cx, cy, cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, 1, "d", cx, cy, cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, -1, 0, "w", cx, cy, cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 1, 0, "s", cx, cy, cz);
            }
        }
        if (objectStateManager.State == 2)
        {
            //Deleting and destroying parent; ready for new piece 
            Matrixgridcontroller.oktoSpawn = true;
            Transform Childc = transform.Find("Core(Clone)" + Matrixgridcontroller.nextNameNumber);
            Childc.parent = null;
            for(int i = 0; i < amount; i++)
            {
                Child[i] = transform.Find("t " + i);
                Child[i].parent = null;
            }
            Matrixgridcontroller.nextNameNumber++;
            Object.Destroy(this.gameObject);
        }
    }
}
*/