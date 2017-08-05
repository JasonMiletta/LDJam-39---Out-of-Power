using UnityEngine;
 
[ExecuteInEditMode]
public class DynamicScale : MonoBehaviour
{
    public float stretchMagnitude = 2.0f;

    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 delta = transform.position - lastPosition;
        transform.localRotation = Quaternion.LookRotation(delta + Vector3.forward * 0.001f);
        float l = 1f + delta.magnitude;
        float wh = Mathf.Sqrt(1f / l);
        transform.localScale = new Vector3(wh, wh, l);

        lastPosition = transform.position;
    }
}