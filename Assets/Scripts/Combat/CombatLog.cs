using TMPro;
using UnityEngine;

namespace Combat
{
    public class CombatLog : MonoBehaviour
    {
        public static CombatLog Instance { get; private set; }

        private int logIndex = 0;
        private readonly string[] _logs = { "", "", "" };
        [SerializeField] private TMP_Text[] logTexts = new TMP_Text[3];

        public void Awake()
        {
            Instance = this;
            ClearLog();
        }

        public void AddLog(string strLog)
        {
            logIndex++;
            _logs[2] = _logs[1];
            _logs[1] = _logs[0];
            _logs[0] = strLog;

            UpdateLog();
        }

        private void ClearLog()
        {
            foreach (var t in logTexts)
            {
                t.text = "";
            }
        }

        private void UpdateLog()
        {
            for (int i = 0; i < logTexts.Length; i++)
            {
                logTexts[i].text = _logs[i];
            }
        }
    }
}
