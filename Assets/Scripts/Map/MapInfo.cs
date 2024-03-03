using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{ 
    [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
    [field: SerializeField] public List<Transform> Nests { get; private set; }
}
