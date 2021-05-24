using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhysicsNObjects : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> obj;
    [SerializeField] private Rigidbody rigSphere;
    [SerializeField] private float G = 9.8f;
    private Vector3 r;
    private float d;
    private RaycastHit hit;

    void Start()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].useGravity = false;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                var sphere = Instantiate(rigSphere, hit.point + Vector3.back * 0.5f, Quaternion.identity);
                obj.Add(sphere);
                sphere.useGravity = false;
                sphere.mass = Random.Range(1, 5);
            }
        }
        for (int i = 0; i < obj.Count; i++)
        {
            for (int j = i+1; j < obj.Count; j++)
            {
                if (j != i)
                {
                    r = obj[i].position - obj[j].position;
                    d = r.magnitude;
                    r.Normalize();
                    obj[i].AddRelativeForce(((r * G * obj[i].mass) / Mathf.Abs(d * d * d)), ForceMode.Force);
                }
            }
        }
    }
}
