using UnityEngine;
using System.Collections;

public interface IDroppable
{
    void DropTo();
    void DropIn();

    private static readonly WaitForSeconds DROP_DELAY = new WaitForSeconds(0.01f);
    private static readonly float DROP_SPEED = 100;
    private static readonly float STOP_DISTANCE = .01f;

    public static IEnumerator DropThroughTime(Transform dropIntoTransform, Transform dropFromTransform)
    {
        while (Vector3.Distance(dropFromTransform.position, dropIntoTransform.position) > STOP_DISTANCE)
        {
            dropFromTransform.position = Vector3.Lerp(dropFromTransform.position, dropIntoTransform.position, Time.deltaTime * DROP_SPEED);
            yield return DROP_DELAY;
        }
    }

    public static IEnumerator DropThroughTime(Transform dropFromTransform)
    {
        Vector3 dropToPos = new Vector3(dropFromTransform.position.x, .75f, dropFromTransform.position.z - dropFromTransform.forward.z);

        while (Vector3.Distance(dropFromTransform.position, dropToPos) > STOP_DISTANCE)
        {
            dropFromTransform.position = Vector3.Lerp(dropFromTransform.position, dropToPos, Time.deltaTime * DROP_SPEED);
            yield return DROP_DELAY;
        }
    }
}