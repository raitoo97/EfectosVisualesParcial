using UnityEngine;
public class BounceCapsule : MonoBehaviour
{
    public Material material;
    [Header("Bounce Settings")]
    public float intensity;
    public float rotationIntensity;
    public float smooth;
    [SerializeField]private Rigidbody _rb;
    private Vector3 _currentBounce;
    void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        _currentBounce = Vector3.zero;
    }
    void Update()
    {
        if (_rb == null) return;
        Vector3 localVelocity =transform.InverseTransformDirection(_rb.velocity);
        Vector3 localAngular =transform.InverseTransformDirection(_rb.angularVelocity);
        Vector3 targetBounce = new Vector3(-localVelocity.x,-localAngular.y,-localVelocity.z);
        targetBounce.x *= intensity;
        targetBounce.z *= intensity;
        targetBounce.y *= rotationIntensity;
        _currentBounce = Vector3.Lerp(_currentBounce,targetBounce,Time.deltaTime * smooth);
        material.SetVector("_Bounce", _currentBounce);
    }
}