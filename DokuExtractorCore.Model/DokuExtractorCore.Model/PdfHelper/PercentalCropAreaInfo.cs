﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DokuExtractorCore.Model.PdfHelper
{
   public class PercentalCropAreaInfo
    {
        public int PageNumber { get; set; }
        public float TopLeftX { get; set; }
        public float TopLeftY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}
