using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ScrollNavigateSample
{
    class DocPosition
    {
        public double Horizontal { get; set; }

        public double Vertical { get; set; }

        public int ZoomPercent { get; set; }

        public DocPosition(int zoompercentage, double horizontal, double vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
            ZoomPercent = zoompercentage;
        }
    }
}
