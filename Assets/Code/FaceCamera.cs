using UnityEngine;

public class FaceCamera : MonoBehaviour
{

  public void Update()
  {
    if (Camera.main == null)
    {
      return;
    }

    transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
  }
}