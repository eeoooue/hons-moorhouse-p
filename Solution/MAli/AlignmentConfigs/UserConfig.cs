﻿using LibAlignment;
using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibModification;
using LibScoring;
using LibScoring.FitnessFunctions;
using MAli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MAli.AlignmentConfigs
{
    public class UserConfig : BaseAlignmentConfig
    {
        // allows the user to provide a customized configuration of objectives

        private BaseAlignmentConfig BaseConfig;
        private JsonConfigHelper Helper = new JsonConfigHelper();
        private string FilePath;

        public UserConfig(BaseAlignmentConfig baseConfig, string filepath)
        {
            BaseConfig = baseConfig;
            FilePath = filepath;
        }

        public override IterativeAligner CreateAligner()
        {
            IterativeAligner aligner = BaseConfig.CreateAligner();
            JsonElement root = Helper.ReadConfigFrom(FilePath);
            JsonElement objElement = root.GetProperty("Objective");
            aligner.Objective = Helper.ExtractObjective(objElement);

            return aligner;
        }
    }
}
