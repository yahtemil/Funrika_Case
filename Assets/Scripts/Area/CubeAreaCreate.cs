using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAreaCreate : MonoBehaviour
{
    public static CubeAreaCreate instance;
    public List<SpriteTriggerControl> AllAreaSprites;
    public List<CubeControl> AllCubes;

    public ReadWrite readWrite;

    [HideInInspector]
    public List<string> letters = new List<string>();

    [SerializeField]
    public List<Letters> LettersList = new List<Letters>();

    [HideInInspector]
    public List<Letters> SelectList = new List<Letters>();

    [HideInInspector]
    public List<SelectCubeList> AllSelectCubeList = new List<SelectCubeList>();

    [SerializeField]
    public List<Color> RandomColors = new List<Color>();

    [SerializeField]
    public List<ListCube> AllListCube = new List<ListCube>();

    int CubeCounter;

    public float DealMaxPosX = 0;
    public float DealMinPosZ = -13f;
    [HideInInspector]
    public float DealMinPosZReserve = 0f;

    [HideInInspector]
    public SelectCubeList selectCubeList;

    void Awake()
    {
        instance = this;
        int counter = 0;
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                AllAreaSprites[counter].transform.localPosition = new Vector3(i, 0.1f, j);
                AllAreaSprites[counter].PosX = i;
                AllAreaSprites[counter].PosZ = j;
                AllAreaSprites[counter].spriteRender.color = new Color(100f / 255f, 100f / 255f, 100f / 255f, 120f / 255f);
                counter++;
            }            
        }

        //letters.Add("C");
        //letters.Add("A");
        //letters.Add("N");

        Invoke("CreateAreaBefore", 0.2f);

    }

    public void CreateAreaBefore()
    {
        int counterLimit = 0;
        if (LevelManager.instance.LevelValue == 1)
        {
            counterLimit = 1;
        }
        else if (LevelManager.instance.LevelValue == 2)
        {
            counterLimit = 2;
        }
        else
        {
            counterLimit = 3;
        }
        int letterCount = PlayerPrefs.GetInt("LetterCount", 0);
        for (int i = 0; i < counterLimit; i++)
        {
            Letters _letters = LettersList.Find(x => x.LetterName == letters[letterCount]);
            if (_letters != null)
            {
                SelectList.Add(_letters);
            }
            else
            {
                SelectList.Add(LettersList.Find(x => x.LetterName == "C"));
            }
            
            letterCount++;
            if (letterCount >= letters.Count && letterCount != 0)
            {
                letterCount = 0;
            }
        }
        PlayerPrefs.SetInt("LetterCount", letterCount);
        CreateArea();
    }


    public void CreateArea()
    {
        int selectValue = SelectList.Count;
        int firstPosX = 0;
        if (selectValue == 1)
        {
            firstPosX = 5;
        }
        else if (selectValue == 2)
        {
            firstPosX = 3;
        }

        for (int i = 0; i < selectValue; i++)
        {
            int CountValue = SelectList[i].letterPoints.Length;
            List<SpriteTriggerControl> AllSpritTriggerControl = new List<SpriteTriggerControl>();
            for (int j = 0; j < CountValue; j++)
            {
                Vector3 localPos = new Vector3(firstPosX + (i * 5) + SelectList[i].letterPoints[j].PosX, 0.5f, SelectList[i].letterPoints[j].PosY);
                SpriteTriggerControl spriteTriggerControl = AllAreaSprites.Find(x => x.PosX == localPos.x && x.PosZ == localPos.z);
                spriteTriggerControl.ActiveSprite = true;
                spriteTriggerControl.spriteRender.color = new Color(200f/255f, 200f / 255f, 200f / 255f, 120f / 255f);
                AllCubes[CubeCounter].transform.parent = spriteTriggerControl.transform;
                AllCubes[CubeCounter].transform.localPosition = Vector3.zero;
                AllCubes[CubeCounter].TrueAreaSprite = spriteTriggerControl;
                spriteTriggerControl.TrueCube = AllCubes[CubeCounter];

                AllSpritTriggerControl.Add(spriteTriggerControl);
                CubeCounter++;
            }
            selectCubeList = new SelectCubeList(AllSpritTriggerControl);
            selectCubeList.Start();
            AllSelectCubeList.Add(selectCubeList);
        }
    }

    public void AllCubeTrueAreaControl()
    {
        bool LevelCompleted = true;
        foreach (SelectCubeList item in AllSelectCubeList)
        {
            if (item.TrueAreaControl() == false)
            {
                LevelCompleted = false;
            }
        }
        if (LevelCompleted)
        {
            StartCoroutine(FinishAnimTiming());
            LevelManager.instance.LevelCompleted();
            Debug.Log("Level Completed");
        }
    }

    public IEnumerator FinishAnimTiming()
    {
        foreach (SelectCubeList item in AllSelectCubeList)
        {
            foreach (SpritePuzzles spritePuzzle in item.spritePuzzleList)
            {
                foreach (ListCube listCube in spritePuzzle.addedListCubes)
                {
                    StartCoroutine(listCube.AnimTiming());
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    public Color GetColor()
    {
        int randomValue = Random.Range(0, RandomColors.Count);
        Color color = RandomColors[randomValue];
        RandomColors.Remove(color);
        return color;
    }

    public ListCube GetListCube()
    {
        for (int i = 0; i < instance.AllSelectCubeList.Count; i++)
        {
            for (int j = 0; j < instance.AllSelectCubeList[i].spritePuzzleList.Count; j++)
            {
                for (int k = 0; k < instance.AllSelectCubeList[i].spritePuzzleList[j].AllListCubes.Count; k++)
                {
                    ListCube listCube = instance.AllSelectCubeList[i].spritePuzzleList[j].AllListCubes[k];
                    if (listCube.Placed == false)
                    {
                        return listCube;
                    }
                }
            }
        }
        return null;
    }
}

