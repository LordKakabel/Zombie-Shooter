using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material hit1;
    public Material hit2;
    public int HP = 3;

    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        HP--;
        if (HP == 2)
            renderer.material = hit1;
        if (HP == 1)
            renderer.material = hit2;
        if (HP == 0)
            Destroy(gameObject);
    }
}
