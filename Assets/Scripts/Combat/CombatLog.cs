using TMPro;
using UnityEngine;

namespace Combat
{
    public class CombatLog : MonoBehaviour
    {
        public static CombatLog Instance { get; private set; }

        private int _logIndex;
        private readonly string[] _logs = { "", "", "" };
        [SerializeField] private TMP_Text[] logTexts = new TMP_Text[3];

        private float _cooldownFirst;
        private float _cooldownSecond;
        private float _cooldownThird;
        private const float CooldownFirstMax = 6f;
        private const float CooldownSecondMax = 4f;
        private const float CooldownThirdMax = 2f;

        public void Awake()
        {
            Instance = this;
            ClearLog();
        }

        private void FixedUpdate()
        {
            _cooldownFirst += Time.fixedDeltaTime;
            _cooldownSecond += Time.fixedDeltaTime;
            _cooldownThird += Time.fixedDeltaTime;
            if (_cooldownFirst >= CooldownFirstMax)
            {
                _logs[0] = "";
                _cooldownFirst = 0;
            }
            if (_cooldownSecond >= CooldownSecondMax)
            {
                _logs[1] = "";
                _cooldownSecond = 0;
            }
            if (_cooldownThird >= CooldownThirdMax)
            {
                _logs[2] = "";
                _cooldownThird = 0;
            }
            UpdateLog();
        }

        public void AddLog(string strLog)
        {
            _logIndex++;
            _cooldownFirst = 0;
            _cooldownSecond = 0;
            _cooldownThird = 0;
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
