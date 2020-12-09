using UnityEngine;
using System.Collections;

public class FollowSpider : MonoBehaviour {
    public GameObject spiderBody;

    Vector3 offset;

    void Awake() 
    {
        offset = transform.position - spiderBody.transform.position;
    }

    void LateUpdate() 
    {
        transform.position = spiderBody.transform.position + offset;
    }
}