using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBackground : MonoBehaviour
{
    [SerializeField] private float speed_x = 1;
    [SerializeField] private float speed_z = 1;
    [SerializeField] private float factor_x = 1;
    [SerializeField] private float factor_z = 0.2f;
    private float offset_x;
    private float offset_z;

    [SerializeField] private bool isParent = false;

    private void Start()
    {
        if (!isParent)
        {
            // speed_x = Random.Range(0.1f, 0.15f);
            speed_z = Random.Range(0.1f, 0.15f);
        }
        offset_x = this.transform.position.x;
        offset_z = this.transform.position.z;
    }
    void Update()
    {
        if (isParent)
        {
            float x = (Mathf.Sin(Time.time * speed_x) * factor_x) + (offset_x + Camera.main.transform.position.x);
            float z = (Mathf.Sin(Time.time * speed_z) * factor_z) + (offset_z + Camera.main.transform.position.z);
            // float z = (offset_z + Camera.main.transform.position.z);
            transform.position = new Vector3(x, this.transform.position.y, z);
        }
        else
        {
            float x = (Mathf.Sin(Time.time * speed_x) * factor_x) + (offset_x + this.transform.parent.position.x);
            float z = (Mathf.Sin(Time.time * speed_z) * factor_z) + (offset_z + this.transform.parent.position.z);
            transform.position = new Vector3(x, this.transform.position.y, z);

        }
    }
}
