using UnityEngine;
using System.Collections;

public class FollowSpider : MonoBehaviour {
    public GameObject spider;

    Vector3 offset;

    void Awake() 
    {
        offset = transform.position - spider.transform.position;
    }

    void LateUpdate() 
    {
        transform.position = spider.transform.position + offset;
        Debug.Log(spider.transform.position);
    }
}