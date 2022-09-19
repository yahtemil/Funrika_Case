using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class ListCube
{
    public SpritePuzzles spritePuzzles;
    public GameObject parentObject;
    public List<CubeControl> CubeList = new List<CubeControl>();

    public bool Placed;

    float firstMinX = 20f;
    float firstMinZ = 20f;

    float DealMaxX = 0f;
    float DealMinZ = 0f;

    public Color selectColor;

    public Vector3 firstPos;

    public void ComeBackPosition()
    {
        ColliderSizeChange(Vector3.one);
        parentObject.transform.DOMove(firstPos, 0.5f).OnComplete(() => Placed = false);
        CubePositionChange();
    }

    public void CubeDragControl()
    {
        Vector3 pos = Input.mousePosition;
        pos.z += Camera.main.transform.position.y;
        Vector3 poss = Camera.main.ScreenToWorldPoint(pos);
        poss.z += 5;
        parentObject.transform.DOMove(poss, 0.25f);
    }

    public void CubeDownControl()
    {
        CubePositionChange();
        ColliderSizeChange(Vector3.one * 0.75f);
        spritePuzzles.addedListCubes.Remove(this);
        Placed = false;
    }

    public void CubeUpControl()
    {
        ColliderSizeChange(Vector3.one);
        bool Placed = true;
        for (int i = 0; i < CubeList.Count; i++)
        {
            if (CubeList[i].Placed == false)
            {
                Placed = false;
            }
        }
        if (Placed)
        {            
            CubePlaced();
        }
        else
        {
            ComeBackPosition();
        }
    }

    public void HelpPuzzleControl()
    {
        ColliderSizeChange(Vector3.one * 0.5f);
        spritePuzzles.addedListCubes.Add(this);
        foreach (var cube in CubeList)
        {
            if (cube.TrueAreaSprite.Placed)
            {
                cube.TrueAreaSprite.lastTriggerCube.listCube.ComeBackPosition();
            }
            cube.PlacedOperations(cube.TrueAreaSprite.transform.position,0.5f);
        }
    }

    public void CubePlaced()
    {
        spritePuzzles.addedListCubes.Add(this);
        for (int i = 0; i < CubeList.Count; i++)
        {
            CubeList[i].PlacedOperations(CubeList[i].PlacedSpriteTrigger.transform.position,0.2f);
        }

        Placed = true;
    }

    public void ColliderSizeChange(Vector3 size)
    {
        for (int i = 0; i < CubeList.Count; i++)
        {
            CubeList[i].collider.size = size;
        }
    }

    public IEnumerator AnimTiming()
    {
        for (int i = 0; i < CubeList.Count; i++)
        {
            CubeList[i].transform.DOLocalMoveY(1f, 0.25f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() => CubeList[i].transform.DOLocalMoveY(0.1f, 0.25f));
            CubeList[i].meshRenderer.material.DOColor(Color.green, 1.75f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void LocalPositionChange()
    {
        selectColor = CubeAreaCreate.instance.GetColor();

        CubePositionChange();
        ParentPositionChange();
        firstPos = parentObject.transform.position;

    }

    public void CubePositionChange()
    {
        firstMinX = CubeList[0].transform.localPosition.x;
        firstMinZ = CubeList[0].transform.localPosition.z;
        CubeList[0].transform.localPosition = new Vector3(0f, 0.1f, 0f);
        CubeOptions(CubeList[0]);

        for (int i = 1; i < CubeList.Count; i++)
        {
            Vector3 changeLocalPosition = CubeList[i].transform.localPosition;
            changeLocalPosition.x -= firstMinX;
            changeLocalPosition.z -= firstMinZ;
            CubeList[i].transform.localPosition = changeLocalPosition;
            CubeOptions(CubeList[i]);

            if (DealMaxX < changeLocalPosition.x)
            {
                DealMaxX = changeLocalPosition.x;
            }
            if (DealMinZ > changeLocalPosition.z)
            {
                DealMinZ = changeLocalPosition.z;
            }
        }
    }

    public void ParentPositionChange()
    {
        parentObject.transform.position = new Vector3(CubeAreaCreate.instance.DealMaxPosX, 0f, CubeAreaCreate.instance.DealMinPosZ);

        CubeAreaCreate.instance.DealMaxPosX += (Mathf.Abs(DealMaxX) + 2);
        if (CubeAreaCreate.instance.DealMaxPosX > 13f)
        {
            CubeAreaCreate.instance.DealMaxPosX = 1;
            CubeAreaCreate.instance.DealMinPosZ += CubeAreaCreate.instance.DealMinPosZReserve;
            CubeAreaCreate.instance.DealMinPosZReserve = 0f;
        }
        else
        {
            if (CubeAreaCreate.instance.DealMinPosZReserve > DealMinZ - 5)
            {
                CubeAreaCreate.instance.DealMinPosZReserve = DealMinZ - 5;
            }
        }
    }

    public void CubeOptions(CubeControl cubeControl)
    {
        cubeControl.meshRenderer.material.color = selectColor;
        cubeControl.meshRenderer.enabled = true;
        cubeControl.collider.enabled = true;
        cubeControl.listCube = this;
    }
}
