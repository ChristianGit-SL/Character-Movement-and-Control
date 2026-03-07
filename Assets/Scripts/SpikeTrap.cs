using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject[] spikes;
    public float spikeUpY = 0.3f;
    public float spikeDownY = -0.3f;
    public float speed = 3f;
    public float stayUpDuration = 2f;

    private bool triggered = false;
    private float timer = 0f;
    private Vector3[] spikeDownPositions;
    private Vector3[] spikeUpPositions;

    void Start()
    {
        spikeDownPositions = new Vector3[spikes.Length];
        spikeUpPositions = new Vector3[spikes.Length];

        for (int i = 0; i < spikes.Length; i++)
        {
            spikeDownPositions[i] = spikes[i].transform.localPosition;
            spikeUpPositions[i] = new Vector3(
                spikes[i].transform.localPosition.x,
                spikeUpY,
                spikes[i].transform.localPosition.z
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            timer = stayUpDuration;
        }
    }

    void Update()
    {
        if (triggered)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                triggered = false;
            }
        }

        for (int i = 0; i < spikes.Length; i++)
        {
            Vector3 target = triggered ? spikeUpPositions[i] : spikeDownPositions[i];
            spikes[i].transform.localPosition = Vector3.MoveTowards(
                spikes[i].transform.localPosition,
                target,
                speed * Time.deltaTime
            );
        }
    }
}