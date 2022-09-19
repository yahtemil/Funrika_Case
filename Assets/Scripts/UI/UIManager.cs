using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI LevelText;
    public GameObject CompletedPanel;
    public GameObject HelpObject;

    public GameObject TutorialHand;

    private void Start()
    {
        instance = this;
        Invoke("OpenTutorial", 0.5f);
        LevelText.text = "Level " + LevelManager.instance.LevelValue.ToString();
    }

    public void OpenTutorial()
    {
        if (LevelManager.instance.LevelValue != 1)
        {
            return;
        }
        ListCube listCube = CubeAreaCreate.instance.GetListCube();
        if (listCube != null)
        {
            TutorialHand.SetActive(true);
            Vector3 firstPosition = Camera.main.WorldToScreenPoint(listCube.parentObject.gameObject.transform.position);
            Vector3 secondPosition = Camera.main.WorldToScreenPoint(listCube.CubeList[0].TrueAreaSprite.transform.position);
            TutorialHand.transform.DOKill();
            TutorialHand.transform.position = firstPosition;
            foreach (var item in listCube.CubeList)
            {
                item.TrueAreaSprite.spriteRender.DOColor(Color.yellow, 1f).SetLoops(-1,LoopType.Restart).From();
            }
            TutorialHand.transform.DOMove(secondPosition, 2f).SetLoops(-1,LoopType.Restart).OnUpdate(() => 
            {
                if (listCube.Placed)
                {
                    foreach (var item in listCube.CubeList)
                    {
                        item.TrueAreaSprite.spriteRender.DOKill();
                        item.TrueAreaSprite.spriteRender.color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 120f / 255f);
                    }
                    OpenTutorial();
                }
            });
        }
        else
        {
            TutorialHand.SetActive(false);
        }
       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
