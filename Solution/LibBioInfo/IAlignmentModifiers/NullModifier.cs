﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public class NullModifier : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            return;
        }
    }
}
