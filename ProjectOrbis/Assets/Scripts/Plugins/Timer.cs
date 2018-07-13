using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbis {

    namespace Timing{

        public class Timer
        {
            const string FORMAT_OPTION_M = "00";
            const string FORMAT_OPTION_S = "00.00";

            string m_DefaultMinuteFormat = FORMAT_OPTION_M;
            string m_defaultSecondFormat = FORMAT_OPTION_S;

            float m_StartTime;
            float m_EndTime;
            float m_TotalTime;

            private bool isStarted = false;
            /// <summary>
            /// Has the timer started.
            /// </summary>
            public bool IsStarted { get { return isStarted; }  }

            /// <summary>
            /// Formating constructor.
            /// </summary>
            /// <param name="secondsFormatOption">The formatting settings for 'seconds'</param>
            /// <param name="minutesFormatOption">The formatting settings for 'minutes'</param>
            public Timer(string secondsFormatOption, string minutesFormatOption)
            {
                m_DefaultMinuteFormat = minutesFormatOption;
                m_defaultSecondFormat = secondsFormatOption;
            }

            /// <summary>
            /// Default constructor.
            /// </summary>
            public Timer()
            {
                //Null constructor
            }

            /// <summary>
            /// Starts timer.
            /// </summary>
            public void Start(float ElapsedTime = 0f)
            {
                if (isStarted == false) {
                    m_StartTime = Time.time - ElapsedTime;
                    isStarted = true;
                } else {
                    Debug.LogWarning("Timer is already started");
                }
            }

            /// <summary>
            /// Stops timer and returns total time in seconds;
            /// </summary>
            /// <returns>Time in seconds</returns>
            public float Stop()
            {
                if (isStarted) {
                    m_EndTime = Time.time;
                    m_TotalTime = m_EndTime - m_StartTime;
                    isStarted = false;
                    return m_TotalTime;
                } else {
                    Debug.LogWarning("Timer has not started");
                    return 0f;
                }
                
            }

            /// <summary>
            /// Resets timer to initial settings
            /// </summary>
            public void Reset()
            {
                m_TotalTime = 0;
                m_StartTime = 0;
                m_EndTime = 0;
                isStarted = false;
            }

            /// <summary>
            /// Formats time given in MM:SS.XX
            /// </summary>
            /// <param name="time">the time in seconds to format</param>
            /// <returns>formatted time as a string</returns>
            public TimeData FormatTime(float time)
            {
                return new TimeData(time, m_defaultSecondFormat, m_DefaultMinuteFormat);
            }

            public static TimeData FormatTime(float time, string secondsFormatOption, string minutesFormatOption)
            {
                return new TimeData(time, secondsFormatOption, minutesFormatOption);
            }

            /// <summary>
            /// Gets the timer's last time, formatted by the FormatTime method
            /// </summary>
            /// <returns></returns>
            public TimeData GetFormattedTime()
            {
                return FormatTime(m_TotalTime);
            }

            /// <summary>
            /// Returns current display time.
            /// </summary>
            /// <returns></returns>
            public string GetCurrentTimeString()
            {
                if (isStarted) {
                    float currTime = Time.time;
                    float passed = currTime - m_StartTime;

                    var _minutesPRECALC = Mathf.Floor(passed/ 60f);
                    float hours = Mathf.Floor(_minutesPRECALC / 60);
                    float minutes = Mathf.Floor(_minutesPRECALC % 60);
                    float seconds = (passed % 60);

                    return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00.00");
                } else {
                    return "00:00:00.00";
                }

            }
        }

        [System.Serializable]
        public struct TimeData {

            public float Hours { get; private set; }
            public float Minutes { get; private set; }
            public float Seconds { get; private set; }
            public float SecondsRaw; 
            public string TimeString;

            public TimeData(float seconds, string opt1, string opt2)
            {
                SecondsRaw = seconds;

                var _minutesPRECALC = Mathf.Floor(seconds / 60f);
                Hours = Mathf.Floor(_minutesPRECALC / 60);
                Minutes = Mathf.Floor(_minutesPRECALC % 60);
                Seconds = (seconds % 60);

                TimeString = Hours.ToString("00") + ":" + Minutes.ToString(opt2) + ":" + Seconds.ToString(opt1);
            }

            public static implicit operator string(TimeData t)
            {
                return t.TimeString;
            }

        }

    }
}

