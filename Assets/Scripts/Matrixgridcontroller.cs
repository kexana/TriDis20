using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrixgridcontroller : MonoBehaviour
{
    public static int x;
    public static int y;
    public static int z;
    public GameObject []pieces;
    public static float movementSpeed= 0.8f;
    public float FreezeMargine = 0.4f;
    public AudioSource LineSound;
    public AudioClip OneLiner;
    public AudioClip DoubleLiner;
    public AudioClip TripleLiner;
    public AudioClip Tetris;
    public AudioClip SpinSound;
    public GameScoreLevelManager gameScoreLevelManager ;
    public GameObject GameOverCanvas;
    public Camera mainCamera;
    public static GameObject CurrentPiece;
    void Awake()
    {
        //--------Linking the parameters from IsntantiateField script------
        x = InstantiateField.FieldDepth+2;
        y = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().y+2;
        z = GameObject.Find("GameFieldCrator").GetComponent<InstantiateField>().z+1;
        //Debug.Log(x+" "+y+" "+z);
        LineSound=GetComponent<AudioSource>();
    }
    public static int[,,] grid = new int[100, 100, 100];
    int nextPieceType;
    void Start()
    {
        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < z; j++)
            {
                for(int k = 0; k < y; k++)
                {
                    grid[i, j, k] = 0;
                }
            }
        }
        resetBag(pieces.Length);
        nextPieceType = Random.Range(0, partsBag.Count);
        GameOverCanvas.SetActive(false);
        gameEnded = false;
        oktoSpawn = true;
        firstHold = true;
        isClear = true;
        totalClearLines = 0;
        LineToClear = false;
    }
    List<int> partsBag = new List<int>();
    public void resetBag(int n)
    {
        for(int i = 0; i < n; i++)
        {
            partsBag.Add(i);
        }
    }
    public void Spawn()   
    {
        int spawnX;
        if (fliped)
        {
            spawnX = 1;
        }
        else {
            spawnX = (int)Mathf.Ceil(x / 2);
        }
        if (partsBag.Count > 1)
        {
            CurrentPiece=Instantiate(pieces[partsBag[nextPieceType]], new Vector3(spawnX, z + 1, Mathf.Ceil(y / 2)), Quaternion.identity);
            partsBag.RemoveAt(nextPieceType);
            nextPieceType = Random.Range(0, partsBag.Count);
        }
        else
        {
            CurrentPiece=Instantiate(pieces[partsBag[0]], new Vector3(spawnX, z + 1, Mathf.Ceil(y / 2)), Quaternion.identity);
            partsBag.RemoveAt(0);
            resetBag(pieces.Length);
        }

        ManageNextPiece();
    }
    void ResetClearLine()
    {
        LineToClear = false;
    }
    public static List<int> LineToClearHeights = new List<int>();
    public static bool LineToClear = false;
    public static void Calculateclearlines()
    {
        for (int i = 1; i < z; i++)
        {
            bool thereislinetoclear = true;
            for (int j = 1; j < x-1; j++)
            {
                for (int k = 1; k < y-1; k++)
                {
                    if (grid[j, i, k] == 0)
                    {
                        thereislinetoclear = false;
                        break;
                    }
                }
                if (!thereislinetoclear) { break; }
            }
            if (thereislinetoclear)
            {
                LineToClear = true;
                LineToClearHeights.Add(i);
            }
        }
    }
    public void EvaluateClearedLines()
    {
        int levelBonus = GameScoreLevelManager.currentLevel;
        if (spin) {
            gameScoreLevelManager.CurrentScore +=( GameScoreLevelManager.scoringTable["spin"] * levelBonus);
            switch (LineToClearHeights.Count)
            {
                case 1: LineSound.PlayOneShot(SpinSound, 0.7f); break;
                case 2: LineSound.PlayOneShot(SpinSound, 0.7f); break;
                case 3: LineSound.PlayOneShot(SpinSound, 0.7f); break;
            }
        }
        else
        {
            switch (LineToClearHeights.Count)
            {
                case 1: LineSound.PlayOneShot(OneLiner, 0.7f); gameScoreLevelManager.CurrentScore += (GameScoreLevelManager.scoringTable["1line"] * levelBonus); break;
                case 2: LineSound.PlayOneShot(DoubleLiner, 0.7f); gameScoreLevelManager.CurrentScore += (GameScoreLevelManager.scoringTable["2lines"] * levelBonus); break;
                case 3: LineSound.PlayOneShot(TripleLiner, 0.7f); gameScoreLevelManager.CurrentScore += (GameScoreLevelManager.scoringTable["3lines"] * levelBonus); break;
                case 4: LineSound.PlayOneShot(Tetris, 0.7f); gameScoreLevelManager.CurrentScore += (GameScoreLevelManager.scoringTable["tetris"] * levelBonus); break;
            }
        }
        spin = false;
    }
    public bool firstHold = true;
    static bool isClear = true;
    public static bool gameEnded = false;
    public static bool spin = false;
    public static bool fliped = false;
    public static int totalClearLines = 0;
    public static void IscleartoSpawn()
    {
        for (int i = 1; i < x-1; i++)
        {
            for (int j = 1; j < y-1; j++)
            {
                if (grid[i,z,j]==1){
                    isClear = false;
                    break;
                }
            }
            if (!isClear) { break; }
        }
    }
    GameObject nextPiece;
    public static bool oktoSpawn=true;
    void ManageNextPiece() {
        Destroy(nextPiece);
        nextPiece = Instantiate(pieces[partsBag[nextPieceType]], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        nextPiece.GetComponent<ControlPiece>().DisablePiece();
        int flippCoef = 1;
        if (fliped) { flippCoef = -1; } else { flippCoef = 1; }
        nextPiece.transform.parent = mainCamera.transform;
        nextPiece.transform.position = new Vector3(mainCamera.transform.position.x - 21 * flippCoef, mainCamera.transform.position.y - 2, mainCamera.transform.position.z + 8f * flippCoef);
    }
    void Update()
    {
        if (oktoSpawn)
        {
            IscleartoSpawn();
            if (isClear)
            {
                LineToClearHeights.Clear();
                Calculateclearlines();
                totalClearLines += LineToClearHeights.Count;
                EvaluateClearedLines();
                Spawn();
                Invoke("ResetClearLine", 0.1f);
                oktoSpawn = false;
            }
            else {
                gameEnded = true;
                GameOverCanvas.SetActive(true);
            }

            gameScoreLevelManager.ManageLevel();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (firstHold)
            {
                firstHold = false;
                IscleartoSpawn();
                if (isClear)
                {
                    Spawn();
                }
            }
        }
    }
}
