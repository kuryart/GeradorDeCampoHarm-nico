using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeCampoHarmônico.Class
{
    class Calculos
    {
        public string[] arrNotesText;
        public string[] arrIntervalsText;
        public int[] arrIntIntervals;
               
        private void SetValoresGerais()
        {
            arrNotesText[0] = "C";
            arrNotesText[1] = "C#";
            arrNotesText[2] = "D";
            arrNotesText[3] = "D#";
            arrNotesText[4] = "E";
            arrNotesText[5] = "F";
            arrNotesText[6] = "F#";
            arrNotesText[7] = "G";
            arrNotesText[8] = "G#";
            arrNotesText[9] = "A";
            arrNotesText[10] = "A#";
            arrNotesText[11] = "B";

            arrIntervalsText[0] = "2b";
            arrIntervalsText[1] = "2";
            arrIntervalsText[2] = "3m";
            arrIntervalsText[3] = "3M";
            arrIntervalsText[4] = "4";
            arrIntervalsText[5] = "5b";
            arrIntervalsText[6] = "5";
            arrIntervalsText[7] = "5#";
            arrIntervalsText[8] = "6";
            arrIntervalsText[9] = "7";
            arrIntervalsText[10] = "7M";

            arrIntIntervals[0] = 1;
            arrIntIntervals[1] = 2;
            arrIntIntervals[2] = 3;
            arrIntIntervals[3] = 4;
            arrIntIntervals[4] = 5;
            arrIntIntervals[5] = 6;
            arrIntIntervals[6] = 7;
            arrIntIntervals[7] = 8;
            arrIntIntervals[8] = 9;
            arrIntIntervals[9] = 10;
            arrIntIntervals[10] = 11;
        }

        private int[] IterarIntervalos(int[] arrIntervalsPassed)
        {
            int[] arrIntervalsCalculated = new int[10];

            return arrIntervalsCalculated;
        }
    }
}
