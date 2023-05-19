using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsManager : MonoBehaviour
{

    public List<GameObject> TabsList = new List<GameObject>();
    public GameObject activeTab;

    public void ChangeTab(GameObject nextTab)
    {
        foreach (GameObject tab in TabsList)
        {
            if(tab!= nextTab)
            {
                tab.SetActive(false);
            }
            else
            {
                tab.SetActive(true);
                activeTab = nextTab;
            }
        }
        
    }
}
