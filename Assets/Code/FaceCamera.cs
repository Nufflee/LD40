using UnityEngine;

public class FaceCamera : MonoBehaviour
{
  #region Events

  public void Update()
  {
    if (Camera.main == null)
    {
      // TODO Test only why is this happening ..
      return;
    }

    transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
  }

  #endregion
}