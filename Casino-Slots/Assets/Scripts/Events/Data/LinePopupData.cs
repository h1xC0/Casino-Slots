﻿namespace Events.Data
{
    public class LinePopupData : ILinePopupData
    {
        public int LineIndex { get; private set; }

        public LinePopupData(int lineIndex)
        {
            LineIndex = lineIndex;
        }
    }
}