using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyAgreement : MonoBehaviour
{
    public GameObject PPpanel;
    public TMP_Text PPtext;
    public Button PPacept;
    private const string PrivacyPolicyAcceptedKey = "PPAccepted";

    private void Start()
    {
        if (PlayerPrefs.HasKey(PrivacyPolicyAcceptedKey))
        {
            PPpanel.SetActive(false);
        }
        else
        {
            ShowPP();
        }
    }
    public void ShowPP()
    {
        PPpanel.SetActive(true);
        PPtext.text = "Your privacy is important to us. This privacy policy explains what data we collect, how we use it, and your rights regarding this data when using our game.\n\n" +
                                 "Data Collection:\n" +
                                 "We may collect the following data when you use our game:\n" +
                                 "- Device IP address: to determine geographical location and monitor user activity.\n" +
                                 "- Device model: to analyze game performance on different devices.\n" +
                                 "- Error logs: to identify and fix issues encountered by users.\n\n" +
                                 "Data Usage:\n" +
                                 "The collected data is used for the following purposes:\n" +
                                 "- Monitoring and fixing errors: we analyze error logs to improve the stability and performance of the game.\n" +
                                 "- Performance analysis: device model data helps us optimize the game for various hardware configurations.\n" +
                                 "- Security: IP addresses are used to ensure security and prevent fraud.\n\n" +
                                 "Data Storage:\n" +
                                 "The collected data is stored on our secure servers and is accessible only to authorized personnel. We take reasonable measures to protect the data from unauthorized access, alteration, or destruction.\n\n" +
                                 "Data Disclosure:\n" +
                                 "We do not sell or transfer your personal data to third parties, except when required by law or necessary to protect our rights.\n\n" +
                                 "Cookies:\n" +
                                 "We do not use cookies to collect data.\n\n" +
                                 "Policy Changes:\n" +
                                 "We may update this privacy policy from time to time.";

        PPacept.onClick.AddListener(AcceptPP);
    }
    public void AcceptPP()
    {
        PlayerPrefs.SetInt(PrivacyPolicyAcceptedKey, 1);
        PlayerPrefs.Save();
        PPpanel.SetActive(false);
    }
}
