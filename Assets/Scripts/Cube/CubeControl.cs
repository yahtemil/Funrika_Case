using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeControl : MonoBehaviour
{
    [HideInInspector]
    public SpriteTriggerControl PlacedSpriteTrigger;
    [HideInInspector]
    public bool Placed;
    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    [HideInInspector]
    public ListCube listCube;
    [HideInInspector]
    public SpriteTriggerControl TrueAreaSprite;

    private void OnMouseDown()
    {
        listCube.CubeDownControl();
    }

    private void OnMouseDrag()
    {
        listCube.CubeDragControl();
    }

    private void OnMouseUp()
    {
        listCube.CubeUpControl();
    }

    public void PlacedOperations(Vector3 pos,float moveTime)
    {
        meshRenderer.material.DOColor(listCube.selectColor, 0.25f);
        transform.DOMove(pos, moveTime).OnComplete(() => 
        {
            listCube.Placed = true;
            meshRenderer.material.DOColor(listCube.selectColor, 0.2f);
            CubeAreaCreate.instance.AllCubeTrueAreaControl();
        });
        
    }
}
