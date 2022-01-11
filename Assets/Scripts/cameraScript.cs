using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    private Transform body;

    private void Start() {
        body = hero.GetComponent<Transform>();
    }
    private void Update() {
        transform.position = new Vector3 (body.position.x, transform.position.y, transform.position.z);
    }
}
