using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private float zoomAmount;

  private bool canZoomIn = true;
  private bool canZoomOut = true;
  private bool lerping;

  private void Update()
  {
    if (Physics.OverlapSphere(transform.position, 0.5f).Length != 0)
    {
      canZoomIn = false;
    }

    if (zoomAmount <= -4 && zoomAmount <= 0)
    {
      canZoomOut = false;
    }

    zoomAmount = Mathf.Clamp(zoomAmount, -4, 0);

    /*ZoomCamera();*/
    MoveCamera();
    /*RotateCamera();*/
  }

  private void RotateCamera()
  {
    if (Input.GetMouseButtonDown(2))
    {
      Cursor.visible = false;
    }

    if (Input.GetMouseButtonUp(2))
    {
      Cursor.visible = true;
    }

    if (Input.GetMouseButton(2))
    {
      transform.RotateAround(transform.position, Vector3.up, -Input.GetAxis("Mouse X") * 135.0f * Time.deltaTime);
    }
  }

  private void ZoomCamera()
  {
    float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");

    if (scrollWheelDelta != 0)
    {
      Vector3 direction = transform.forward * scrollWheelDelta;

      if (!canZoomIn && direction.z > 0)
      {
        return;
      }

      if (!canZoomOut && direction.z < 0)
      {
        return;
      }

      canZoomIn = true;
      canZoomOut = true;

      zoomAmount += direction.z * 1.5f;

      if (lerping)
      {
        StopAllCoroutines();
      }

      StartCoroutine(ZoomLerp(transform.position + direction * 40));
    }
  }

  private void MoveCamera()
  {
    float vertical = Input.GetAxis("Vertical");
    float horizontal = Input.GetAxis("Horizontal");

    if (vertical != 0 || horizontal != 0)
    {
      Vector3 direction = Quaternion.identity * transform.TransformDirection(Vector3.forward) * vertical * 0.25f;
      direction.y = 0;

      transform.position += direction;
      transform.position += transform.right * horizontal * 0.25f;
    }
  }

  private IEnumerator ZoomLerp(Vector3 target)
  {
    lerping = true;

    while (Vector3.SqrMagnitude(transform.position - target) > 0.5f)
    {
      transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 4);

      yield return new WaitForFixedUpdate();
    }

    lerping = false;
  }
}