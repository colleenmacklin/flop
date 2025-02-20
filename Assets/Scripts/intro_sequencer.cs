using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using MoreMountains.Tools;


public class intro_sequencer : MonoBehaviour
{

    [Header("Camera Stuff")]
    public CinemachineCamera[] cameras;

    public CinemachineCamera titleCamera;
    public CinemachineCamera playerCamera;
    public CinemachineCamera openingCamera;

    public CinemachineCamera startCamera;
    private CinemachineCamera currentCam;

    public Animator InstructionsMenuFader;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 _starting_position;
    public Vector3 mouse_offset;

    public float moveSpeed = 0.1f;
    public GameObject pen;
    public GameObject cap;
    public Rigidbody rbhand;
    public Rigidbody rcap;
    public Rigidbody _rpen;
    public bool capFall = false;

    Plane plane = new Plane(Vector3.back, 0);
    public float forceAmount;

    public bool MouseActive;
    public bool mouseIsMoving = false;
    private int i = 0;

    //public GameObject hand;
    public SceneCrossFade scenecrossfader;
    public AudioClip penCapSound;
    public AudioClip music;
    public GameAudio gameaudioplayer;

    private void OnEnable()
    {
        Actions.onMouseActivate += activateMouse;
        Actions.onMouseDeactivate += deactivateMouse;
    }

    private void OnDisable()
    {
        Actions.onMouseActivate -= activateMouse;
        Actions.onMouseDeactivate -= deactivateMouse;
    }


    void Start()
    {
        _starting_position = pen.transform.position;
        MouseActive = false;

        pen.GetComponent<BoingKit.BoingBones>().enabled = false;

        currentCam = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
        StartCoroutine(MySequence());

    }

    void Update()
    {
        if (MouseActive)
        {
            //check to see if mouseinputs have started (intro scene is over)
            screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (plane.Raycast(ray, out float distance))
            {
                worldPosition = ray.GetPoint(distance);
            }


            _rpen.position = worldPosition + mouse_offset;
            rbhand.position = worldPosition + mouse_offset;
            if (capFall == false)
            {
                rcap.position = worldPosition + mouse_offset;
            }
        }

        if (Input.GetAxis("Mouse Y") != 0)

        {
            // Mouse is moving

            i++;
            //Debug.Log("mouse is moving " + i);
                if (i > 20)
            {
                mouseIsMoving = true;
                InstructionsMenuFader.SetTrigger("CMFadeOut");

            }

        }

    }

    private void activateMouse()
    {
        MouseActive = true;

    }

    private void deactivateMouse()
    {
        MouseActive = false;

    }


    public void switchCamera(CinemachineCamera newCam)
    {
        currentCam = newCam;
        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }

    IEnumerator MySequence()
    {
        //sequencing:
        //1. hand and pen moves in
        // switch to playercam
        //2. up and down arrow appears
        //3. mouse is activated / player moves mouse
        //4. pen becomes floppy (enable boing bones)
        // show "floppy pen" title

        yield return new WaitForSeconds(2.5f);
        switchCamera(playerCamera);
        Actions.onMouseActivate();
        gameaudioplayer.playSound(music);
        //show mouse cue
        yield return new WaitForSeconds(1f);

        //instructions
        InstructionsMenuFader.SetTrigger("CMFadeIn");

        //Make boingy
        yield return new WaitForSeconds(2.5f);
        pen.GetComponent<BoingKit.BoingBones>().enabled = true;
        InstructionsMenuFader.SetTrigger("CMFadeIn");

        if (mouseIsMoving == true)
        {
            capFall = true;
            rcap.constraints = RigidbodyConstraints.None;
            cap.GetComponent<Rigidbody>().useGravity = true;

            rcap.AddExplosionForce(100f, rbhand.position, 10.0f, 3.0F);
            rcap.AddTorque(new Vector3(10f, 10f, 10f));
            gameaudioplayer.playSound(penCapSound);
        }
        yield return new WaitForSeconds(1.5f);

        scenecrossfader.fadeToLevel("Main Menu");
    }



}

