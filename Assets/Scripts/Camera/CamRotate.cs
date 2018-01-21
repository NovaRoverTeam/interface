using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotateSpeed = 1f, scrollSpeed = 200f;
    public Transform pivot;

    public SphericalCoordinates sc;

    private void Start()
    {
        //we use the function SphericalCoordinates. It does what I want it to but it isn't commented, so I just made it up
        // the final two values specify the min and max elevation relative to the pivot position
        sc = new SphericalCoordinates(transform.position, 1f, 1f, 0f, Mathf.PI * 2f, -1 * Mathf.PI / 4f, Mathf.PI / 4f);
        // Initialize position
        transform.position = sc.toCartesian + pivot.position;
        transform.eulerAngles = pivot.eulerAngles;

    }

    void Update()
    {

            float kh, kv, mh, mv, h, v;
        // I have manually inverted the controls because I think it makes it more intuitive
        kh = -1 * Input.GetAxis("Horizontal");
        kv = -1 * Input.GetAxis("Vertical");

        bool anyMouseButton = Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2);
        mh = anyMouseButton ? Input.GetAxis("Mouse X") : 0f;
        mv = anyMouseButton ? Input.GetAxis("Mouse Y") : 0f;

        h = kh * kh > mh * mh ? kh : mh;
        v = kv * kv > mv * mv ? kv : mv;

        if (h * h > .1f || v * v > .1f)
            transform.position = sc.Rotate(h * rotateSpeed * Time.deltaTime, v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;

        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if (sw * sw > Mathf.Epsilon)
            transform.position = sc.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian + pivot.position;

       transform.LookAt(pivot.position);
    }
}