using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPuzzle : MonoBehaviour
{    
    public int clickCounter = 0;
    public bool active;

    public void Click()
    {
        if (active)
        {
            return;
        }
        active = true;
        Invoke("AgainActive", 0.55f);
        clickCounter++;
        if (clickCounter <= 2)
        {
            if (clickCounter == 2)
            {
                GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);
            }
            HelpPlace();
        }
    }

    public void AgainActive()
    {
        active = false;
    }

    public void HelpPlace()
    {
        int RandomValue = Random.Range(0, CubeAreaCreate.instance.AllListCube.Count);

        ListCube selectListCube = CubeAreaCreate.instance.AllListCube[RandomValue];
        if (selectListCube != null)
        {
            while (selectListCube.Placed)
            {
                RandomValue = Random.Range(0, CubeAreaCreate.instance.AllListCube.Count);
                selectListCube = CubeAreaCreate.instance.AllListCube[RandomValue];
            }
            selectListCube?.HelpPuzzleControl();
        }
    }

}
