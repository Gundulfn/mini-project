using UnityEngine;

// Public bool values are set from animation clips
// all animation loops are closed for performance improvement, therefore all changes and last states should be set correctly in animation clips
public class GunPart : MonoBehaviour
{
    private SkinnedMeshRenderer rend;
    public bool showObj, hideObj;

    // Skeleton state pose setting
    private Animator anim;
    public bool run, tPose, lyingPose, bendPose, straightLinePose, leftHandPose;

    // Material setting
    public bool setBlackMaterial, setRedMaterial, setYellowMaterial, setBlueMaterial, setGrayMaterial;
    public Material blackMaterial, redMaterial, yellowMaterial, blueMaterial, grayMaterial;

    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>(true);
    }

    // Sets GameObject state for current Gun
    public void UpdateCurrentState()
    {
        // Hide/Show GameObject respectively
        if (showObj)
        {
            rend.enabled = true;
        }
        else if (hideObj)
        {
            rend.enabled = false;
        }

        // Change GameObject pose for different Gun structures
        if (tPose)
        {
            anim.SetTrigger("tPose");
        }
        else if (lyingPose)
        {
            anim.SetTrigger("lyingPose");
        }
        else if (bendPose)
        {
            anim.SetTrigger("bendPose");
        }
        else if (straightLinePose)
        {
            anim.SetTrigger("straightLinePose");
        }
        else if (leftHandPose)
        {
            anim.SetTrigger("leftHandPose");
        }
        else
        {
            anim.SetBool("run", run);
        }

        // Set GameObject color for different Gun structures
        if (setBlackMaterial)
        {
            rend.sharedMaterial = blackMaterial;
        }
        else if (setRedMaterial)
        {
            rend.sharedMaterial = redMaterial;
        }
        else if (setYellowMaterial)
        {
            rend.sharedMaterial = yellowMaterial;
        }
        else if (setBlueMaterial)
        {
            rend.sharedMaterial = blueMaterial;
        }
        else if (setGrayMaterial)
        {
            rend.sharedMaterial = grayMaterial;
        }
    }
}