using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * 20;
    }
}
