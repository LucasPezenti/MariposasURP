using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darknessMovement : MonoBehaviour
{
    public float Velocidade = .005f;
    public Transform GameObjectSeguido;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObjectSeguido != null)
        {
            this.transform.position = GameObjectSeguido.position;
        }
    }
}
