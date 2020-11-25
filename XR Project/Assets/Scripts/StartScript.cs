using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityTemplateProjects;

public class StartScript : MonoBehaviour
{
    public XRNode inputSource;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    public TextMeshPro score;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        score.text += Category.score;

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit){
        if (hit.collider.name == "Geography" || hit.collider.name == "Math" || hit.collider.name == "History")
        {
            Category.category = hit.collider.name;
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw*new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime);
    }
}
