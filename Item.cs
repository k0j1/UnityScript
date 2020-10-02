using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject Item01 = null;
    public GameObject Item02 = null;
    public GameObject Item03 = null;
    public GameObject Item04 = null;
    public GameObject Item05 = null;
    public GameObject Item06 = null;
    public GameObject Item07 = null;
    public GameObject Item08 = null;
    public GameObject Item09 = null;
    public GameObject Item10 = null;

    public int ItemNum = 10;

    // Start is called before the first frame update
    void Start()
    {
        SetItem(Item01, ItemNum);
        SetItem(Item02, ItemNum);
        SetItem(Item03, ItemNum);
        SetItem(Item04, ItemNum);
        SetItem(Item05, ItemNum);
        SetItem(Item06, ItemNum);
        SetItem(Item07, ItemNum);
        SetItem(Item08, ItemNum);
        SetItem(Item09, ItemNum);
        SetItem(Item10, ItemNum);

        //StartCoroutine("LateSetItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LateSetItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void SetItem(GameObject instObj, int nNum)
    {
        // 例外処理
        if (!instObj) return;

        int nIndex = 0;
        for(; nIndex < nNum; nIndex++)
        {
            GameObject Item = Instantiate(instObj);
            Item.transform.position = new Vector3(Random.Range(-55.0f, 63.0f), 8, Random.Range(-64.0f, 84.0f));
            Item.SetActive(true);
        }
    }
}
