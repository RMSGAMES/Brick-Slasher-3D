using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    [SerializeField] private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField] private float sensitivityX = 15f;
    [SerializeField] private float sensitivityY = 15f;

    [SerializeField] private float minimumX = 0f;
    [SerializeField] private float maximumX = 35f;

    [SerializeField] private float minimumY = 0f;
    [SerializeField] private float maximumY = 35f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";

    private void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    private void Update ()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
            rotationX -= Input.GetAxis(mouseX) * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            rotationY += Input.GetAxis(mouseY) * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, -rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
            rotationX -= Input.GetAxis(mouseX) * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -rotationX, 0);
		}
		else
		{
			rotationY += Input.GetAxis(mouseY) * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}