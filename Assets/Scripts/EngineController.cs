using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineController : MonoBehaviour
{
    public GameObject enginePuzzle;
    public GameObject engineOriginal;

    [HeaderAttribute("Part of EnginePuzzle")]
    public List<GameObject> puzzleObjMoved;
    public List<GameObject> puzzleObjOri;

    [HeaderAttribute("Part of EngineOriginal")]
    public List<GameObject> ObjMoved;
    public List<GameObject> ObjOri;

    [HeaderAttribute("AudioSetting")]
    private AudioSource audioSource;
    public AudioClip sound_Success;
    public AudioClip sound_Fail;
    [Range(0.0f, 1.0f)]
    public float successVolume;
    [Range(0.0f, 1.0f)]
    public float failVolume;

    public static int step = 0;
    public float snapDistance;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Button01OnClick()
    {
        if (enginePuzzle.activeSelf == false)
        {
            enginePuzzle.SetActive(true);
            engineOriginal.SetActive(false);
        }
    }
    public void Button02OnClick()
    {
        if (enginePuzzle.activeSelf == true)
        {
            enginePuzzle.SetActive(false);
            engineOriginal.SetActive(true);
        }
    }

    public void ResetEngine()
    {
        if (enginePuzzle.activeSelf == true)
        {
            Debug.Log("ResetEnginePuzzle");
            for (int i = 0; i < puzzleObjMoved.Count; i++)
            {
                puzzleObjMoved[i].transform.position = puzzleObjOri[i].transform.position;
                puzzleObjMoved[i].transform.rotation = puzzleObjOri[i].transform.rotation;
                puzzleObjMoved[i].transform.localScale = puzzleObjOri[i].transform.localScale;
            }
            foreach (GameObject i in puzzleObjMoved)
            {
                i.GetComponent<BoxCollider>().enabled = true;
            }
            step = 0;
        }
        else
        {
            Debug.Log("ResetEngineOriginal");
            for (int i = 0; i < ObjMoved.Count; i++)
            {
                ObjMoved[i].transform.position = ObjOri[i].transform.position;
                ObjMoved[i].transform.rotation = ObjOri[i].transform.rotation;
                ObjMoved[i].transform.localScale = ObjOri[i].transform.localScale;
            }
        }
    }

    public void PuzzleDetermine()
    {
        Debug.Log("PuzzleDetermine: Step " + (step + 1));
        if (enginePuzzle.activeSelf == true)
        {
            if (Vector3.Distance(puzzleObjMoved[step].transform.position, ObjOri[step].transform.position) < snapDistance)
            {
                SnapToCorrectPos();
                audioSource.volume = successVolume;
                audioSource.PlayOneShot(sound_Success);
            }
            else if (Vector3.Distance(puzzleObjMoved[step].transform.position, ObjOri[step].transform.position) >= snapDistance)
            {
                ResetCompExceptFinish();
                audioSource.volume = failVolume;
                audioSource.PlayOneShot(sound_Fail);
            }
        }
    }

    void SnapToCorrectPos()
    {
        Debug.Log("SnapToCorrectPos");
        puzzleObjMoved[step].transform.position = ObjOri[step].transform.position;
        puzzleObjMoved[step].transform.rotation = ObjOri[step].transform.rotation;
        puzzleObjMoved[step].transform.localScale = ObjOri[step].transform.localScale;
        puzzleObjMoved[step].GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Name: " + puzzleObjMoved[step].name);
        if (step < puzzleObjMoved.Count)
        {
            step++;
        }
        if (step == puzzleObjMoved.Count)
        {
            Debug.Log("Success!!!");
        }
    }

    void ResetCompExceptFinish()
    {
        Debug.Log("ResetCompExceptFinish");

        for (int i = step; i < puzzleObjMoved.Count; i++)
        {
            puzzleObjMoved[i].transform.position = puzzleObjOri[i].transform.position;
            puzzleObjMoved[i].transform.rotation = puzzleObjOri[i].transform.rotation;
            puzzleObjMoved[i].transform.localScale = puzzleObjOri[i].transform.localScale;
        }
    }
}
