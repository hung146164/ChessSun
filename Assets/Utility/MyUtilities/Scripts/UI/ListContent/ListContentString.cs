using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContentString : MonoBehaviour
{
    public ContentString contentStringPrefab;

    private Queue<ContentString> contentPool;

    private List<ContentString> contentList;

    public ContentString GetContentString()
    {
        if(contentPool==null)
            contentPool = new Queue<ContentString>();
        if (contentPool.TryDequeue(out ContentString ct))
        {
            return ct;
        }

        return Instantiate(contentStringPrefab); 
    }

    public void AddItem(string name)
    {
        ContentString ct = GetContentString();
        ct.ChangeText(name);
        ct.gameObject.SetActive(true);
        ct.transform.SetParent(transform);
        ct.transform.localScale = Vector3.one;

    }
    public void RemoveItem(string name)
    {
        if(contentList==null) contentList = new List<ContentString>();
        foreach (var item in contentList)
        {
            if (item.name == name)
            {
                contentPool.Enqueue(item);
                contentList.Remove(item);
                item.gameObject.SetActive(false);
            }
        }

    }

    public void ClearListContent()
    {
        if (contentList == null) return;
        foreach (var item in contentList)
        {
            contentPool.Enqueue(item);
        }
        contentList.Clear();
    }

    public void UpdateListContent(List<string> content)
    {
        foreach(var item in content)
        {
            AddItem(item);
        }
    }
}