using System.Collections;
using UnityEngine;

public class EnvelopeClick : MonoBehaviour
{
    public GameObject invite;
    public GameObject text;

    public GameObject envelope;
    public GameObject acceptButton;
    public GameObject declineButton;

    public void OnEnvelopeClick()
    {
        text.SetActive(false);
        envelope.SetActive(false);
        StartCoroutine(ShowInvite());
    }

    private IEnumerator ShowInvite()
    {
        yield return new WaitForSeconds(0.4f);
        invite.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        acceptButton.SetActive(true);
        declineButton.SetActive(true);
    }
}
