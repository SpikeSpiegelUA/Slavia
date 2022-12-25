using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Compass : MonoBehaviour
{
    public RawImage compassImage;
    public Transform player;
    public QuestMarker questMarker;
    float compassUnit;
    // Start is called before the first frame update
    void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
        if (questMarker.gameObject.activeSelf)
            questMarker.image.rectTransform.anchoredPosition = GetPositionOnCompass(questMarker);
        if (questMarker == null)
            gameObject.SetActive(false);
    }
    private Vector2 GetPositionOnCompass(QuestMarker marker)
    {
        Vector2 playerPosition = new Vector2(player.position.x, player.position.z);
        Vector2 playerForward = new Vector2(player.forward.x, player.forward.z);
        float angle = Vector2.SignedAngle(marker.position - playerPosition, playerForward);
        return new Vector2(compassUnit * angle,11f);
    }
}
