using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteTriggerControl : MonoBehaviour
{
    [HideInInspector]
    public bool PuzzleActive = true;
    [HideInInspector]
    public bool ActiveSprite;
    [HideInInspector]
    public bool Placed;

    [HideInInspector]
    public int PosX;
    [HideInInspector]
    public int PosZ;

    public SpriteRenderer spriteRender;

    [HideInInspector]
    public Color firstColor;

    [HideInInspector]
    public CubeControl lastTriggerCube;
    [HideInInspector]
    public CubeControl TrueCube;

    public void CheckTriggerEnter(CubeControl cubeControl)
    {
        if (ActiveSprite)
        {
            if (!Placed)
            {
                lastTriggerCube = cubeControl;
                Placed = true;
                cubeControl.meshRenderer.material.DOColor(Color.green, 0.25f);
                cubeControl.PlacedSpriteTrigger = this;
                cubeControl.Placed = true;
                CubeAreaCreate.instance.AllListCube.Remove(cubeControl.listCube);
            }
        }
    }

    public void CheckTriggerExit(CubeControl cubeControl)
    {
        if (ActiveSprite)
        {
            if (lastTriggerCube == cubeControl)
            {
                lastTriggerCube = null;
                cubeControl.PlacedSpriteTrigger = null;
                cubeControl.Placed = false;
                cubeControl.meshRenderer.material.DOColor(cubeControl.listCube.selectColor, 0.25f);
                CubeAreaCreate.instance.AllListCube.Add(cubeControl.listCube);
                Placed = false;
            }
        }
    }
}
