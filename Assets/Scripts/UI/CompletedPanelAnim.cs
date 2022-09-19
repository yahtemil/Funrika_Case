using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CompletedPanelAnim : MonoBehaviour
{
    public RectTransform[] DownToUpObjects;
    private void OnEnable()
    {
        foreach (RectTransform item in DownToUpObjects)
        {
            item.DOAnchorPosY(-1000f, 2f).From();
        }
    }
}
