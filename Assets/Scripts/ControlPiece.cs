using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ControlPiece : MonoBehaviour
{
    public GameObject Segment;
    public static int maxamount = 10;
    public int amount = 4;
    public bool IsRotatable = true;
    public bool IsMovable = true;
    public bool TstyleRotation = false;
    private ObjectStateManager[] objectStateManager= new ObjectStateManager[maxamount];
    public int[] ofsetx = new int[maxamount];
    public int[] ofsety = new int[maxamount];
    public int[] ofsetz = new int[maxamount];
    public Color32 pieceCol;
    public static float movementSpeed;
    float movbuff,frezbuff;
    public static float FreezeMargine;
    public static int height;
    int spawnX, spawnY, spawnZ;
    private GameObject[] piecefrag = new GameObject[maxamount];
    private GameObject[] ghostfrag = new GameObject[maxamount];
    private Vector3 DropPos;
    public Camera MainCamera;
    public GameScoreLevelManager gameScoreLevelManager;
    //Instantiating and giving parent to the prefabs making script instances; 
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            piecefrag[i] = Instantiate(Segment, new Vector3(transform.position.x + ofsetx[i], transform.position.y + ofsety[i], transform.position.z + ofsetz[i]), Quaternion.identity, transform) as GameObject;
            if (!isDisabled) { 
                ghostfrag[i] = Instantiate(Segment, new Vector3(transform.position.x + ofsetx[i], transform.position.y + ofsety[i], transform.position.z + ofsetz[i]), Quaternion.identity, transform) as GameObject;
                ghostfrag[i].name = "g " + i;
            }
            else
            {
                Destroy(objectStateManager[i]);
            }
            piecefrag[i].name = "p " + i;
            objectStateManager[i] = piecefrag[i].GetComponent<ObjectStateManager>();
        }
        SetPieceColor();
        if (!isDisabled) { SetGhostPiece(); }
        movementSpeed =Matrixgridcontroller.movementSpeed;
        movbuff = movementSpeed;
        FreezeMargine = GameObject.Find("MG").GetComponent<Matrixgridcontroller>().FreezeMargine;
        frezbuff = GameObject.Find("MG").GetComponent<Matrixgridcontroller>().FreezeMargine;
        spawnX = (int)Mathf.Ceil((InstantiateField.FieldDepth + 2)/2);
        spawnY = (int)Mathf.Ceil((GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().y + 2)/2);
        spawnZ = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z + 2;
        height = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameScoreLevelManager = GameObject.Find("ScoreManager").GetComponent<GameScoreLevelManager>();

        if (!isDisabled)
        {
            UpdateGhostPiece();
        }
    }
    void SetGhostPiece() {
        for (int i = 0; i < amount; i++) {
            MeshRenderer renderer = ghostfrag[i].GetComponent<MeshRenderer>();
            Material material = renderer.material;

            material.SetFloat("_Mode", 4f);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            Color32 col = renderer.material.GetColor("_Color");
            col = pieceCol;
            col.a = 70;
            renderer.material.SetColor("_Color", col);
        }
    }
    void SetPieceColor()
    {
        for (int i = 0; i < amount; i++)
        {
            MeshRenderer renderer = piecefrag[i].GetComponent<MeshRenderer>();
            Material material = renderer.material;
            Color32 col = renderer.material.GetColor("_Color");
            col=pieceCol;
            col.a = 100;
            renderer.material.SetColor("_Color", col);
        }
    }
    void Movedirection(int tx, int ty, int tz, int mx, int mz)
    {
        bool posible = true;
        for (int i = 0; i < amount; i++)
        {
            if (objectStateManager[i].CheckAround(mx, mz) == false)
            {
                posible = false;
                break;
            }
        }
        if (posible)
        {
            transform.position = new Vector3(tx + mx, ty, tz + mz);
        }
        UpdateGhostPiece();
    }
    public void UpdateGhostPiece()
    {
        DropPos = GetFinalPosition();
        for (int i = 0; i < amount; i++)
        {
            ghostfrag[i].transform.position = new Vector3(DropPos.x + ofsetx[i], DropPos.y + ofsety[i], DropPos.z + ofsetz[i]);
        }
    }
    bool ChecktheBounds(int[] ofset1, int[] ofset2, int tx, int ty, int tz, int rotcase)
    {
        bool check = true;
        for (int i = 0; i < amount; i++)
        {
            switch (rotcase)
            {
                case 1: if (Matrixgridcontroller.grid[tx - ofset2[i], ty + ofset1[i], tz + ofsetz[i]] == 1) { check = false; } break;
                case 2: if (Matrixgridcontroller.grid[tx + ofsetx[i], ty - ofset2[i], tz + ofset1[i]] == 1) { check = false; } break;
                case 3: if (Matrixgridcontroller.grid[tx + ofset1[i], ty + ofsety[i], tz - ofset2[i]] == 1) { check = false; } break;
            }
            if (!check) { break; }
        }
        if (check) { return true; } else { return false; }
    }
    void Rotate(int tx, int ty, int tz, int[] rotofset1, int[] rotofset2, int rotcase)
    {
        void RotMain()
        {
            transform.position = new Vector3(tx, ty, tz);
            for (int i = 1; i < amount; i++)
            {
                switch (rotcase)
                {
                    case 1: piecefrag[i].transform.position = new Vector3(tx - rotofset2[i], ty + rotofset1[i], tz + ofsetz[i]); break;
                    case 2: piecefrag[i].transform.position = new Vector3(tx + ofsetx[i], ty - rotofset2[i], tz + rotofset1[i]); break;
                    case 3: piecefrag[i].transform.position = new Vector3(tx + rotofset1[i], ty + ofsety[i], tz - rotofset2[i]); break;
                }
                int buff = rotofset1[i];
                rotofset1[i] = -rotofset2[i];
                rotofset2[i] = buff;
            }
        }
        if (ChecktheBounds(rotofset1, rotofset2, tx, ty, tz, rotcase))
        {
            RotMain();
        }
        else if (ChecktheBounds(rotofset1, rotofset2, tx + 1, ty, tz, rotcase))
        {
            tx += 1;
            RotMain();
        }
        else if (ChecktheBounds(rotofset1, rotofset2, tx - 1, ty, tz, rotcase))
        {
            tx -= 1;
            RotMain();
        }
        else if (ChecktheBounds(rotofset1, rotofset2, tx, ty, tz + 1, rotcase))
        {
            tz += 1;
            RotMain();
        }
        else if (ChecktheBounds(rotofset1, rotofset2, tx, ty, tz - 1, rotcase)) {
            tz -= 1;
            RotMain();
        }
        UpdateGhostPiece();
    }
    void HardDrop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke("FallDown");
            IsMovable = false;
            FreezeMargine = 0;
            movementSpeed = 0;
            gameScoreLevelManager.CurrentScore += (int)(transform.position.y - DropPos.y) * 2;
            transform.position = DropPos;
            CameraEffects.Shake();
        }
    }
    void SoftDrop()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            CancelInvoke("FallDown");
            fallnow = true;
            FreezeMargine = 0;
            movementSpeed = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            movementSpeed = movbuff;
            FreezeMargine = frezbuff;
        }
    }
    private Transform[] Child = new Transform[maxamount];
    void TheStop()
    {
        if (IsLokedCheck()) { Matrixgridcontroller.spin = true; }
        for (int i = 0; i < amount; i++) {
            objectStateManager[i].State = 2;
            Destroy(ghostfrag[i]);
        }
    }
    bool IsLokedCheck()
    {
        bool moveSidePosible = true;
        bool moveUpPosible = true;
        for (int i = 0; i < amount; i++)
        {
            //for (int mx = -1; mx < 2; mx++)
            //{
                for (int mz = -1; mz < 2; mz++)
                {
                    if (objectStateManager[i].CheckAround(0, mz) == false)
                    {
                        moveSidePosible = false;
                        break;
                    }
                }
                if (!moveSidePosible) { break; }
            //}
            //if (!moveSidePosible) { break; }
        }
        for(int i = 0; i < amount; i++)
        {
            if (objectStateManager[i].CheckAbove() == false)
            {
                moveUpPosible = false;
                break;
            }
        }
        if (!moveSidePosible && !moveUpPosible)
        {
            return true;
        }
        return false;
    }
    bool fallnow = true;
    Vector3 GetFinalPosition() {
        int tx = (int)transform.position.x;
        int ty = (int)transform.position.y;
        int tz = (int)transform.position.z;
        Vector3 eventualCoor = new Vector3(tx,ty,tz);
        bool possible = true;
        for(int j = 0; j < height+2; j++)
        {
            for (int i = 0; i < amount; i++)
            {
                if (Matrixgridcontroller.grid[tx+ofsetx[i],ty+ofsety[i]-1,tz+ofsetz[i]]==1)
                {
                    possible = false; break;
                }
            }
            if (possible)
            {
                ty -= 1;
                eventualCoor = new Vector3(tx, ty, tz);
            }
            else {
                break;
            }
        }
        return eventualCoor;
    }
    void FallDown()
    {
        int tx = (int)transform.position.x;
        int ty = (int)transform.position.y;
        int tz = (int)transform.position.z;
        bool possible = true;
        for(int i = 0; i < amount; i++)
        {
            if (objectStateManager[i].CheckBelow())
            {
                possible = false; break;
            }
            ghostfrag[i].transform.position = new Vector3(DropPos.x + ofsetx[i], DropPos.y + ofsety[i]+1, DropPos.z + ofsetz[i]);
        }
        if (possible) { 
        transform.position = new Vector3(tx, ty - 1, tz); }
        fallnow = true;
    }
    bool inhold = false;
    bool isDisabled = false;
    public void DisablePiece()
    {
        IsMovable = false;
        fallnow = false;
        CancelInvoke("FallDown");
        isDisabled = true;
    }
    private Quaternion preHoldRotation;
    void Update()
    {
        if (!isDisabled)
        {
            if (objectStateManager[0].State == 1 && fallnow) {Invoke("FallDown", movementSpeed);fallnow = false;}
            if (objectStateManager[0].State == 1 || objectStateManager[0].State == 0)
            {
                int cx = (int)piecefrag[0].transform.position.x; int cy = (int)piecefrag[0].transform.position.y; int cz = (int)piecefrag[0].transform.position.z;
                //Doing the stopping if the bottom pieces should stop
                bool cancelStop = true;
                for (int i = 0; i < amount; i++)
                {
                    if (objectStateManager[i].CheckBelow())
                    {
                        for (int j = 0; j < amount; j++) { objectStateManager[j].State = 0; }
                        cancelStop = false;
                        Invoke("TheStop", FreezeMargine);
                        break;
                    }
                }
                if (cancelStop)
                {
                    CancelInvoke("TheStop");
                    for (int i = 0; i < amount; i++) { objectStateManager[i].State = 1; }
                }
                //else if (objectStateManager.fallnow) - neshto takova zashtoto se suzdavat nqkakvi bugove i chastite se razglobqvat
                if (IsMovable)
                {
                    int flipControls = 1;
                    if (Matrixgridcontroller.fliped)
                    {
                        flipControls = -1;
                    }
                    switch (Input.inputString)
                    {

                        case "4":
                            Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, -1 * flipControls);
                            return;
                        case "6":
                            Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 0, 1 * flipControls);
                            return;
                        case "8":
                            Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, -1 * flipControls, 0);
                            return;
                        case "f":
                        case "5":
                            Movedirection((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, 1 * flipControls, 0);
                            return;
                        case "z":
                            if (IsRotatable)
                            {
                                Rotate(cx, cy, cz, ofsetx, ofsety, 1);
                                return;
                            }
                            break;
                        case "x":
                            if (IsRotatable)
                            {
                                Rotate(cx, cy, cz, ofsety, ofsetz, 2);
                                return;
                            }
                            break;
                        case "c":
                            if (IsRotatable)
                            {
                                Rotate(cx, cy, cz, ofsetz, ofsetx, 3);
                                return;
                            }
                            break;
                    }
                    SoftDrop();
                }
                HardDrop();
            }
            if (objectStateManager[0].State == 2)
            {
                objectStateManager[0].Invoke("playStopSound", 0.05f);
                //Deleting and destroying parent; ready for new piece 
                Matrixgridcontroller.oktoSpawn = true;
                for (int i = 0; i < amount; i++)
                {
                    Child[i] = transform.Find("p " + i);
                    Child[i].parent = null;
                }
                Destroy(this.gameObject);
            }

            //Hold mechanism
            if (Input.GetKeyDown(KeyCode.LeftShift) && !InstantiateField.inRotation)
            {
                if (!inhold)
                {
                    preHoldRotation = transform.rotation;
                    inhold = true;
                    IsMovable = false;
                    fallnow = false;
                    CancelInvoke("FallDown");
                    for (int i = 0; i < amount; i++)
                    {
                        objectStateManager[i].State = 3;
                    }
                    int flippCoef = 1;
                    if (Matrixgridcontroller.fliped) { flippCoef = -1; } else { flippCoef = 1; }
                    transform.parent = MainCamera.transform;
                    transform.position = new Vector3(transform.parent.transform.position.x - 19 * flippCoef, transform.parent.transform.position.y - 3.5f, transform.parent.transform.position.z - 6.5f * flippCoef);
                    for (int i = 0; i < amount; i++)
                    {
                        ghostfrag[i].transform.position = new Vector3(transform.parent.transform.position.x - 19 * flippCoef, transform.parent.transform.position.y - 3.5f, transform.parent.transform.position.z - 6.5f * flippCoef);
                    }
                    return;
                }
                else
                {
                    transform.rotation = preHoldRotation;
                    transform.parent = null;
                    IsMovable = true;
                    fallnow = true;
                    for (int i = 0; i < amount; i++)
                    {
                        objectStateManager[i].State = 1;
                    }
                    transform.position = new Vector3(spawnX, spawnZ, spawnY);
                    inhold = false;

                    UpdateGhostPiece();
                    return;
                }
            }
            if (inhold) {
                transform.Rotate(new Vector3(transform.rotation.x+1, transform.rotation.y + 1, transform.rotation.z + 1));
            }
        }
    }
}
