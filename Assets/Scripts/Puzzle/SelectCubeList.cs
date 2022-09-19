using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectCubeList
{
    [HideInInspector]
    public List<SpriteTriggerControl> _allSpriteTriggerControl;
    public SelectCubeList(List<SpriteTriggerControl> allSpriteTriggerControl)
    {
        _allSpriteTriggerControl = allSpriteTriggerControl;
    }

    public List<SpritePuzzles> spritePuzzleList = new List<SpritePuzzles>();

    SpriteTriggerControl _lastSprite;

    public void Start()
    {
        CreatePuzzle();
    }

    public void CreatePuzzle()
    {
        GameObject parentGameObject = new GameObject();
        parentGameObject.name = "Puzzle";
        SpritePuzzles newSpritePuzzles = parentGameObject.AddComponent<SpritePuzzles>();
        newSpritePuzzles.selectCubeList = this;
        newSpritePuzzles.CreatePuzzle();

        spritePuzzleList.Add(newSpritePuzzles);
    }

    public bool TrueAreaControl()
    {
        bool AllTrueArea = true;
        for (int i = 0; i < spritePuzzleList.Count; i++)
        {
            if (spritePuzzleList[i].TrueAreaControl() == false)
                AllTrueArea = false;
        }
        return AllTrueArea;
    }

    public SpriteTriggerControl GetLastSpriteTrigger()
    {
        if (_lastSprite != null)
        {
            return _lastSprite;
        }
        else
        {
            return _allSpriteTriggerControl[0];
        }
    }

    public List<SpriteTriggerControl> GetListSpriteTriggerControl()
    {
        List<SpriteTriggerControl> selects = new List<SpriteTriggerControl>();
        bool lastSpriteChange = false;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (((i == -1 || i == 1) && j == 0) || i == 0)
                {
                    if (_allSpriteTriggerControl.Count <= 0)
                    {
                        break;
                    }
                    SpriteTriggerControl selectSprite = _allSpriteTriggerControl.Find(x => x.PosX == (GetLastSpriteTrigger().PosX + i) && x.PosZ == (GetLastSpriteTrigger().PosZ + j) && x.ActiveSprite && x.PuzzleActive);
                    if (selectSprite != null)
                    {
                        if (_lastSprite != selectSprite)
                        {                            
                            _lastSprite = selectSprite;
                            lastSpriteChange = true;
                        }
                        selectSprite.PuzzleActive = false;
                        selects.Add(selectSprite);
                        _allSpriteTriggerControl.Remove(selectSprite);
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        if (!lastSpriteChange)
        {
            _lastSprite = null;
        }
        return selects;
    }
}
