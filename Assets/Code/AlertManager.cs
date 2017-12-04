using System.Collections;
using TMPro;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
  private TextMeshProUGUI alertText;

  private void Start()
  {
    alertText = GameObject.Find("AlertText").GetComponent<TextMeshProUGUI>();
    alertText.gameObject.SetActive(false);
  }

  public void Alert(string message)
  {
    alertText.text = message;
    alertText.gameObject.SetActive(true);

    StartCoroutine(ShowAlertCoroutine());
  }

  private IEnumerator ShowAlertCoroutine()
  {
    while (alertText.GetComponent<TextMeshProUGUI>().color.a <= 1)
    {
      alertText.GetComponent<TextMeshProUGUI>().color += new Color(0.0f, 0.0f, 0.0f, 0.1f);

      yield return null;
    }

    yield return new WaitForSeconds(0.7f);

    while (alertText.GetComponent<TextMeshProUGUI>().color.a > 0)
    {
      alertText.GetComponent<TextMeshProUGUI>().color -= new Color(0.0f, 0.0f, 0.0f, 0.1f);

      yield return null;
    }

    alertText.gameObject.SetActive(false);
  }
}