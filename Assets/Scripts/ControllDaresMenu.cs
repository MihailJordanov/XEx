using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllDaresMenu : MonoBehaviour
{
    Dare[] arrDareForOneClassic; 
    string[] arr = { "123", "avb", "car" };
    int curId = 0;

    [SerializeField]
    private TMP_Text curDareText;
    [SerializeField]
    private TMP_Text curDareId;

    private void Update()
    {
        
    }

    public void Next()
    {
        if (curId == (arr.Count() - 1))
            curId = 0;
        else
            curId++;

        curDareText.text = arr[curId];
        curDareId.text = curId.ToString();
    }

    public void Prev()
    {
        if (curId == 0)
            curId = (arr.Count() - 1);
        else
            curId--;

        curDareText.text = arr[curId];
        curDareId.text = curId.ToString();
    }


}
