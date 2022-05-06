using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;
using Mosframe;

public class menuScrolls : MonoBehaviour
{
    public enum eState
    {
        Upg = 0,
        Equip,
        Shop,
    }

    [SerializeField] DynamicVScrollView[] _scrs;

    public void Open(eState state)
    {
        int iState = (int)state;
        gameObject.SetActive(true);
        for (int i = 0; i < 3; ++i)
        {
            _scrs[i].gameObject.SetActive(i == iState);
        }
        switch (state)
        {
            case eState.Upg:
             //   _scrs[iState].totalItemCount = upgTables.max;
                break;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
