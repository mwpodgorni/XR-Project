using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityTemplateProjects;

public class ContinousMovemenet : MonoBehaviour
{
    private CharacterController character;
    private XRRig rig;
    public XRNode inputSource1;
    public XRNode inputSource2;
    private Vector2 inputAxis1;
    private Vector2 inputAxis2;


    public float additionalHeight = 0.2f;
    public GameObject wallLeft;
    public TextMeshPro textLeft;
    public GameObject wallCenter;
    public TextMeshPro textCenter;
    public GameObject wallRight;
    public TextMeshPro textRight;
    public GameObject wallTop;
    public TextMeshPro textTop;

    private int questionIndex = 0;
    public TextAsset geographyQuestions;
    public TextAsset mathQuestions;
    public TextAsset historyQuestions;

    private Questions chosenQuestions;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        Category.score = 0;
        switch (Category.category)
        {
            case "Geography":
                chosenQuestions = JsonUtility.FromJson<Questions>(geographyQuestions.text);
                break;
            case "Math":
                chosenQuestions = JsonUtility.FromJson<Questions>(mathQuestions.text);
                break;
            case "History":
                chosenQuestions = JsonUtility.FromJson<Questions>(historyQuestions.text);
                break;
        }

        textLeft.text = chosenQuestions.questions[questionIndex].answers[0].answer;
        textCenter.text = chosenQuestions.questions[questionIndex].answers[1].answer;
        textRight.text = chosenQuestions.questions[questionIndex].answers[2].answer;
        textTop.text = chosenQuestions.questions[questionIndex].question;
    }

    private void Update()
    {
        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputSource1);
        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);

        device1.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis1);
        device2.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis2);
    }

    private void FixedUpdate()
    {
        Vector3 direction1 = new Vector3(inputAxis1.x, 0, 0);
        Vector3 direction2 = new Vector3(inputAxis2.x, 0, 0);
        character.Move(direction1 *2* Time.fixedDeltaTime);
        character.Move(direction2 *2* Time.fixedDeltaTime);
        if (character.transform.position.z % 15 == 0 && character.transform.position.z < 140)
        {
            wallLeft.transform.position += new Vector3(0, 0, 15);
            textLeft.text = chosenQuestions.questions[questionIndex].answers[0].answer;
            if (Convert.ToBoolean(chosenQuestions.questions[questionIndex].answers[0].isCorrect))
            {
                wallLeft.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                wallLeft.GetComponent<BoxCollider>().enabled = true;
            }
            wallCenter.transform.position += new Vector3(0, 0, 15);
            textCenter.text = chosenQuestions.questions[questionIndex].answers[1].answer;
            if (Convert.ToBoolean(chosenQuestions.questions[questionIndex].answers[1].isCorrect))
            {
                wallCenter.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                wallCenter.GetComponent<BoxCollider>().enabled = true;
            }
            wallRight.transform.position += new Vector3(0, 0, 15);
            textRight.text = chosenQuestions.questions[questionIndex].answers[2].answer;
            if (Convert.ToBoolean(chosenQuestions.questions[questionIndex].answers[2].isCorrect))
            {
                wallRight.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                wallRight.GetComponent<BoxCollider>().enabled = true;
            }
            wallTop.transform.position += new Vector3(0, 0, 15);
            textTop.text = chosenQuestions.questions[questionIndex].question;
            questionIndex++;
            Category.score++;
        }

        character.Move(Vector3.forward * (Time.fixedDeltaTime*1.5f));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);
        // Debug.Log(chosenQuestions.questions[questionIndex].answers[1].answer);
        if (hit.collider.name == "End")
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }

        if (hit.collider.name == "WallLeft" || hit.collider.name == "WallCenter" || hit.collider.name == "WallRight"
        )
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }

        if (hit.collider.tag == "Obstacle")
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}