using UnityEngine;

public class Ship : MonoBehaviour
{
    [field: SerializeField] private LayerMask layerMask;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Managers.GameManager.GameClear();
        }
    }
}
