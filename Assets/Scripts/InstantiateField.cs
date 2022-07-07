using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateField : MonoBehaviour
{

    private int x=8;
    public int y=8;
    public int z=9;
    public static int FieldDepth=2;
    public GameObject wall;
    public GameObject cameraPivot;
    public GameObject barrier;
    Animator pivotMove;
    void Start()
    {
        x = FieldDepth;
        cameraPivot.transform.position = new Vector3(x / 2.0f, z / 2.0f, y / 2.0f);
        pivotMove = cameraPivot.GetComponent<Animator>();
        GameObject top = Instantiate(barrier, new Vector3((x+1)/2.0f,z+1,(y+1)/2.0f),Quaternion.identity) as GameObject;
        top.transform.parent = transform;
        top.transform.localScale = new Vector3(x/10f,1/10f,y/10f);
        for(int i = 1; i < x+1; i++)
        {
            for(int j = 1; j < y+1; j++)
            {
                //floor
                GameObject floor = Instantiate(wall, new Vector3(i, 0.5f, j), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                floor.transform.parent = transform;
                Matrixgridcontroller.grid[i, 0, j] = 1;
            }
        }
        for (int i = 1; i < x+2-1; i++)
        {
            for (int k = 1; k < z+4; k++)
            {
                //walls on x
                GameObject wall1 = Instantiate(wall, new Vector3(i, k, 0.5f), Quaternion.Euler(90f, 0f, 0f)) as GameObject;
                GameObject wall2 = Instantiate(wall, new Vector3(i, k, y+2-1.5f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                wall1.transform.parent = transform;
                wall2.transform.parent = transform;
                Matrixgridcontroller.grid[i, k, 0] = 1;
                Matrixgridcontroller.grid[i, k, y+2-1] = 1;
            }
        }
        for (int k = 1; k < z+4; k++)
        {
            for (int j = 1; j < y+2-1; j++)
            {
                //walls on y
                GameObject wall1 = Instantiate(wall, new Vector3(0.5f, k, j), Quaternion.Euler(0f, 0f,-90f)) as GameObject;
                GameObject wall2 = Instantiate(wall, new Vector3(x+2-1.5f, k, j), Quaternion.Euler(0f, 0f,90f)) as GameObject;
                wall1.transform.parent = transform;
                wall2.transform.parent = transform;
                Matrixgridcontroller.grid[0, k, j] = 1;
                Matrixgridcontroller.grid[x+2-1, k, j] = 1;
            }
        }
        //Debug.Log(Matrixgridcontroller.grid[5, 16, 5]);
    }
    public static bool inRotation = false;
    void Update()
    {

        if (Input.GetButtonDown("Flip")) {
            if (!pivotMove.GetCurrentAnimatorStateInfo(0).IsName(null))
            {
                if (!Matrixgridcontroller.fliped)
                {
                    pivotMove.Play("FlipForward");
                    Matrixgridcontroller.fliped = true;
                }
                else
                {
                    pivotMove.Play("FlipBackward");
                    Matrixgridcontroller.fliped = false;
                }
                inRotation = true;
            }
        }
        if (pivotMove.GetCurrentAnimatorStateInfo(0).length < pivotMove.GetCurrentAnimatorStateInfo(0).normalizedTime) {
            inRotation = false;
        }
    }
}
