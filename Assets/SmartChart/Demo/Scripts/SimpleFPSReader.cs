using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ToucanSystems;

/// <summary>
/// Test script for pushing FPS values onto the chart.
/// </summary>
public class SimpleFPSReader : MonoBehaviour
{
    [SerializeField]
    private Text debugText;
    [SerializeField]
    private ChartDataMonitor monitor;

    private void OnEnable()
    {
        StartCoroutine(DataFlowCoroutine());
    }

    private IEnumerator DataFlowCoroutine()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            monitor.monitorValue = 1.0f / Time.deltaTime;
            debugText.text = "FPS: " + monitor.monitorValue.ToString("0.00");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
