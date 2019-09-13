using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelpSetName
{

    [MenuItem("MyTools/SetMapName")]
    private static void SetMapName()
    {
        GameObject Eniroment = GameObject.Find("Eniroment");

        for (int i = 0; i < 6; i++)
        {
            for (int j=0;j<6;j++)
            {
                Eniroment.transform.GetChild(i).GetChild(j).name=i.ToString()+"_"+j.ToString();
            }
        }


    }

}

