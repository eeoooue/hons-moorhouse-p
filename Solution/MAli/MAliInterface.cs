﻿using MAli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli
{
    public enum UserRequestError
    {
        ContainsForeignCommands,
        RequestIsAmbiguous,
        NoArgumentsGiven,
        RequestIsInvalid,
        MultipleLimitations,
    }

    public class MAliInterface
    {
        private MAliFacade Facade = new MAliFacade();
        private ArgumentHelper ArgumentHelper = new ArgumentHelper();

        public void ProcessArguments(string[] args)
        {
            Dictionary<string, string?> table = ArgumentHelper.InterpretArguments(args);

            if (args.Length == 0)
            {
                Facade.NotifyUserError(UserRequestError.NoArgumentsGiven);
                return;
            }

            if (ArgumentHelper.SpecifiesSeed(table))
            {
                Facade.SetSeed(table["seed"]!);
            }

            if (ArgumentHelper.ContainsForeignCommands(table))
            {
                Facade.NotifyUserError(UserRequestError.ContainsForeignCommands);
                return;
            }

            if (ArgumentHelper.SpecifiesMultipleLimitations(table))
            {
                Facade.NotifyUserError(UserRequestError.MultipleLimitations);
                return;
            }

            if (ArgumentHelper.IsAmbiguousRequest(table))
            {
                Facade.NotifyUserError(UserRequestError.RequestIsAmbiguous);
                return;
            }

            if (ArgumentHelper.IsParetoAlignmentRequest(table))
            {
                AlignmentRequest instructions = ArgumentHelper.UnpackInstructions(table);
                Facade.PerformParetoAlignment(instructions);
                return;
            }


            if (ArgumentHelper.IsBatchAlignmentRequest(table))
            {
                AlignmentRequest instructions = ArgumentHelper.UnpackInstructions(table);
                Facade.PerformBatchAlignment(instructions);
                return;
            }

            if (ArgumentHelper.IsAlignmentRequest(table))
            {
                AlignmentRequest instructions = ArgumentHelper.UnpackInstructions(table);
                Facade.PerformAlignment(instructions);
                return;
            }

            if (ArgumentHelper.IsHelpRequest(table))
            {
                Facade.ProvideHelp();
                return;
            }

            Facade.NotifyUserError(UserRequestError.RequestIsInvalid);
        }
    }
}
