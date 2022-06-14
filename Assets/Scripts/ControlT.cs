/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlT : MonoBehaviour
{
    public GameObject Core;
    public GameObject Extend;
    private ObjectStateManager objectStateManager;
    private ExtStateManager extstate1;
    private ExtStateManager extstate2;
    private ExtStateManager extstate3;
    public static float movementSpeed;
    public static int height;
    GameObject core;
    //Instantiating and giving parent to the prefabs making script instances; 
    void Start()
    {
        //Instantiate(Extend, new Vector3(transform.position.x, transform.position.y, transform.position.z+1), Quaternion.identity);
        /*GameObject core = Instantiate(Core, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        core.name = "Core(Clone)"+Matrixgridcontroller.nextNameNumber;
        core.transform.parent = GameObject.Find("T-piece(Clone)").transform;
        GameObject Ext1 = Instantiate(Extend, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        Ext1.name = "t 1";
        Ext1.transform.parent = GameObject.Find("T-piece(Clone)").transform;
        GameObject Ext2 = Instantiate(Extend, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        Ext2.name = "t 2";
        Ext2.transform.parent = GameObject.Find("T-piece(Clone)").transform;
        GameObject Ext3 = Instantiate(Extend, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity) as GameObject;
        Ext3.name = "t 3";
        Ext3.transform.parent = GameObject.Find("T-piece(Clone)").transform;
        core = Instantiate(Core, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity,transform) as GameObject;
        core.name = "Core(Clone)" + Matrixgridcontroller.nextNameNumber;
        objectStateManager = core.GetComponent<ObjectStateManager>();
        GameObject Ext1 = Instantiate(Extend, new Vector3(transform.position.x-1, transform.position.y, transform.position.z), Quaternion.identity, transform) as GameObject;
        Ext1.name = "t 1";
        extstate1 = Ext1.GetComponent<ExtStateManager>();
        GameObject Ext2 = Instantiate(Extend, new Vector3(transform.position.x+1, transform.position.y, transform.position.z), Quaternion.identity, transform) as GameObject;
        Ext2.name = "t 2";
        extstate2 = Ext2.GetComponent<ExtStateManager>();
        GameObject Ext3 = Instantiate(Extend, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.identity, transform) as GameObject;
        Ext3.name = "t 3";
        extstate3 = Ext3.GetComponent<ExtStateManager>();
        movementSpeed = GameObject.Find("MG").GetComponent<Matrixgridcontroller>().movementSpeed;
        height= GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z;
    }
    void Movedirection(int tx, int ty, int tz, int mx, int mz, string key,int gridx, int gridy, int gridz)
    {
        if (Input.GetKeyDown(key))
        {
            if (Matrixgridcontroller.grid[gridx +1+ mx, gridy, gridz + mz] == 0 && Matrixgridcontroller.grid[gridx - 1 + mx, gridy, gridz + mz] == 0)//some arguments acording to rotation
            {
                transform.position = new Vector3(tx + mx, ty, tz + mz);
                //--Imp--Matrixgridcontroller.grid[x, y, z] = 0;
                //Debug.Log(x + " " + y + " " + z + " grid= " + Matrixgridcontroller.grid[x, y, z]);
                //--Imp--Matrixgridcontroller.grid[x + mx, y, z + mz] = 1;
            }

        }
    }
    void Update()
    {

        if (objectStateManager.State == 1)
        {
            int cx = (int)core.transform.position.x; int cy = (int)core.transform.position.y; int cz = (int)core.transform.position.z;
            if (objectStateManager.frame == movementSpeed-1)
            {
                //Doing the stopping if the bottom pieces should stop
                if (Matrixgridcontroller.grid[cx, cy - 1, cz] == 1 || Matrixgridcontroller.grid[cx - 1, cy - 1, cz] == 1 || Matrixgridcontroller.grid[cx + 1, cy - 1, cz] == 1)
                {
                    objectStateManager.State = 2;
                    extstate1.State = 2;
                    extstate2.State = 2;
                    extstate3.State = 2;
                }   
            }
            if (objectStateManager.frame % movementSpeed != 0)
            {
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, -1, "a",cx,cy,cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, 1, "d", cx, cy, cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, -1, 0, "w", cx, cy, cz);
                Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 1, 0, "s", cx, cy, cz);
            }
        }
        if (objectStateManager.State == 2)
        {
            //Deleting and destroying parent; ready for new piece 
            Matrixgridcontroller.oktoSpawn = true;
            Transform Child = transform.Find("Core(Clone)"+ Matrixgridcontroller.nextNameNumber);
            Transform Child1 = transform.Find("t 1");
            Transform Child2 = transform.Find("t 2");
            Transform Child3 = transform.Find("t 3");
            Child.parent = null;
            Child1.parent = null;
            Child2.parent = null;
            Child3.parent = null;
            Matrixgridcontroller.nextNameNumber++;
            Object.Destroy(this.gameObject);
        }
    }
}*/
