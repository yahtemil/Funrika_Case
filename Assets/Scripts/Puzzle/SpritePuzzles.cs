using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpritePuzzles : MonoBehaviour
{
    public SelectCubeList selectCubeList;

    public List<CubeControl> AllPuzzles = new List<CubeControl>();

    public List<ListCube> addedListCubes = new List<ListCube>();
    public List<ListCube> AllListCubes = new List<ListCube>();

    public void CreatePuzzle()
    {
        StartCoroutine(CreatePuzzleTiming());
    }

    IEnumerator CreatePuzzleTiming()
    {
        int randomValue = Random.Range(2, 5);
        GameObject g = new GameObject();
        g.transform.parent = transform;
        ListCube newListCube = new ListCube();
        newListCube.parentObject = g;
        newListCube.spritePuzzles = this;
        AllListCubes.Add(newListCube);
        CubeAreaCreate.instance.AllListCube.Add(newListCube);

        int counter = 0;
        for (int i = 0; i < selectCubeList._allSpriteTriggerControl.Count; i++)
        {
            List<SpriteTriggerControl> selects = selectCubeList.GetListSpriteTriggerControl();
            if (selects.Count == 0 && counter == 0)
            {
                AllListCubes.Remove(newListCube);
                CubeAreaCreate.instance.AllListCube.Remove(newListCube);
                newListCube = null;
                Destroy(g);
                break;
            }
            for (int j = 0; j < selects.Count; j++)
            {
                CubeControl cubeControl = selects[j].GetComponentInChildren<CubeControl>();
                cubeControl.transform.parent = g.transform;
                AllPuzzles.Add(cubeControl);
                newListCube.CubeList.Add(cubeControl);
                counter++;
            }
            yield return new WaitForSeconds(0.001f);            
            if (counter >= randomValue)
            {
                break;
            }
        }
        
        yield return new WaitForSeconds(0.001f);
        if (selectCubeList._allSpriteTriggerControl.Count > 0)
        {
            StartCoroutine(CreatePuzzleTiming());
        }
        else
        {

        }
        yield return new WaitForSeconds(0.05f);
        if (newListCube != null)
        {
            newListCube.LocalPositionChange();
        }
    }

    public bool TrueAreaControl()
    {
        bool trueControl = true;
        for (int i = 0; i < AllPuzzles.Count; i++)
        {
            if (AllPuzzles[i].Placed == false)
            {
                trueControl = false;
            }
        }
        return trueControl;
    }
}